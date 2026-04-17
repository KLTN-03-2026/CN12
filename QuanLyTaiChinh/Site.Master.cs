using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyTaiChinh
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                HienThiMenuTheoVaiTro();
        }

        private void HienThiMenuTheoVaiTro()
        {
            string vaiTro = Session["VaiTro"] != null ? Session["VaiTro"].ToString() : "";

            // Lấy tên trang hiện tại
            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            string menuHtml = "";

            // Hàm tạo link có active
            Func<string, string, string> TaoLink = (url, text) =>
            {
                string active = (currentPage.Equals(url, StringComparison.OrdinalIgnoreCase)) ? "active" : "";
                return $"<a href='{url}' class='{active}'>{text}</a>";
            };

            // Chưa đăng nhập
            if (string.IsNullOrEmpty(vaiTro))
            {
                menuHtml += TaoLink("TrangChu.aspx", "Trang chủ");
                menuHtml += TaoLink("GioiThieu.aspx", "Giới thiệu");
                menuHtml += TaoLink("DangKy.aspx", "Đăng ký");
                menuHtml += TaoLink("DangNhap.aspx", "Đăng nhập");
            }
            // Thành viên
            else if (vaiTro == "ThanhVien")
            {
                menuHtml += TaoLink("TrangChu.aspx", "Trang chủ");
                menuHtml += TaoLink("QuanLyTaiKhoan.aspx", "Quản lý tài khoản");
                menuHtml += TaoLink("QuanLyKhoanThu.aspx", "Quản lý khoản thu");
                menuHtml += TaoLink("QuanLyKhoanChi.aspx", "Quản lý khoản chi");
                menuHtml += TaoLink("NganSach.aspx", "Ngân sách");
                menuHtml += TaoLink("ThongKe.aspx", "Thống kê");

                menuHtml += "<a href='DangXuat.aspx' style='color:red;'>Đăng xuất</a>";
            }
            // Admin
            else if (vaiTro == "Admin")
            {
                menuHtml += TaoLink("TrangChu.aspx", "Trang chủ");
                menuHtml += TaoLink("QuanLyNguoiDung.aspx", "Quản lý người dùng");
                menuHtml += TaoLink("QuanLyDanhMuc.aspx", "Quản lý danh mục");

                menuHtml += "<a href='DangXuat.aspx' style='color:red;'>Đăng xuất</a>";
            }

            menu.InnerHtml = menuHtml;
        }
    }
}