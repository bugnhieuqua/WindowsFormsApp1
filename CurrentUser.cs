using System;

namespace WindowsFormsApp1
{
    // Class tĩnh lưu thông tin session của người chơi hiện tại
    public static class CurrentUser
    {
        // Lưu Mã Người Dùng (MaUS) của người đang đăng nhập
        public static string MaUS { get; set; } = "";

        // Lưu Tên Người Dùng (TenUS) - optional
        public static string TenUS { get; set; } = "";

        // Hàm kiểm tra xem có người dùng nào đang đăng nhập không
        public static bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(MaUS);
        }

        // Hàm đăng xuất - xóa dữ liệu session
        public static void Logout()
        {
            MaUS = "";
            TenUS = "";
        }
    }
}
