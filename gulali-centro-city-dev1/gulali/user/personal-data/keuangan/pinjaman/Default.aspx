<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_user_keuangan_pinjaman" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - User >> Data Pribadi >> Pinjaman</title>
    <ucmeta:meta ID="meta" runat="server" />
    <style>
        .dayheader {
            color: #797979;
            font-size: 8pt;
            background: #ebeff2;
            font-size: 14px;
            text-transform: uppercase;
            text-align: center;
        }

        .nextprev {
            padding: 10px 50px 10px 50px;
        }
    </style>
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
                        <h4 class="page-title">Keuangan</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div id="basicwizard" class=" pull-in">
                                <!-- tab pinjaman / slip gaji -->
                                <ul>
                                    <li class="active">
                                        <asp:Literal ID="tab_pinjaman" runat="server"></asp:Literal>
                                    </li>
                                    <li class="">
                                        <asp:Literal ID="tab_slip_gaji" runat="server"></asp:Literal></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            <ucfooter:footer id="footer" runat="server" />
            </div>
            <!-- end row -->
        </div>
    </form>
</body>
<ucscript:script id="script" runat="server" />
</html>
