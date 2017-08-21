<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_user_pinjaman" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Gulali HRIS - User - Pinjaman</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="personal_data" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <div class="container">
                <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="page-title">Pinjaman</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div id="basicwizard" class=" pull-in">
                                <!-- tab Data Pribadi/Kontrak/Pinjaman/Surat Peringatan/Exit Clearance -->
                                <ul>
                                    <li class="">
                                        <asp:Literal ID="tab_data_pribadi" runat="server"></asp:Literal>
                                    </li>
                                    <li class="">
                                        <asp:Literal ID="tab_kontrak" runat="server"></asp:Literal></li>
                                    <li class="">
                                        <asp:Literal ID="tab_surat_peringatan" runat="server"></asp:Literal></li>
                                    <li class="active">
                                        <asp:Literal ID="tab_pinjaman" runat="server"></asp:Literal></li>
                                    <li class="">
                                        <asp:Literal ID="tab_exit_clearance" runat="server"></asp:Literal></li>
                                </ul>
                                <div class="form-horizontal" role="form" runat="server">
                                    <div class="card-box">
                                        <h4 class="header-title m-t-0 m-b-30">Pinjaman</h4>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    test
                                                </div>
                                            </div>
                                            <!-- end row -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
                <ucfooter:footer ID="footer" runat="server" />
            </div>
            <!-- end row -->
        </div>
    </form>
</body>
<ucscript:script id="script" runat="server" />
</html>
