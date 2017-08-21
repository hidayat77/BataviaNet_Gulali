<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_setup_payroll_lembur" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setup >> Payroll >> Rumus Lembur</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="setup" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <div class="container">
                <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <div class="btn-group pull-right m-t-15">
                            <asp:Literal ID="link_back" runat="server"></asp:Literal>
                        </div>
                        <h4 class="page-title">Rumus Lembur</h4>
                    </div>
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-lg-12">
                        <div id="basicwizard" class=" pull-in">
                            <ul>
                                <li class="">
                                    <asp:Literal ID="tab_komponen" runat="server"></asp:Literal>
                                </li>
                                <li class="">
                                    <asp:Literal ID="tab_ptkp" runat="server"></asp:Literal></li>
                                <li class="">
                                    <asp:Literal ID="tab_progressive_tax" runat="server"></asp:Literal></li>
                                <li class="">
                                    <asp:Literal ID="tab_parameter" runat="server"></asp:Literal></li>
                                <li class="active">
                                    <asp:Literal ID="tab_lembur" runat="server"></asp:Literal></li>
                            </ul>
                            <div class="card-box">
                                <div class="row">
                                    <div class="col-sm-6" style="padding-top: 20px;">
                                        <h4 class="header-title m-t-0 m-b-30">Lembur Pada Hari Kerja</h4>
                                        <asp:Literal ID="table_hari_kerja" runat="server" />
                                    </div>
                                    <div class="col-sm-6" style="padding-top: 20px;">
                                        <h4 class="header-title m-t-0 m-b-30">Lembur Pada Hari Libur</h4>
                                        <asp:Literal ID="table_hari_libur" runat="server" />
                                        <%--<asp:Literal ID="table_hari_libur_6H_kerja" runat="server" />
                                        <asp:Literal ID="table_hari_libur_terpendek" runat="server" />
                                        <asp:Literal ID="table_hari_libur_5H_kerja" runat="server" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
    </form>
</body>
<ucscript:script ID="script" runat="server" />
</html>
