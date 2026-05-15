using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Chuỗi kết nối database - Lấy username và password từ cơ sở dữ liệu
        // Database: Cowofsurviva
        // Table: Users (MaUS - primary key, TenUS - tên người dùng, Password - mật khẩu)
        string connect = @"Data Source=DAI-THANG\MSSQLSERVER04;Initial Catalog=Cowofsurviva;User ID=sa;Password=231006;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;
        

        public Form1()
        {
            InitializeComponent();
            // Khởi tạo kết nối SQL
            con = new SqlConnection(connect);
        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Xoá text gợi ý khi form load
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        // Nút LOGIN - Kiểm tra MaUS (mã người dùng) và Password từ database
        private void btt1_Click(object sender, EventArgs e)
        {
            // Lấy MaUS và password từ textbox
            string maUS = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Kiểm tra xem MaUS và password có được nhập không
            if (string.IsNullOrEmpty(maUS) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi hàm kiểm tra đăng nhập
            if (KiemTraDangNhap(maUS, password))
            {
                // Đăng nhập thành công - Chuyển sang trang chủ
                MessageBox.Show("Đăng nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmtrangchu trangchu = new frmtrangchu();
                this.Hide();
                trangchu.Show();
            }
            else
            {
                // Đăng nhập thất bại
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
            }
        }

        // Hàm kiểm tra MaUS và Password trong database
        // Sử dụng table Users với cột: MaUS (primary key), TenUS (tên), Password
        private bool KiemTraDangNhap(string maUS, string password)
        {
            try
            {
                // Mở kết nối
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Tạo câu lệnh SQL để kiểm tra user từ table Users
                // Kiểm tra MaUS (tài khoản/mã người dùng) và Password
                // Chọn cả MaUS và TenUS để lưu vào session
                string query = "SELECT MaUS, TenUS FROM Users WHERE MaUS = @maUS AND Password = @password";
                cmd = new SqlCommand(query, con);

                // Thêm tham số để tránh SQL Injection
                cmd.Parameters.AddWithValue("@maUS", maUS);
                cmd.Parameters.AddWithValue("@password", password);

                // Thực thi câu lệnh và lấy dữ liệu
                SqlDataReader reader = cmd.ExecuteReader();

                // Nếu có dữ liệu, đăng nhập thành công
                if (reader.HasRows)
                {
                    // Đọc dòng dữ liệu đầu tiên
                    reader.Read();

                    // Lấy MaUS và TenUS từ database
                    string maUSFromDB = reader["MaUS"].ToString();
                    string tenUSFromDB = reader["TenUS"].ToString();

                    // Lưu vào CurrentUser session
                    CurrentUser.MaUS = maUSFromDB;
                    CurrentUser.TenUS = tenUSFromDB;

                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }
            catch (SqlException ex)
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
                // Đóng kết nối
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // Nút FORGET - Quên mật khẩu
        private void btt2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên để lấy lại mật khẩu!", "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
