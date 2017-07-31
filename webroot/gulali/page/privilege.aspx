<%@ Page Language="C#" AutoEventWireup="true" CodeFile="privilege.aspx.cs" Inherits="_notfound" %>

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
            <div class="text-error" style="padding-top: 80px;padding-bottom:20px;">
                <img src="/assets/images/icons/open-page.png" style="width: 200px;">
            </div>
            <p class="text-muted">
                Sorry You're Not Allowed To Open That Page,<br />
                Please Contact Your Web Administrator.
            </p>
            <br>
            <asp:Literal ID="link" runat="server"></asp:Literal>
        </div>
    </div>
    <!-- End wrapper page -->
</body>
<!-- end container -->
</html>

