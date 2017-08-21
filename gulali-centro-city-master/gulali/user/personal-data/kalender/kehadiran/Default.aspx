<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_user_kalender_kehadiran" %>

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
    <title>Gulali HRIS - User >> Data Pribadi >> Kehadiran</title>
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
                        <h4 class="page-title">Kalender</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div id="basicwizard" class=" pull-in">
                                <!-- tab cuti/lembur/kehadiran -->
                                <ul>
                                    <li class="">
                                        <asp:Literal ID="tab_cuti" runat="server"></asp:Literal>
                                    </li>
                                    <li class="">
                                        <asp:Literal ID="tab_lembur" runat="server"></asp:Literal></li>
                                    <li class="active">
                                        <asp:Literal ID="tab_kehadiran" runat="server"></asp:Literal></li>
                                </ul>
                                <div class="tab-content b-0 m-b-0" style="padding-top: 0px !important;">
                                    <div class="row">
                                        <div class="form-group clearfix">
                                            <div class="col-sm-12">
                                                <div id="datatable-buttons_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                                                    <asp:Calendar ID="Calendar" runat="server" DayNameFormat="Full" BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" Height="400px" NextPrevFormat="FullMonth" OnDayRender="Calendar_DayRender" Width="100%">
                                                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                        <WeekendDayStyle BackColor="#fcf8e3" ForeColor="#ff0000" />
                                                        <TodayDayStyle BackColor="#f3f3f3" />
                                                        <OtherMonthDayStyle ForeColor="#999999" />
                                                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" VerticalAlign="Bottom" CssClass="nextprev" ForeColor="#71b6f9" />
                                                        <DayHeaderStyle BackColor="#ebeff2" CssClass="dayheader" />
                                                        <TitleStyle BackColor="white" BorderColor="white" BorderWidth="2px" Font-Bold="True"
                                                            Font-Size="12pt" />
                                                    </asp:Calendar>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- end col -->
                                    </div>
                                </div>
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
