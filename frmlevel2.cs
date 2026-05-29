using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class frmlevel2 : Form
    {
        private int moveStep = 5;
        private string dbPath = Application.StartupPath + @"\Cowofsurviva.db";
        private string connect => $"Data Source={dbPath};Version=3;";
        private SQLiteConnection con;
        private SQLiteCommand cmd;

        public frmlevel2()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += frmlevel2_KeyDown;
            this.Load += frmlevel2_Load;
            con = new SQLiteConnection(connect);
        }

        private void frmlevel2_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void frmlevel2_KeyDown(object sender, KeyEventArgs e)
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
            {
                ptrb1.Location = new System.Drawing.Point(newX, newY);
            }

            if (ptrb1.Bounds.IntersectsWith(ptrb2.Bounds))
            {
                this.KeyDown -= frmlevel2_KeyDown;

                var questionResult = MessageBox.Show(
                    "Bạn đã đến đích Level 2!\n\nĐiền vào chỗ trống sau: ....., tôi là cow?\nA. Không\nB. Đúng\nC. Không đúng\n\nChọn Yes cho A, No cho B, Cancel cho C.",
                    "Câu hỏi Level 2",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (questionResult == DialogResult.No)
                {
                    CongDiem(20);
                    MessageBox.Show("Chính xác! Bạn được cộng 10 điểm!", "Thắng Level 2", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi chuyển lên Level 3
                    DialogResult next = MessageBox.Show(
                        "Bạn có muốn tiếp tục chơi Level 3 không?",
                        "Chuyển level",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                    if (next == DialogResult.Yes)
                    {
                        frmlevel3 level3 = new frmlevel3();
                        level3.Show();
                        this.Close();
                    }
                    else
                    {
                        var trangchu = Application.OpenForms.OfType<frmtrangchu>().FirstOrDefault();
                        if (trangchu == null) trangchu = new frmtrangchu();
                        else trangchu.RefreshBangDiem();
                        trangchu.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Sai rồi! Đáp án đúng là B. Bạn quay về trang chủ.", "Thua", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    var trangchu = Application.OpenForms.OfType<frmtrangchu>().FirstOrDefault();
                    if (trangchu == null) trangchu = new frmtrangchu();
                    else trangchu.RefreshBangDiem();
                    trangchu.Show();
                    this.Close();
                }
                e.Handled = true;
                return;
            }
            e.Handled = true;
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
            {
                if (control is Label && control.Tag != null && control.Tag.ToString() == "chan")
                {
                    if (playerBounds.IntersectsWith(control.Bounds)) return true;
                }
            }
            return false;
        }
    }
}