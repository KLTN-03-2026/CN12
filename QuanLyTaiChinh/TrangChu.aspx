<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs" Inherits="QuanLyTaiChinh.TrangChu" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <style>
    .home-wrapper {
        height: 100vh;
        width: 100%;
        background-color: #f8f5f1;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: center;
        box-sizing: border-box;
        padding: 35px 30px 25px 30px;
        overflow: hidden;
    }

    .home-top {
        text-align: center;
        margin-top: 10px;
    }

    .home-title {
        font-size: 42px;
        font-weight: 700;
        color: #222;
        line-height: 1.3;
        margin-bottom: 12px;
    }

    .home-subtitle {
        font-size: 18px;
        color: #444;
        line-height: 1.6;
        margin-bottom: 25px;
    }

    .home-image {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .home-image img {
        width: 300px;
        max-width: 100%;
        height: auto;
        object-fit: contain;
    }

    .home-bottom {
        width: 100%;
        max-width: 900px;
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 50px;
        flex-wrap: wrap;
        margin-bottom: 10px;
    }

    .feature-item {
        display: flex;
        align-items: center;
        gap: 10px;
    }

    .feature-item img {
        width: 36px;
        height: 36px;
        object-fit: contain;
    }

    .feature-text {
        font-size: 16px;
        color: #333;
        font-weight: 500;
    }

    @media screen and (max-width: 768px) {
        .home-wrapper {
            padding: 20px 15px;
        }

        .home-title {
            font-size: 28px;
        }

        .home-subtitle {
            font-size: 14px;
        }

        .home-image img {
            width: 220px;
        }

        .home-bottom {
            gap: 20px;
            flex-direction: column;
        }
    }
</style>

    <div class="home-wrapper">

        <div class="home-top">
            <div class="home-title">
                Quản lý tài chính cá nhân<br />
                thông minh và hiệu quả
            </div>

            <div class="home-subtitle">
                Theo dõi thu nhập, chi tiêu và ngân sách<br />
                một lúc mọi nơi
            </div>

            <div class="home-image">
                <img src="Images/banner.jpg" alt="Tài chính cá nhân" />
            </div>
        </div>

        <div class="home-bottom">
            <div class="feature-item">
                <img src="Images/iconthuchi.jpg" alt="Quản lý thu chi" />
                <div class="feature-text">Quản lý thu chi dễ dàng</div>
            </div>

            <div class="feature-item">
                <img src="Images/iconbaocao.jpg" alt="Báo cáo trực quan" />
                <div class="feature-text">Báo cáo trực quan</div>
            </div>

            <div class="feature-item">
<img src="Images/iconbaomat.jpg" alt="An toàn" />
                <div class="feature-text">An toàn và bảo mật</div>
            </div>
        </div>

    </div>

</asp:Content>