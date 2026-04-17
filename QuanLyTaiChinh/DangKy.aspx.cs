using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace QuanLyTaiChinh
{
    public partial class DangKy : Page
    {
        string strConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string xacNhanMatKhau = txtXacNhanMatKhau.Text.Trim();

            if (hoTen == "" || email == "" || soDienThoai == "" || matKhau == "" || xacNhanMatKhau == "")
            {
                lblThongBao.CssClass = "error-message";
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin.";
                return;
            }

            if (matKhau != xacNhanMatKhau)
            {
                lblThongBao.CssClass = "error-message";
                lblThongBao.Text = "Mật khẩu xác nhận không khớp.";
                return;
            }

            if (matKhau.Length < 6)
            {
                lblThongBao.CssClass = "error-message";
                lblThongBao.Text = "Mật khẩu phải có ít nhất 6 ký tự.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    string sqlCheck = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email";
                    SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, tran);
                    cmdCheck.Parameters.AddWithValue("@Email", email);

                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count > 0)
                    {
                        lblThongBao.CssClass = "error-message";
                        lblThongBao.Text = "Email đã tồn tại.";
                        tran.Rollback();
                        return;
                    }

                    string sqlVaiTro = "SELECT MaVaiTro FROM VaiTro WHERE TenVaiTro = N'ThanhVien'";
                    SqlCommand cmdVaiTro = new SqlCommand(sqlVaiTro, conn, tran);
                    object maVaiTroObj = cmdVaiTro.ExecuteScalar();

                    if (maVaiTroObj == null)
                    {
                        lblThongBao.CssClass = "error-message";
                        lblThongBao.Text = "Không tìm thấy vai trò Thành viên.";
                        tran.Rollback();
                        return;
                    }

                    int maVaiTro = Convert.ToInt32(maVaiTroObj);

                    string sqlInsertTaiKhoan = @"
                        INSERT INTO TaiKhoan (Email, MatKhau, MaVaiTro, TrangThai)
                        VALUES (@Email, @MatKhau, @MaVaiTro, 1);
SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdTaiKhoan = new SqlCommand(sqlInsertTaiKhoan, conn, tran);
                    cmdTaiKhoan.Parameters.AddWithValue("@Email", email);
                    cmdTaiKhoan.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmdTaiKhoan.Parameters.AddWithValue("@MaVaiTro", maVaiTro);

                    int maTaiKhoan = Convert.ToInt32(cmdTaiKhoan.ExecuteScalar());

                    string sqlInsertThongTin = @"
                        INSERT INTO ThongTinCaNhan (MaTaiKhoan, HoTen, SoDienThoai)
                        VALUES (@MaTaiKhoan, @HoTen, @SoDienThoai)";

                    SqlCommand cmdThongTin = new SqlCommand(sqlInsertThongTin, conn, tran);
                    cmdThongTin.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    cmdThongTin.Parameters.AddWithValue("@HoTen", hoTen);
                    cmdThongTin.Parameters.AddWithValue("@SoDienThoai", soDienThoai);

                    cmdThongTin.ExecuteNonQuery();

                    tran.Commit();

                    lblThongBao.CssClass = "error-message success-message";
                    lblThongBao.Text = "Đăng ký thành công.";

                    txtHoTen.Text = "";
                    txtEmail.Text = "";
                    txtSoDienThoai.Text = "";
                    txtMatKhau.Text = "";
                    txtXacNhanMatKhau.Text = "";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    lblThongBao.CssClass = "error-message";
                    lblThongBao.Text = "Lỗi: " + ex.Message;
                }
            }
        }
    }
}
