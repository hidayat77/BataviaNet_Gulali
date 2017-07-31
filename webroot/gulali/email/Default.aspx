<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_user_kalender_cuti_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Gulali HRIS - Kalender >> Cuti</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="dashboard" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title">Cek send email</h4>
                </div>
            </div>
            <div class="row">
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="sm" runat="server" />
                    <div class="col-sm-8">
                        <asp:Label ID="note" runat="server"></asp:Label>
                        <div class="card-box">
                            <div class="row">
                                <div>
                                    <asp:Button ID="SendEmail" runat="server" Style="border: 1px solid #10c469 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #10c469 !important; border-radius: 2px; padding: 6px 14px;" Text="Send Email" name="SendEmail" OnClick="BtnSendEmail" AccessKey="s" />
                                </div>
                            </div>
                            <!-- end row -->
                        </div>
                    </div>

                </form>
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>
</html>
