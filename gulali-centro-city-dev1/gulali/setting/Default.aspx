<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Setting" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Dashboard Page</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title">Setting</h4>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/menu.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="menu/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Setting Menu</a>
                    </div>
                </div>
            </div>
            <ucfooter:footer id="footer" runat="server" />
        </div>
    </div>
</body>
</html>
