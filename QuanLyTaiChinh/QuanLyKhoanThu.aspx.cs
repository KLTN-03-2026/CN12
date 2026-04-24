using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace QuanLyTaiChinh
{
    public partial class QuanLyKhoanThu : System.Web.UI.Page
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
                LoadDanhMucThu();
                LoadKhoanThu();
            }
        }

        private void LoadDanhMucThu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT MaDanhMucThu, TenDanhMucThu 
                               FROM DanhMucThu 
                               WHERE TrangThai = 1";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlDanhMuc.DataSource = dt;
                ddlDanhMuc.DataTextField = "TenDanhMucThu";
                ddlDanhMuc.DataValueField = "MaDanhMucThu";
                ddlDanhMuc.DataBind();
                ddlDanhMuc.Items.Insert(0, new ListItem("▼ Chọn danh mục", "0"));

                ddlDanhMucThem.DataSource = dt;
                ddlDanhMucThem.DataTextField = "TenDanhMucThu";
                ddlDanhMucThem.DataValueField = "MaDanhMucThu";
                ddlDanhMucThem.DataBind();
                ddlDanhMucThem.Items.Insert(0, new ListItem("▼ Chọn danh mục", "0"));

                ddlDanhMucSua.DataSource = dt;
                ddlDanhMucSua.DataTextField = "TenDanhMucThu";
                ddlDanhMucSua.DataValueField = "MaDanhMucThu";
                ddlDanhMucSua.DataBind();
                ddlDanhMucSua.Items.Insert(0, new ListItem("▼ Chọn danh mục", "0"));
            }
        }

        private void LoadKhoanThu()
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT 
                        kt.MaKhoanThu,
                        kt.TenKhoanThu,
                        kt.SoTien,
                        kt.NgayThu,
                        dm.TenDanhMucThu
                    FROM KhoanThu kt
                    INNER JOIN DanhMucThu dm 
                        ON kt.MaDanhMucThu = dm.MaDanhMucThu
                    WHERE kt.MaTaiKhoan = @MaTaiKhoan
                ";

                if (!string.IsNullOrEmpty(txtTuNgay.Text))
                    sql += " AND kt.NgayThu >= @TuNgay";

                if (!string.IsNullOrEmpty(txtDenNgay.Text))
                    sql += " AND kt.NgayThu <= @DenNgay";

                if (ddlDanhMuc.SelectedValue != "0")
                    sql += " AND kt.MaDanhMucThu = @MaDanhMucThu";

                sql += " ORDER BY kt.NgayThu DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                if (!string.IsNullOrEmpty(txtTuNgay.Text))
                    cmd.Parameters.AddWithValue("@TuNgay", txtTuNgay.Text);

                if (!string.IsNullOrEmpty(txtDenNgay.Text))
                    cmd.Parameters.AddWithValue("@DenNgay", txtDenNgay.Text);

                if (ddlDanhMuc.SelectedValue != "0")
                    cmd.Parameters.AddWithValue("@MaDanhMucThu", ddlDanhMuc.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvKhoanThu.DataSource = dt;
                gvKhoanThu.DataBind();

                decimal tongThu = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tongThu += Convert.ToDecimal(row["SoTien"]);
                }

                lblTongKhoanThu.Text = tongThu.ToString("#,##0") + " VNĐ";

                if (gvKhoanThu.PageCount == 0)
                    lblTrang.Text = "0/0";
                else
                    lblTrang.Text = (gvKhoanThu.PageIndex + 1) + "/" + gvKhoanThu.PageCount;
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            gvKhoanThu.PageIndex = 0;
            LoadKhoanThu();
        }

        protected void gvKhoanThu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvKhoanThu.PageIndex = e.NewPageIndex;
            LoadKhoanThu();
        }

        protected void btnThemKhoanThu_Click(object sender, EventArgs e)
        {
            lblLoiThem.Text = "";
            txtTenKhoanThuThem.Text = "";
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

            if (txtTenKhoanThuThem.Text.Trim() == "")
            {
                lblLoiThem.Text = "Vui lòng nhập tên khoản thu.";
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
                    INSERT INTO KhoanThu
                    (MaTaiKhoan, MaDanhMucThu, TenKhoanThu, SoTien, NgayThu, MoTa)
                    VALUES
                    (@MaTaiKhoan, @MaDanhMucThu, @TenKhoanThu, @SoTien, GETDATE(), NULL)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                cmd.Parameters.AddWithValue("@MaDanhMucThu", ddlDanhMucThem.SelectedValue);
                cmd.Parameters.AddWithValue("@TenKhoanThu", txtTenKhoanThuThem.Text.Trim());
                cmd.Parameters.AddWithValue("@SoTien", soTien);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            pnlPopupThem.Visible = false;
            gvKhoanThu.PageIndex = 0;
            LoadKhoanThu();
        }

        protected void gvKhoanThu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int maKhoanThu = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "SuaKhoanThu")
            {
                MoPopupSua(maKhoanThu);
            }
            else if (e.CommandName == "XoaKhoanThu")
            {
                MoPopupXoa(maKhoanThu);
            }
        }

        private void MoPopupSua(int maKhoanThu)
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT MaKhoanThu, MaDanhMucThu, TenKhoanThu, SoTien
                    FROM KhoanThu
                    WHERE MaKhoanThu = @MaKhoanThu
                    AND MaTaiKhoan = @MaTaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoanThu", maKhoanThu);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    hfMaKhoanThuSua.Value = reader["MaKhoanThu"].ToString();
                    txtTenKhoanThuSua.Text = reader["TenKhoanThu"].ToString();
                    txtSoTienSua.Text = Convert.ToDecimal(reader["SoTien"]).ToString("0");

                    ddlDanhMucSua.SelectedValue = reader["MaDanhMucThu"].ToString();

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

            if (txtTenKhoanThuSua.Text.Trim() == "")
            {
                lblLoiSua.Text = "Vui lòng nhập tên khoản thu.";
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
            int maKhoanThu = Convert.ToInt32(hfMaKhoanThuSua.Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    UPDATE KhoanThu
                    SET 
                        TenKhoanThu = @TenKhoanThu,
                        SoTien = @SoTien,
                        MaDanhMucThu = @MaDanhMucThu
                    WHERE MaKhoanThu = @MaKhoanThu
                    AND MaTaiKhoan = @MaTaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenKhoanThu", txtTenKhoanThuSua.Text.Trim());
                cmd.Parameters.AddWithValue("@SoTien", soTien);
                cmd.Parameters.AddWithValue("@MaDanhMucThu", ddlDanhMucSua.SelectedValue);
                cmd.Parameters.AddWithValue("@MaKhoanThu", maKhoanThu);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            pnlPopupSua.Visible = false;
            LoadKhoanThu();
        }

        private void MoPopupXoa(int maKhoanThu)
        {
            int maTaiKhoan = Convert.ToInt32(Session["MaTaiKhoan"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT MaKhoanThu, TenKhoanThu, SoTien
                    FROM KhoanThu
                    WHERE MaKhoanThu = @MaKhoanThu
                    AND MaTaiKhoan = @MaTaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoanThu", maKhoanThu);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    hfMaKhoanThuXoa.Value = reader["MaKhoanThu"].ToString();
                    lblTenKhoanThuXoa.Text = reader["TenKhoanThu"].ToString();
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
            int maKhoanThu = Convert.ToInt32(hfMaKhoanThuXoa.Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    DELETE FROM KhoanThu
                    WHERE MaKhoanThu = @MaKhoanThu
                    AND MaTaiKhoan = @MaTaiKhoan";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKhoanThu", maKhoanThu);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            pnlPopupXoa.Visible = false;
            LoadKhoanThu();
        }
    }
}
