using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class frmlevel1 : Form
    {
        private int moveStep = 5;
        private string dbPath = Application.StartupPath + @"\Cowofsurviva.db";
        private string connect => $"Data Source={dbPath};Version=3;";
        private SQLiteConnection con;
        private SQLiteCommand cmd;

        public frmlevel1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += frmlevel1_KeyDown;
            this.Load += frmlevel1_Load;
            con = new SQLiteConnection(connect);
        }

        private void frmlevel1_Load(object sender, EventArgs e) => this.Focus();

        private void frmlevel1_KeyDown(object sender, KeyEventArgs e)
        {
            int newX = ptrb1.Location.X;
            int newY = ptrb1.Location.Y;

            switch (e.KeyCode)
            {
                case Keys.Up: newY -= moveStep; break;
                case Keys.Down: newY += moveStep; break;
                case Keys.Left: newX -= moveStep; break;
                case Keys.Right: newX += moveStep; break;
                default: return;
            }

            var newPosition = new System.Drawing.Rectangle(newX, newY, ptrb1.Width, ptrb1.Height);
            if (!IsCollidingWithObstacles(newPosition))
                ptrb1.Location = new System.Drawing.Point(newX, newY);

            if (ptrb1.Bounds.IntersectsWith(ptrb2.Bounds))
            {
                this.KeyDown -= frmlevel1_KeyDown;

                // CÂU HỎI CỦA LEVEL 1 (sửa theo ý bạn)
                var questionResult = MessageBox.Show(
                    "Bạn đã đến đích!\n\nNhư tên thật là gì?\nA. Cow Thị Như Quỳnh\nB. Cao Thị Quỳnh Như\nC. Cow Thị Quỳnh Như\n\nChọn Yes cho A, No cho B, Cancel cho C.",
                    "Câu hỏi kết thúc màn chơi",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (questionResult == DialogResult.Yes) // ĐÚNG
                {
                    int diemThuong = 10;   // Điểm thưởng level 1
                    CongDiem(diemThuong);
                    MessageBox.Show($"Chính xác! Bạn được cộng {diemThuong} điểm!", "Thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn chơi level tiếp theo không?
                    DialogResult next = MessageBox.Show(
                        "Bạn có muốn chơi Level 2 không?",
                        "Chuyển level",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (next == DialogResult.Yes)
                    {
                        // Mở level tiếp theo (frmlevel2)
                        frmlevel2 nextLevel = new frmlevel2();
                        nextLevel.Show();
                        this.Close();   // Đóng level hiện tại
                    }
                    else
                    {
                        // Về trang chủ
                        ReturnToHomePage();
                    }
                }
                else // SAI
                {
                    MessageBox.Show("Sai rồi! Bạn đã thua.\n\nCâu trả lời đúng là: A", "Thua", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ReturnToHomePage();
                }

                e.Handled = true;
                return;
            }
            e.Handled = true;
        }

        // Hàm quay về trang chủ và refresh bảng điểm
        private void ReturnToHomePage()
        {
            var trangchu = Application.OpenForms.OfType<frmtrangchu>().FirstOrDefault();
            if (trangchu == null)
                trangchu = new frmtrangchu();
            else
                trangchu.RefreshBangDiem();
            trangchu.Show();
            this.Close();
        }

        private int CongDiem(int diem)
        {
            try
            {
                if (!CurrentUser.IsLoggedIn())
                {
                    MessageBox.Show("Không có người dùng đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                if (con.State == System.Data.ConnectionState.Closed) con.Open();
                string query = "UPDATE Users SET DiemSo = COALESCE(DiemSo, 0) + @diem WHERE MaUS = @maUS";
                cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@diem", diem);
                cmd.Parameters.AddWithValue("@maUS", CurrentUser.MaUS);
                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    MessageBox.Show($"Bạn đã được cộng {diem} điểm!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return diem;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy người dùng để cộng điểm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }
        }

        private bool IsCollidingWithObstacles(System.Drawing.Rectangle playerBounds)
        {
            foreach (Control control in this.Controls)
                if (control is Label && control.Tag != null && control.Tag.ToString() == "chan")
                    if (playerBounds.IntersectsWith(control.Bounds)) return true;
            return false;
        }

        private void label24_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}