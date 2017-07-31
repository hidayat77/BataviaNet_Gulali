<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_notfound" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Not Found</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="account-pages"></div>
    <div class="clearfix"></div>
    <div class="wrapper-page">
        <div class="ex-page-content text-center">
            <div class="text-error" style="font-size: 160px; padding-top: 80px;">404</div>
            <h3 class="text-uppercase font-600">Halaman Tidak Ditemukan</h3>
            <p class="text-muted">
                Mohon maaf, Halaman yang Anda cari tidak ditemukan!<br />
                Kemungkinan halaman telah dihapus atau Anda salah menulisakan URL.
            </p>
            <br>
            <asp:Literal ID="link" runat="server"></asp:Literal>
        </div>
    </div>
    <!-- End wrapper page -->
</body>
<!-- end container -->
</html>

