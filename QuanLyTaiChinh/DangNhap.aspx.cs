using System;
using System.Configuration;
using System.Data.SqlClient;

namespace QuanLyTaiChinh
{
    public partial class DangNhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (email == "" || matKhau == "")
            {
                lblThongBao.Text = "Vui lòng nhập đầy đủ email và mật khẩu.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT tk.MaTaiKhoan, vt.TenVaiTro
                    FROM TaiKhoan tk
                    INNER JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
                    WHERE tk.Email = @Email
                      AND tk.MatKhau = @MatKhau
                      AND tk.TrangThai = 1";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Session["MaTaiKhoan"] = dr["MaTaiKhoan"].ToString();
                    Session["Email"] = email;
                    Session["VaiTro"] = dr["TenVaiTro"].ToString();

                    Response.Redirect("TrangChu.aspx");
                }
                else
                {
                    lblThongBao.Text = "Email hoặc mật khẩu không đúng.";
                }
            }
        }
    }
}