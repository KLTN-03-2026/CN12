<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuanLyKhoanChi.aspx.cs" Inherits="QuanLyTaiChinh.QuanLyKhoanChi" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style>
.expense-page {
    background: #ffffff;
    min-height: 100vh;
    padding: 20px 25px;
    font-family: Arial, sans-serif;
    box-sizing: border-box;
}

.expense-card {
    background: #ffffff;
    width: 100%;
    min-height: 620px;
    box-sizing: border-box;
}

.expense-header {
    display: grid;
    grid-template-columns: 1fr auto 1fr;
    align-items: start;
    margin-bottom: 35px;
}

.expense-title {
    font-size: 22px;
    font-weight: bold;
    color: #111;
    justify-self: start;
}

.total-money {
    font-size: 16px;
    font-weight: bold;
    color: #111;
    justify-self: center;
    margin-top: 10px;
}

.btn-add {
    background: #e87522;
    color: white;
    border: none;
    padding: 14px 28px;
    font-weight: bold;
    cursor: pointer;
    justify-self: end;
}

.filter-box {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 14px;
    margin-bottom: 18px;
}

.input-date,
.select-category {
    height: 38px;
    border: 1px solid #999;
    border-radius: 6px;
    padding: 0 10px;
    background: white;
    font-weight: bold;
}

.btn-search {
    background: #21b34b;
    color: white;
    border: none;
    height: 42px;
    padding: 0 30px;
    font-weight: bold;
    cursor: pointer;
}

.table-box {
    width: 100%;
    min-height: 430px;
    border: 1px solid #777;
    background: #f6eee7;
    position: relative;
    padding-bottom: 45px;
    box-sizing: border-box;
}

.expense-grid {
    width: 100%;
    border-collapse: collapse;
    font-size: 15px;
    background: #f6eee7;
}

.expense-grid th {
    padding: 16px 10px;
    text-align: center;
    font-weight: normal;
    background: #f6eee7;
    border-bottom: 1px solid #a89f96;
}

.expense-grid td {
    padding: 18px 10px;
    text-align: center;
    background: #f6eee7;
    border-bottom: 1px solid #cfc6bd;
}

.expense-grid tr:last-child td {
    border-bottom: none;
}

.stt {
    font-weight: bold;
}

.btn-edit {
    background: #27a9e1;
    color: black;
    padding: 9px 18px;
    border: none;
    font-weight: bold;
    cursor: pointer;
    margin-right: 8px;
}

.btn-delete {
    background: #ef4136;
    color: black;
    padding: 9px 18px;
    border: none;
    font-weight: bold;
    cursor: pointer;
}

.pager-box {
    position: absolute;
    right: 25px;
    bottom: 15px;
    font-size: 15px;
    color: #111;
}

/* POPUP */
.popup-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0,0,0,0.12);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 9999;
}

.popup-box {
    width: 370px;
    background: #f6eee7;
    border: 1px solid #888;
padding: 25px 35px;
    box-sizing: border-box;
}

.popup-title {
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 25px;
}

.popup-label {
    font-weight: bold;
    margin-bottom: 8px;
    display: block;
}

.popup-input,
.popup-select {
    width: 100%;
    height: 38px;
    border: 1px solid #999;
    margin-bottom: 22px;
    padding: 0 8px;
    box-sizing: border-box;
    background: white;
}

.popup-button-box {
    display: flex;
    justify-content: center;
    gap: 35px;
    margin-top: 10px;
}

.btn-save,
.btn-cancel,
.btn-confirm-delete {
    background: #dca13a;
    border: none;
    padding: 9px 28px;
    cursor: pointer;
    font-weight: bold;
}

.delete-box {
    width: 430px;
    background: #f6eee7;
    border: 1px solid #777;
    padding: 30px;
    box-sizing: border-box;
    text-align: center;
}

.delete-title {
    font-size: 24px;
    font-weight: bold;
    margin-bottom: 30px;
}

.delete-message {
    font-size: 16px;
    line-height: 1.5;
    margin-bottom: 35px;
}

.message-error {
    color: red;
    font-weight: bold;
    margin-bottom: 10px;
}
</style>

<div class="expense-page">
    <div class="expense-card">

        <div class="expense-header">
            <div class="expense-title">Quản lý khoản chi</div>

            <div class="total-money">
                Tổng khoản chi:
                <asp:Label ID="lblTongKhoanChi" runat="server" Text="0 VNĐ"></asp:Label>
            </div>

            <asp:Button ID="btnThemKhoanChi" runat="server"
                Text="Thêm khoản chi"
                CssClass="btn-add"
                OnClick="btnThemKhoanChi_Click" />
        </div>

        <div class="filter-box">
            <asp:TextBox ID="txtTuNgay" runat="server"
                TextMode="Date"
                CssClass="input-date"></asp:TextBox>

            <b>đến</b>

            <asp:TextBox ID="txtDenNgay" runat="server"
                TextMode="Date"
                CssClass="input-date"></asp:TextBox>

            <asp:DropDownList ID="ddlDanhMuc" runat="server"
                CssClass="select-category">
            </asp:DropDownList>

            <asp:Button ID="btnTimKiem" runat="server"
                Text="Tìm kiếm"
                CssClass="btn-search"
                OnClick="btnTimKiem_Click" />
        </div>

        <div class="table-box">
            <asp:GridView ID="gvKhoanChi" runat="server"
                AutoGenerateColumns="False"
                CssClass="expense-grid"
                GridLines="None"
                AllowPaging="True"
                PageSize="8"
                OnPageIndexChanging="gvKhoanChi_PageIndexChanging"
                OnRowCommand="gvKhoanChi_RowCommand">

                <Columns>
                    <asp:TemplateField HeaderText="STT">
                        <ItemTemplate>
                            <span class="stt">
<%# Container.DataItemIndex + 1 + (gvKhoanChi.PageIndex * gvKhoanChi.PageSize) %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TenKhoanChi" HeaderText="Tên" />

                    <asp:BoundField DataField="SoTien" HeaderText="Số tiền"
                        DataFormatString="{0:#,##0 VNĐ}" />

                    <asp:BoundField DataField="TenDanhMucChi" HeaderText="Danh mục" />

                    <asp:BoundField DataField="NgayChi" HeaderText="Ngày"
                        DataFormatString="{0:dd/MM/yyyy}" />

                    <asp:TemplateField HeaderText="Hành động">
                        <ItemTemplate>
                            <asp:Button ID="btnSua" runat="server"
                                Text="Sửa"
                                CssClass="btn-edit"
                                CommandName="SuaKhoanChi"
                                CommandArgument='<%# Eval("MaKhoanChi") %>' />

                            <asp:Button ID="btnXoa" runat="server"
                                Text="Xóa"
                                CssClass="btn-delete"
                                CommandName="XoaKhoanChi"
                                CommandArgument='<%# Eval("MaKhoanChi") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="pager-box">
                Trang <asp:Label ID="lblTrang" runat="server"></asp:Label>
            </div>
        </div>

    </div>
</div>

<!-- POPUP THÊM -->
<asp:Panel ID="pnlPopupThem" runat="server" CssClass="popup-overlay" Visible="false">
    <div class="popup-box">
        <div class="popup-title">Thêm khoản chi</div>

        <asp:Label ID="lblLoiThem" runat="server" CssClass="message-error"></asp:Label>

        <asp:Label runat="server" Text="Tên khoản chi" CssClass="popup-label"></asp:Label>
        <asp:TextBox ID="txtTenKhoanChiThem" runat="server" CssClass="popup-input"></asp:TextBox>

        <asp:Label runat="server" Text="Số tiền" CssClass="popup-label"></asp:Label>
        <asp:TextBox ID="txtSoTienThem" runat="server" CssClass="popup-input"></asp:TextBox>

        <asp:Label runat="server" Text="Danh mục" CssClass="popup-label"></asp:Label>
        <asp:DropDownList ID="ddlDanhMucThem" runat="server" CssClass="popup-select"></asp:DropDownList>

        <div class="popup-button-box">
            <asp:Button ID="btnLuuThem" runat="server" Text="Lưu"
                CssClass="btn-save" OnClick="btnLuuThem_Click" />

            <asp:Button ID="btnHuyThem" runat="server" Text="Hủy"
                CssClass="btn-cancel" OnClick="btnHuyThem_Click" />
        </div>
    </div>
</asp:Panel>

<!-- POPUP SỬA -->
<asp:Panel ID="pnlPopupSua" runat="server" CssClass="popup-overlay" Visible="false">
<div class="popup-box">
        <div class="popup-title">Sửa khoản chi</div>

        <asp:HiddenField ID="hfMaKhoanChiSua" runat="server" />
        <asp:Label ID="lblLoiSua" runat="server" CssClass="message-error"></asp:Label>

        <asp:Label runat="server" Text="Tên khoản chi" CssClass="popup-label"></asp:Label>
        <asp:TextBox ID="txtTenKhoanChiSua" runat="server" CssClass="popup-input"></asp:TextBox>

        <asp:Label runat="server" Text="Số tiền" CssClass="popup-label"></asp:Label>
        <asp:TextBox ID="txtSoTienSua" runat="server" CssClass="popup-input"></asp:TextBox>

        <asp:Label runat="server" Text="Danh mục" CssClass="popup-label"></asp:Label>
        <asp:DropDownList ID="ddlDanhMucSua" runat="server" CssClass="popup-select"></asp:DropDownList>

        <div class="popup-button-box">
            <asp:Button ID="btnLuuSua" runat="server" Text="Lưu"
                CssClass="btn-save" OnClick="btnLuuSua_Click" />

            <asp:Button ID="btnHuySua" runat="server" Text="Hủy"
                CssClass="btn-cancel" OnClick="btnHuySua_Click" />
        </div>
    </div>
</asp:Panel>

<!-- POPUP XÓA -->
<asp:Panel ID="pnlPopupXoa" runat="server" CssClass="popup-overlay" Visible="false">
    <div class="delete-box">
        <div class="delete-title">Xác nhận xóa</div>

        <asp:HiddenField ID="hfMaKhoanChiXoa" runat="server" />

        <div class="delete-message">
            Bạn có chắc muốn xóa khoản chi<br />
            “<asp:Label ID="lblTenKhoanChiXoa" runat="server"></asp:Label>
            - <asp:Label ID="lblSoTienXoa" runat="server"></asp:Label>” không?
        </div>

        <div class="popup-button-box">
            <asp:Button ID="btnHuyXoa" runat="server"
                Text="Hủy"
                CssClass="btn-cancel"
                OnClick="btnHuyXoa_Click" />

            <asp:Button ID="btnXacNhanXoa" runat="server"
                Text="Xóa"
                CssClass="btn-confirm-delete"
                OnClick="btnXacNhanXoa_Click" />
        </div>
    </div>
</asp:Panel>

</asp:Content>