<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_profile" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Profil Page</title>
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
                    <h4 class="page-title">Profil</h4>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-8">
                    <div class="bg-picture card-box">
                        <div class="profile-info-name">
                            <asp:Image ID="Image1" class="img-thumbnail" runat="server" />
                            <div class="profile-info-detail" style="padding-bottom: 50px;">
                                <div style="float: left; width: 30%;">
                                    <h4 class="text-muted m-b-5 font-13 font-bold">Username</h4>
                                    <h4 class="text-muted m-b-5 font-13 font-bold">Nama</h4>
                                    <h4 class="text-muted m-b-5 font-13 font-bold">HP</h4>
                                </div>
                                <div style="float: right; width: 70%;">
                                    <h4 class="text-muted m-b-5 font-13">:
                                        <asp:Literal ID="user_name" runat="server"></asp:Literal></h4>
                                    <h4 class="text-muted m-b-5 font-13">:
                                        <asp:Literal ID="full_name" runat="server"></asp:Literal></h4>
                                    <h4 class="text-muted m-b-5 font-13">:
                                        <asp:Literal ID="phone_number" runat="server"></asp:Literal></h4>
                                </div>
                                <div style="text-align: center;">
                                    <a href="edit/">
                                        <button type="button" class="btn btn-custom btn-rounded waves-effect waves-light w-md m-b-5 waves-input-wrapper">Update Profil</button></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
                <div class="col-sm-4">
                    <div class="card-box" style="overflow: auto">
                        <h4 class="header-title m-t-0 m-b-30">My Team Members</h4>
                        <div style="width: 100%;">
                            <ul class="list-group m-b-0 user-list">
                                <asp:Literal ID="isi" runat="server" />
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>
            <!-- end row -->
            <ucfooter:footer id="footer" runat="server" />
        </div>
    </div>
</body>
<!-- end container -->
</html>

