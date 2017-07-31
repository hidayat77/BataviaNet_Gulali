<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_payroll_detail"
    ValidateRequest="false" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Keuangan >> Payroll >> Detail</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="keuangan" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <div class="container">
                <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <div class="btn-group pull-right m-t-15">
                            <asp:Literal ID="link_href" runat="server"></asp:Literal>
                        </div>
                        <h4 class="page-title">Payroll Detail</h4>
                    </div>
                </div>
                <!-- end row -->
                <div class="card-box">
                    <div class="widgetcontent">
                        <asp:UpdatePanel runat="server" ID="updatePanelTable" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr hidden="hidden">
                                        <td style="width: 45%; text-align: left;">
                                            <div class="div-button-link" style="cursor: pointer;">
                                                <asp:Button Style="color: #033363;" class="btn btn-primary" ID="Export" runat="server"
                                                    Text="Export to Excel" name="Export" OnClick="BtnExportExcel" AccessKey="s" />
                                            </div>
                                        </td>
                                        <td style="width: 10%; text-align: right;">
                                            <div>
                                                Search :
                                                    <asp:TextBox ID="search_text" class="form-control" autocomplete="off" runat="server" AutoPostBack="True"
                                                        OnTextChanged="btn_search_Click" placeholder="Employee Name" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr id="tr_sum" runat="server">
                                        <td colspan="2">
                                            <table style="width: 50%; font-weight: bold;">
                                                <tr>
                                                    <td style="width: 30%;">
                                                        <asp:Label ID="lable_month" runat="server"></asp:Label>
                                                        <asp:Label ID="lable_year" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;">&nbsp;
                                                    </td>
                                                    <td>Total Gaji :
                                                            <asp:Label ID="sum" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Literal ID="summary_total" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <br>
                                <%--<div style="overflow-x:auto; width: 100%; height: 500px; overflow: scroll;">--%>
                                <%--<div style="overflow-x:auto; width: 100%; height: 300px; overflow: scroll;"/>--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Export" />
                                <asp:PostBackTrigger ControlID="search_text" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div style="overflow-x: auto; width: 100%; height: 240px;">
                            <asp:Literal ID="table" runat="server" />
                        </div>
                        <asp:Literal ID="lable" runat="server" />
                    </div>
                </div>
                <!-- Trigger the modal with a button -->
                <!-- Modal -->
                <asp:Literal ID="modal" runat="server" />
                <!--maincontent-->
            </div>
            <!--rightpanel-->
        </div>
        <ucfooter:footer ID="footer" runat="server" />
    </form>
</body>
</html>
