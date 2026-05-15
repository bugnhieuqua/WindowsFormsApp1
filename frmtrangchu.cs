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
    public partial class frmtrangchu : Form
    {
        // Chuỗi kết nối đến database Cowofsurviva
        string connect = @"Data Source=DAI-THANG\MSSQLSERVER04;Initial Catalog=Cowofsurviva;User ID=sa;Password=231006;TrustServerCertificate=True";
        SqlConnection con;
        SqlDataAdapter adt;
        DataTable dt;

        public frmtrangchu()
        {
            InitializeComponent();
            con = new SqlConnection(connect);
        }

        // Khi form load, tải bảng điểm số từ database
        private void frmtrangchu_Load(object sender, EventArgs e)
        {
            // Cấu hình DataGridView
            CauHinhDataGridView();

            // Tải dữ liệu điểm số
            TaiDiemSo();
        }

        // Cấu hình các cột cho DataGridView
        private void CauHinhDataGridView()
        {
            dgbang.AllowUserToAddRows = false;
            dgbang.AllowUserToDeleteRows = false;
            dgbang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgbang.MultiSelect = false;
            dgbang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Tải dữ liệu điểm số từ database
        private void TaiDiemSo()
        {
            try
            {
                // Mở kết nối
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Tạo DataTable để lưu trữ dữ liệu
                dt = new DataTable();

                // Tạo câu SQL để lấy tất cả người chơi và điểm số
                // Hiện tại Users chưa có cột DiemSo, nên sẽ hiển thị tất cả người chơi
                // Sau này có thể mở rộng với bảng Scores riêng
                string query = "SELECT MaUS AS [Mã người chơi], TenUS AS [Tên người chơi], ISNULL(DiemSo, 0) AS [Điểm số] FROM Users ORDER BY DiemSo DESC";

                // Sử dụng SqlDataAdapter để tải dữ liệu
                adt = new SqlDataAdapter(query, con);
                adt.Fill(dt);

                // Gán DataTable cho DataGridView
                dgbang.DataSource = dt;

                // Nếu không có dữ liệu, hiển thị thông báo
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu người chơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Hàm public để refresh bảng điểm - có thể gọi từ frmlevel1
        public void RefreshBangDiem()
        {
            // Clear DataTable cũ
            if (dt != null)
            {
                dt.Clear();
            }

            // Tải lại dữ liệu mới
            TaiDiemSo();
        }

        // Nút START - Vào game
        private void btt1_Click(object sender, EventArgs e)
        {
            // Load trước khi vào game
            frmload load = new frmload();
            this.Hide();
            load.Show();
        }
    }
}
