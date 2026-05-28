using System;
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

        private void frmlevel1_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

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
            {
                ptrb1.Location = new System.Drawing.Point(newX, newY);
            }

            if (ptrb1.Bounds.IntersectsWith(ptrb2.Bounds))
            {
                this.KeyDown -= frmlevel1_KeyDown;

                var questionResult = MessageBox.Show(
                    "Bạn đã đến đích!\n\nNhư tên thật là gì?\nA. Cow Thị Như Quỳnh\nB. Cao Thị Quỳnh Như\nC. Cow Thị Quỳnh Như\n\nChọn Yes cho A, No cho B, Cancel cho C.",
                    "Câu hỏi kết thúc màn chơi",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (questionResult == DialogResult.Yes)
                {
                    CongDiem(10);
                    MessageBox.Show("Chính xác! Bạn đã vượt qua level này!\n\nBạn được cộng 10 điểm!", "Thắng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sai rồi! Bạn đã thua.\n\nCâu trả lời đúng là: A", "Thua", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                frmtrangchu trangchu = new frmtrangchu();
                trangchu.RefreshBangDiem();
                this.Hide();
                trangchu.Show();

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

                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                string query = "UPDATE Users SET DiemSo = DiemSo + @diem WHERE MaUS = @maUS";
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
                    MessageBox.Show("Không thể cộng điểm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        private bool IsCollidingWithObstacles(System.Drawing.Rectangle playerBounds)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.Tag != null)
                {
                    string tag = control.Tag.ToString();
                    if (playerBounds.IntersectsWith(control.Bounds))
                    {
                        if (tag == "chan")
                        {
                            return true;
                        }
                        else if (tag == "dich")
                        {
                            var result = MessageBox.Show(
                                "Việt Nam có bao nhiêu tỉnh thành?\nA. 63\nB. 64\nC. 61\n\nChọn Yes cho 63, No cho 64, Cancel cho 61.",
                                "Câu hỏi khi chạm đích",
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question
                            );
                            if (result == DialogResult.Yes)
                            {
                                CongDiem(5);
                                MessageBox.Show("Chính xác! Bạn được cộng 5 điểm.", "Đúng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            else
                            {
                                MessageBox.Show("Sai rồi! Bạn không được đi qua.", "Sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void label24_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}