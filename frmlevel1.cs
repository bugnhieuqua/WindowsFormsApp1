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
    // Lớp form cho Level 1 của trò chơi
    public partial class frmlevel1 : Form
    {
        // Biến xác định mỗi lần di chuyển nhân vật đi bao nhiêu pixel (5 pixel)
        private int moveStep = 5;

        // Chuỗi kết nối database
        string connect = @"Data Source=DAI-THANG\MSSQLSERVER04;Initial Catalog=Cowofsurviva;User ID=sa;Password=231006;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;

        // Constructor: Khởi tạo form
        public frmlevel1()
        {
            InitializeComponent();

            // KeyPreview = true cho phép form nhận được tất cả các sự kiện phím
            // trước khi các control con xử lý chúng
            this.KeyPreview = true;

            // Gắn event handler cho sự kiện nhấn phím
            this.KeyDown += frmlevel1_KeyDown;

            // Gắn event handler cho sự kiện form load
            this.Load += frmlevel1_Load;

            // Khởi tạo kết nối SQL
            con = new SqlConnection(connect);
        }

        // Sự kiện khi form được tải lên
        // Mục đích: Đặt focus vào form để form có thể nhận sự kiện phím
        private void frmlevel1_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        // Sự kiện xảy ra khi người chơi nhấn một phím bất kỳ
        private void frmlevel1_KeyDown(object sender, KeyEventArgs e)
        {
            // Lấy vị trí hiện tại của nhân vật (ptrb1)
            int newX = ptrb1.Location.X;
            int newY = ptrb1.Location.Y;

            // Xử lý các phím mũi tên để di chuyển nhân vật
            switch (e.KeyCode)
            {
                // Phím lên: giảm Y (di chuyển lên)
                case Keys.Up:
                    newY -= moveStep;
                    break;

                // Phím xuống: tăng Y (di chuyển xuống)
                case Keys.Down:
                    newY += moveStep;
                    break;

                // Phím trái: giảm X (di chuyển sang trái)
                case Keys.Left:
                    newX -= moveStep;
                    break;

                // Phím phải: tăng X (di chuyển sang phải)
                case Keys.Right:
                    newX += moveStep;
                    break;

                // Nếu không phải phím mũi tên, thoát khỏi hàm
                default:
                    return;
            }

            // Tạo một hình chữ nhật tại vị trí mới để kiểm tra va chạm
            // (vị trí tại X, Y mới với kích thước của nhân vật)
            var newPosition = new System.Drawing.Rectangle(newX, newY, ptrb1.Width, ptrb1.Height);

            // Kiểm tra xem vị trí mới có va chạm với tường (obstacle) không
            // Nếu không va chạm thì cho phép di chuyển
            if (!IsCollidingWithObstacles(newPosition))
            {
                // Cập nhật vị trí mới cho nhân vật
                ptrb1.Location = new System.Drawing.Point(newX, newY);
            }

            // Kiểm tra xem nhân vật có chạm vào điểm kết thúc (ptrb2) không
            if (ptrb1.Bounds.IntersectsWith(ptrb2.Bounds))
            {
                // Vô hiệu hóa phím để tránh xử lý thêm
                this.KeyDown -= frmlevel1_KeyDown;

                // Hỏi câu hỏi khi chạm ptrb2 (đích đến)
                var questionResult = MessageBox.Show(
                    "Bạn đã đến đích!\n\nNhư tên thật là gì?\nA. Cow Thị Như Quỳnh\nB. Cao Thị Quỳnh Như\nC. Cow Thị Quỳnh Như\n\nChọn Yes cho A, No cho B, Cancel cho C.",
                    "Câu hỏi kết thúc màn chơi",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (questionResult == DialogResult.Yes)
                {
                    // Trả lời đúng (63 tỉnh thành)
                    // Cộng 10 điểm vào database
                    CongDiem(10);

                    // Hiển thị thông báo thắng
                    MessageBox.Show(
                        "Chính xác! Bạn đã vượt qua level này!\n\nBạn được cộng 10 điểm!",
                        "Thắng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Quay về trang chủ
                    frmtrangchu trangchu = new frmtrangchu();
                    trangchu.RefreshBangDiem();
                    this.Hide();
                    trangchu.Show();
                }
                else
                {
                    // Trả lời sai - không cộng điểm, thua
                    MessageBox.Show(
                        "Sai rồi! Bạn đã thua.\n\nCâu trả lời đúng là: A",
                        "Thua",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    // Quay về trang chủ mà không cộng điểm
                    frmtrangchu trangchu = new frmtrangchu();
                    trangchu.RefreshBangDiem();
                    this.Hide();
                    trangchu.Show();
                }

                e.Handled = true;
                return;
            }

            // Đánh dấu rằng event này đã được xử lý
            e.Handled = true;
        }

        // Hàm cộng điểm cho người chơi hiện tại
        // Sửa chữ ký phương thức từ void thành int
        private int CongDiem(int diem)
        {
            try
            {
                MessageBox.Show($"DEBUG: CurrentUser.MaUS = '{CurrentUser.MaUS}'", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!CurrentUser.IsLoggedIn())
                {
                    MessageBox.Show("Không có người dùng đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;   // Trả về 0 nếu không có user
                }

                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "UPDATE Users SET DiemSo = DiemSo + @diem WHERE MaUS = @maUS";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@diem", diem);
                cmd.Parameters.AddWithValue("@maUS", CurrentUser.MaUS);

                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show($"Bạn đã được cộng {diem} điểm!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return diem;   // Trả về số điểm đã cộng
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
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

       

        // Hàm kiểm tra va chạm giữa nhân vật và các tường
        // Tham số: playerBounds - hình chữ nhật của vị trí mới của nhân vật
        // Trả về: true nếu va chạm, false nếu không va chạm
        private bool IsCollidingWithObstacles(System.Drawing.Rectangle playerBounds)
        {
            // Duyệt qua tất cả các control trên form
            foreach (Control control in this.Controls)
            {
                // Chỉ xử lý Label có Tag
                if (control is Label && control.Tag != null)
                {
                    string tag = control.Tag.ToString();

                    // Nếu vị trí mới giao với control này
                    if (playerBounds.IntersectsWith(control.Bounds))
                    {
                        if (tag == "chan")
                        {
                            // Vật cản: không thể đi qua
                            //MessageBox.Show("Đây là vật cản, bạn không thể đi qua!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return true; // Có va chạm -> chặn di chuyển
                        }
                        else if (tag == "dich")
                        {
                            // Đích: hỏi đáp, đúng thì cho qua và cộng điểm, sai thì chặn
                            var result = MessageBox.Show(
                                "Việt Nam có bao nhiêu tỉnh thành?\nA. 63\nB. 64\nC. 61\n\nChọn Yes cho 63, No cho 64, Cancel cho 61.",
                                "Câu hỏi khi chạm đích",
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question
                            );

                            if (result == DialogResult.Yes)
                            {
                                // Đúng (63 tỉnh thành) - cho đi qua và cộng điểm
                                CongDiem(5); // Cộng 5 điểm
                                MessageBox.Show("Chính xác! Bạn được cộng 5 điểm.", "Đúng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false; // Không chặn -> cho di chuyển
                            }
                            else
                            {
                                // Sai - không cho đi qua
                                MessageBox.Show("Sai rồi! Bạn không được đi qua.", "Sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return true; // Chặn di chuyển
                            }
                        }
                    }
                }
            }

            // Không có va chạm với vật cản hoặc đích
            return false;
        }

        // Sự kiện click label (không sử dụng)
        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
