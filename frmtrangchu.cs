using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class frmtrangchu : Form
    {
        private string dbPath = Application.StartupPath + @"\Cowofsurviva.db";
        private string connect => $"Data Source={dbPath};Version=3;";
        private SQLiteConnection con;
        private SQLiteDataAdapter adt;
        private DataTable dt;

        public frmtrangchu()
        {
            InitializeComponent();
            con = new SQLiteConnection(connect);
        }

        private void frmtrangchu_Load(object sender, EventArgs e)
        {
            CauHinhDataGridView();
            TaiDiemSo();
        }

        private void CauHinhDataGridView()
        {
            dgbang.AllowUserToAddRows = false;
            dgbang.AllowUserToDeleteRows = false;
            dgbang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgbang.MultiSelect = false;
            dgbang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void TaiDiemSo()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                dt = new DataTable();
                string query = @"
                    SELECT MaUS AS [Mã người chơi], 
                           TenUS AS [Tên người chơi], 
                           COALESCE(DiemSo, 0) AS [Điểm số] 
                    FROM Users 
                    ORDER BY DiemSo DESC";

                adt = new SQLiteDataAdapter(query, con);
                adt.Fill(dt);
                dgbang.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("Không có dữ liệu người chơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Lỗi kết nối database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void RefreshBangDiem()
        {
            if (dt != null)
                dt.Clear();
            TaiDiemSo();
        }

        private void btt1_Click(object sender, EventArgs e)
        {
            frmload load = new frmload();
            this.Hide();
            load.Show();
        }
    }
}