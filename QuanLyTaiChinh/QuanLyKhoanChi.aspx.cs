using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace QuanLyTaiChinh

{
    public partial class QuanLyKhoanChi : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MaTaiKhoan"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadDanhMucChi();
                LoadKhoanChi();
            }
        }

        private void LoadDanhMucChi()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT MaDanhMucChi, TenDanhMucChi 
                               FROM DanhMucChi 
                               WHERE TrangThai = 1";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlDanhMuc.DataSource = dt;
                ddlDanhMuc.DataTextField = "TenDanhMucChi";
                ddlDanhMuc.DataValueField = "MaDanhMucChi";
                ddlDanhMuc.DataBind();
                ddlDanhMuc.Items.Insert(0, new ListItem("▼ Chọn danh mục", "0"));

                ddlDanhMucThem.DataSource = dt;
                ddlDanhMucThem.DataTextField = "TenDanhMucChi";
                ddlDanhMucThem.DataValueField = "MaDanhMucChi";
                ddlDanhMucThem.DataBind();
                ddlDanhMucThem.Items.Insert(0, new ListItem("▼ Chọn danh mục", "0"));

                ddlDanhMucSua.DataSource = dt;
                ddlDanhMucSua.DataTextField = "TenDanhMucChi";
                ddlDanhMucSua.DataValueField = "MaDanhMucChi";
                ddlDanhMucSua.DataBind();
                ddlDanhMucSua.Items.Insert(0, new ListItem("▼ Chọn danh mục", "0"));
            }
        }

        private void LoadKhoanChi()
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT 
                        kc.MaKhoanChi,
                        kc.TenKhoanChi,
                        kc.SoTien,
                        kc.NgayChi,
                        dm.TenDanhMucChi
                    FROM KhoanChi kc
                    INNER JOIN DanhMucChi dm 
                        ON kc.MaDanhMucChi = dm.MaDanhMucChi
                    WHERE kc.MaTaiKhoan = @MaTaiKhoan
                ";

                if (!string.IsNullOrEmpty(txtTuNgay.Text))
                    sql += " AND kc.NgayChi >= @TuNgay";

                if (!string.IsNullOrEmpty(txtDenNgay.Text))
                    sql += " AND kc.NgayChi <= @DenNgay";

                if (ddlDanhMuc.SelectedValue != "0")
                    sql += " AND kc.MaDanhMucChi = @MaDanhMucChi";

                sql += " ORDER BY kc.NgayChi DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                if (!string.IsNullOrEmpty(txtTuNgay.Text))
                    cmd.Parameters.AddWithValue("@TuNgay", txtTuNgay.Text);

                if (!string.IsNullOrEmpty(txtDenNgay.Text))
                    cmd.Parameters.AddWithValue("@DenNgay", txtDenNgay.Text);

                if (ddlDanhMuc.SelectedValue != "0")
                    cmd.Parameters.AddWithValue("@MaDanhMucChi", ddlDanhMuc.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvKhoanChi.DataSource = dt;
                gvKhoanChi.DataBind();

                decimal tongChi = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tongChi += Convert.ToDecimal(row["SoTien"]);
                }

                lblTongKhoanChi.Text = tongChi.ToString("#,##0") + " VNĐ";

                if (gvKhoanChi.PageCount == 0)
                    lblTrang.Text = "0/0";
                else
                    lblTrang.Text = (gvKhoanChi.PageIndex + 1) + "/" + gvKhoanChi.PageCount;
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            gvKhoanChi.PageIndex = 0;
            LoadKhoanChi();
        }

        protected void gvKhoanChi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvKhoanChi.PageIndex = e.NewPageIndex;
            LoadKhoanChi();
        }

        protected void btnThemKhoanChi_Click(object sender, EventArgs e)
        {
            lblLoiThem.Text = "";
            txtTenKhoanChiThem.Text = "";
            txtSoTienThem.Text = "";
            ddlDanhMucThem.SelectedValue = "0";

            pnlPopupThem.Visible = true;
        }

        protected void btnHuyThem_Click(object sender, EventArgs e)
        {
            pnlPopupThem.Visible = false;
        }

        protected void btnLuuThem_Click(object sender, EventArgs e)
        {
            decimal soTien;

            if (txtTenKhoanChiThem.Text.Trim() == "")
            {
                lblLoiThem.Text = "Vui lòng nhập tên khoản chi.";
                pnlPopupThem.Visible = true;
                return;
            }

            if (!decimal.TryParse(txtSoTienThem.Text.Trim(), out soTien) || soTien <= 0)
            {
                lblLoiThem.Text = "Số tiền không hợp lệ.";
                pnlPopupThem.Visible = true;
                return;
            }

            if (ddlDanhMucThem.SelectedValue == "0")
            {
                lblLoiThem.Text = "Vui lòng chọn danh mục.";
                pnlPopupThem.Visible = true;
                return;
            }
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    INSERT INTO KhoanChi
                    (MaTaiKhoan, MaDanhMucChi, TenKhoanChi, SoTien, NgayChi, MoTa)
                    VALUES
                    (@MaTaiKhoan, @MaDanhMucChi, @TenKhoanChi, @SoTien, GETDATE(), NULL)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                cmd.Parameters.AddWithValue("@MaDanhMucChi", ddlDanhMucThem.SelectedValue);
                cmd.Parameters.AddWithValue("@TenKhoanChi", txtTenKhoanChiThem.Text.Trim());
                cmd.Parameters.AddWithValue("@SoTien", soTien);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            pnlPopupThem.Visible = false;
            gvKhoanChi.PageIndex = 0;
            LoadKhoanChi();
        }

        protected void gvKhoanChi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int maKhoanChi = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "SuaKhoanChi")
            {
                MoPopupSua(maKhoanChi);
            }
            else if (e.CommandName == "XoaKhoanChi")
            {
                MoPopupXoa(maKhoanChi);
            }
        }

        private void MoPopupSua(int maKhoanChi)
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT MaKhoanChi, MaDanhMucChi, TenKhoanChi, SoTien
                    FROM KhoanChi
                    WHERE MaKhoanChi = @MaKhoanChi
                    AND MaTaiKhoan = @MaTaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoanChi", maKhoanChi);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    hfMaKhoanChiSua.Value = reader["MaKhoanChi"].ToString();
                    txtTenKhoanChiSua.Text = reader["TenKhoanChi"].ToString();
                    txtSoTienSua.Text = Convert.ToDecimal(reader["SoTien"]).ToString("0");
                    ddlDanhMucSua.SelectedValue = reader["MaDanhMucChi"].ToString();

                    lblLoiSua.Text = "";
                    pnlPopupSua.Visible = true;
                }
            }
        }

        protected void btnHuySua_Click(object sender, EventArgs e)
        {
            pnlPopupSua.Visible = false;
        }

        protected void btnLuuSua_Click(object sender, EventArgs e)
        {
            decimal soTien;

            if (txtTenKhoanChiSua.Text.Trim() == "")
            {
                lblLoiSua.Text = "Vui lòng nhập tên khoản chi.";
                pnlPopupSua.Visible = true;
                return;
            }

            if (!decimal.TryParse(txtSoTienSua.Text.Trim(), out soTien) || soTien <= 0)
            {
                lblLoiSua.Text = "Số tiền không hợp lệ.";
                pnlPopupSua.Visible = true;
                return;
            }

            if (ddlDanhMucSua.SelectedValue == "0")
            {
                lblLoiSua.Text = "Vui lòng chọn danh mục.";
                pnlPopupSua.Visible = true;
                return;
            }

            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);
            int maKhoanChi = Convert.ToInt32(hfMaKhoanChiSua.Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    UPDATE KhoanChi
                    SET 
                        TenKhoanChi = @TenKhoanChi,
                        SoTien = @SoTien,
                        MaDanhMucChi = @MaDanhMucChi
                    WHERE MaKhoanChi = @MaKhoanChi
                    AND MaTaiKhoan = @MaTaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenKhoanChi", txtTenKhoanChiSua.Text.Trim());
                cmd.Parameters.AddWithValue("@SoTien", soTien);
                cmd.Parameters.AddWithValue("@MaDanhMucChi", ddlDanhMucSua.SelectedValue);
                cmd.Parameters.AddWithValue("@MaKhoanChi", maKhoanChi);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            pnlPopupSua.Visible = false;
            LoadKhoanChi();
        }

        private void MoPopupXoa(int maKhoanChi)
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT MaKhoanChi, TenKhoanChi, SoTien
                    FROM KhoanChi
                    WHERE MaKhoanChi = @MaKhoanChi
                    AND MaTaiKhoan = @MaTaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoanChi", maKhoanChi);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    hfMaKhoanChiXoa.Value = reader["MaKhoanChi"].ToString();
                    lblTenKhoanChiXoa.Text = reader["TenKhoanChi"].ToString();
                    lblSoTienXoa.Text = Convert.ToDecimal(reader["SoTien"]).ToString("#,##0") + " VNĐ";

                    pnlPopupXoa.Visible = true;
                }
            }
        }

        protected void btnHuyXoa_Click(object sender, EventArgs e)
        {
            pnlPopupXoa.Visible = false;
        }

        protected void btnXacNhanXoa_Click(object sender, EventArgs e)
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);
            int maKhoanChi = Convert.ToInt32(hfMaKhoanChiXoa.Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    DELETE FROM KhoanChi
                    WHERE MaKhoanChi = @MaKhoanChi
                    AND MaTaiKhoan = @MaTaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoanChi", maKhoanChi);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            pnlPopupXoa.Visible = false;
            LoadKhoanChi();
        }
    }
}