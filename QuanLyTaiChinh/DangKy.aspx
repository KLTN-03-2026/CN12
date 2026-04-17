<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="QuanLyTaiChinh.DangKy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .register-page {
            height: 100%;
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background: #f8f5f1;
            box-sizing: border-box;
            padding: 20px;
            overflow: hidden;
        }

        .register-box {
            width: 960px;
            max-width: 100%;
            min-height: 560px;
            background: #ffffff;
            border: 1px solid #e0d8cd;
            box-shadow: 0 2px 10px rgba(0,0,0,0.06);
            display: flex;
            overflow: hidden;
        }

        .register-left {
            width: 48%;
            background: #f4efe7;
            padding: 34px 28px 20px 28px;
            box-sizing: border-box;
            border-right: 1px solid #e7ddd0;
        }

        .register-right {
            width: 52%;
            background: #f8f5f1;
            padding: 20px 28px 24px 28px;
            box-sizing: border-box;
        }

        .left-title {
            text-align: center;
            font-size: 22px;
            line-height: 1.5;
            font-weight: 700;
            color: #222;
            margin-bottom: 32px;
        }

        .feature-list {
            margin-bottom: 28px;
        }

        .feature-item {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-bottom: 34px;
            font-size: 16px;
            color: #333;
        }

        .tick-icon {
            width: 20px;
            height: 20px;
            object-fit: contain;
            flex-shrink: 0;
        }

        .left-image {
            text-align: center;
            margin-top: 10px;
        }

        .left-image img {
            max-width: 92%;
            max-height: 270px;
            height: auto;
            object-fit: contain;
        }

        .register-title {
            font-size: 22px;
            font-weight: 700;
            color: #222;
            margin-bottom: 14px;
        }

        .form-group {
            margin-bottom: 22px;
        }

        .form-group label {
            display: block;
            margin-bottom: 8px;
            font-size: 15px;
            font-weight: 600;
            color: #444;
        }

        .input-box {
            width: 100%;
            height: 40px;
            padding: 8px 10px;
            border: 1px solid #d7d7d7;
            box-sizing: border-box;
            font-size: 15px;
            border-radius: 4px;
            background: #fff;
            outline: none;
        }

        .input-box:focus {
            border-color: #d89c1d;
            box-shadow: 0 0 3px rgba(216, 156, 29, 0.30);
        }

        .btn-register {
            width: 100%;
            height: 46px;
            background: #f2a300;
            color: white;
border: none;
            font-size: 18px;
            font-weight: 700;
            cursor: pointer;
            border-radius: 2px;
            margin-top: 6px;
        }

        .btn-register:hover {
            background: #df9500;
        }

        .login-link {
            text-align: center;
            margin-top: 18px;
            font-size: 14px;
            color: #444;
        }

        .login-link a {
            color: #f2a300;
            text-decoration: none;
            font-weight: 600;
        }

        .login-link a:hover {
            text-decoration: underline;
        }

        .error-message {
            color: red;
            font-size: 14px;
            margin-top: 10px;
            text-align: center;
            min-height: 20px;
        }

        .success-message {
            color: green;
        }

        @media screen and (max-width: 768px) {
            .register-box {
                flex-direction: column;
                min-height: auto;
            }

            .register-left,
            .register-right {
                width: 100%;
            }

            .register-left {
                padding: 24px 20px;
                border-right: none;
                border-bottom: 1px solid #e7ddd0;
            }

            .register-right {
                padding: 24px 20px;
            }

            .left-image img {
                max-height: 220px;
            }

            .feature-item {
                margin-bottom: 20px;
            }
        }
        .feature-item {
    display: flex;
    align-items: center;
    gap: 12px;
    margin-bottom: 30px;
    font-size: 16px;
    color: #333;
}

.tick {
    color: #6fbe44;       /* xanh giống hình */
    font-size: 22px;
    font-weight: bold;
    width: 20px;
    text-align: center;
}
    </style>

    <div class="register-page">
        <div class="register-box">

            <!-- Bên trái -->
            <div class="register-left">
                <div class="left-title">
                    Tạo tài khoản để bắt<br />
                    đầu quản lý tài chính thông minh
                </div>

                <div class="feature-item">
    <span class="tick">✓</span>
    <span>Miễn phí và dễ sử dụng</span>
</div>

<div class="feature-item">
    <span class="tick">✓</span>
    <span>An toàn tuyệt đối</span>
</div>

<div class="feature-item">
    <span class="tick">✓</span>
    <span>Quản lý mọi lúc mọi nơi</span>
</div>

<div class="feature-item">
    <span class="tick">✓</span>
    <span>Đồng hành cùng mục tiêu</span>
</div>

                <div class="left-image">
                    <img src="Images/anhdangky.jpg" alt="Đăng ký FinTrack" />
                </div>
            </div>

            <!-- Bên phải -->
            <div class="register-right">
                <div class="register-title">Đăng ký</div>

                <div class="form-group">
<label for="txtHoTen">Họ và tên</label>
                    <asp:TextBox ID="txtHoTen" runat="server" CssClass="input-box"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtEmail">Địa chỉ email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="input-box"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtSoDienThoai">Số điện thoại</label>
                    <asp:TextBox ID="txtSoDienThoai" runat="server" CssClass="input-box"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtMatKhau">Mật khẩu</label>
                    <asp:TextBox ID="txtMatKhau" runat="server" CssClass="input-box" TextMode="Password"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtXacNhanMatKhau">Xác nhận mật khẩu</label>
                    <asp:TextBox ID="txtXacNhanMatKhau" runat="server" CssClass="input-box" TextMode="Password"></asp:TextBox>
                </div>

                <asp:Button ID="btnDangKy" runat="server" Text="Đăng ký" CssClass="btn-register" OnClick="btnDangKy_Click" />

                <div class="login-link">
                    Đã có tài khoản?
                    <a href="DangNhap.aspx">Đăng nhập</a>
                </div>

                <div class="error-message">
                    <asp:Label ID="lblThongBao" runat="server"></asp:Label>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
