<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="QuanLyTaiChinh.DangNhap" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .login-page {
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

        .login-box {
            width: 900px;
            max-width: 100%;
            min-height: 420px;
            background: #ffffff;
            border: 1px solid #ddd;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            display: flex;
            overflow: hidden;
        }

        .login-left {
            width: 48%;
            background: #f3ede4;
            padding: 35px 30px;
            box-sizing: border-box;
        }

        .login-left h2 {
            margin: 0 0 10px 0;
            font-size: 22px;
            color: #333;
            font-weight: 700;
        }

        .login-left p {
            margin: 0 0 28px 0;
            color: #777;
            font-size: 16px;
        }

        .feature-list {
            margin-top: 10px;
        }

        .feature-item {
            font-size: 18px;
            color: #666;
            margin-bottom: 22px;
            display: flex;
            align-items: center;
        }

        .feature-item .tick {
            color: #67b82f;
            font-weight: bold;
            font-size: 22px;
            margin-right: 12px;
        }

        .login-right {
            width: 52%;
            padding: 28px 35px;
            box-sizing: border-box;
            background: #fff;
        }

        .login-title {
            font-size: 22px;
            font-weight: 700;
            color: #333;
            margin-bottom: 10px;
        }

        .login-desc {
            font-size: 16px;
            color: #555;
            margin-bottom: 18px;
        }

        .form-group {
            margin-bottom: 16px;
        }

        .form-group label {
            display: block;
            margin-bottom: 6px;
            font-size: 15px;
            font-weight: 600;
            color: #444;
        }

        .input-box {
            width: 100%;
            height: 38px;
            padding: 8px 10px;
            border: 1px solid #ccc;
            box-sizing: border-box;
            font-size: 15px;
            border-radius: 2px;
            outline: none;
        }

        .input-box:focus {
            border-color: #d89c1d;
            box-shadow: 0 0 3px rgba(216, 156, 29, 0.35);
        }

        .forgot-password {
            text-align: center;
            margin: 8px 0 16px 0;
        }

        .forgot-password a {
            color: #666;
            font-size: 14px;
            text-decoration: none;
        }

        .forgot-password a:hover {
            color: #d89c1d;
        }

        .btn-login {
width: 100%;
            height: 46px;
            background: #f2a300;
            color: white;
            border: none;
            font-size: 18px;
            font-weight: 700;
            cursor: pointer;
            border-radius: 3px;
        }

        .btn-login:hover {
            background: #df9500;
        }

        .register-link {
            text-align: center;
            margin-top: 18px;
            font-size: 15px;
            color: #444;
        }

        .register-link a {
            color: #f2a300;
            text-decoration: none;
            font-weight: 600;
        }

        .register-link a:hover {
            text-decoration: underline;
        }

        .error-message {
            color: red;
            font-size: 14px;
            margin-top: 10px;
            text-align: center;
        }

        @media screen and (max-width: 768px) {
            .login-box {
                flex-direction: column;
                min-height: auto;
            }

            .login-left,
            .login-right {
                width: 100%;
            }

            .login-left {
                padding: 25px 20px;
            }

            .login-right {
                padding: 25px 20px;
            }
        }
    </style>

    <div class="login-page">
        <div class="login-box">

            <!-- Bên trái -->
            <div class="login-left">
                <h2>Chào mừng bạn đến với FinTrack</h2>
                <p>Quản lý tài chính cá nhân dễ dàng hơn</p>

                <div class="feature-list">
                    <div class="feature-item">
                        <span class="tick">✓</span>
                        <span>Theo dõi chi tiêu</span>
                    </div>

                    <div class="feature-item">
                        <span class="tick">✓</span>
                        <span>Lập kế hoạch tài chính</span>
                    </div>

                    <div class="feature-item">
                        <span class="tick">✓</span>
                        <span>Quản lý thu nhập</span>
                    </div>

                    <div class="feature-item">
                        <span class="tick">✓</span>
                        <span>Báo cáo trực quan</span>
                    </div>
                </div>
            </div>

            <!-- Bên phải -->
            <div class="login-right">
                <div class="login-title">Đăng nhập</div>
                <div class="login-desc">Vui lòng nhập thông tin đăng nhập của bạn</div>

                <div class="form-group">
                    <label for="txtEmail">Địa chỉ email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="input-box"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtMatKhau">Mật khẩu</label>
<asp:TextBox ID="txtMatKhau" runat="server" CssClass="input-box" TextMode="Password"></asp:TextBox>
                </div>

                <div class="forgot-password">
                    <a href="QuenMatKhau.aspx">Quên mật khẩu?</a>
                </div>

                <asp:Button ID="btnDangNhap" runat="server" Text="Đăng nhập" CssClass="btn-login" OnClick="btnDangNhap_Click" />

                <div class="register-link">
                    Bạn chưa có tài khoản?
                    <a href="DangKy.aspx">Đăng ký</a>
                </div>

                <div class="error-message">
                    <asp:Label ID="lblThongBao" runat="server"></asp:Label>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
