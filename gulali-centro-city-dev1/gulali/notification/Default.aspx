<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_notifikasi" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Notification Page</title>
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
            <div class="row">
                <div class="col-sm-12">
                    <div class="inbox-app-main">
                        <div class="row">
                            <div class="col-md-3">
                                <aside id="sidebar" class="nano">
                                    <div class="nano-content">
                                        <menu class="menu-segment">
                                            <ul class="list-unstyled">
                                                <asp:Literal ID="inbox_left_menu" runat="server" />
                                            </ul>
                                        </menu>
                                        <div class="separator"></div>
                                        <div class="text-center">
                                            <asp:Literal ID="trash_left_menu" runat="server" />
                                        </div>
                                    </div>
                                </aside>
                            </div>
                            <!-- end col -->

                            <div class="col-md-9">
                                <main id="main">
                                    <div id="main-nano-wrapper" class="nano">
                                        <div class="nano-content">
                                            <ul class="message-list">
                                                <asp:Literal ID="notifikasi" runat="server" />
                                            </ul>
                                        </div>
                                    </div>
                                </main>
                            </div>
                            <!-- end col -->
                        </div>
                        <!-- end row -->
                    </div>
                </div>
            </div>
            <!-- End row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>
<!-- end container -->
</html>

