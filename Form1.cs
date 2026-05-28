using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string dbPath = Application.StartupPath + @"\Cowofsurviva.db";
        private string connect => $"Data Source={dbPath};Version=3;";
        private SQLiteConnection con;
        private SQLiteCommand cmd;

        public Form1()
        {
            InitializeComponent();
            con = new SQLiteConnection(connect);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void btt1_Click(object sender, EventArgs e)
        {
            string maUS = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(maUS) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (KiemTraDangNhap(maUS, password))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmtrangchu trangchu = new frmtrangchu();
                this.Hide();
                trangchu.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
            }
        }

        private bool KiemTraDangNhap(string maUS, string password)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                string query = "SELECT MaUS, TenUS FROM Users WHERE MaUS = @maUS AND Password = @password";
                cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@maUS", maUS);
                cmd.Parameters.AddWithValue("@password", password);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CurrentUser.MaUS = reader["MaUS"].ToString();
                        CurrentUser.TenUS = reader["TenUS"].ToString();
                        return true;
                    }
                    return false;
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Lỗi kết nối database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        private void btt2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên để lấy lại mật khẩu!", "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}