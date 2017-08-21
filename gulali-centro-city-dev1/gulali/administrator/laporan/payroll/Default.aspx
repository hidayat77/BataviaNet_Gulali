<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_laporan_payroll" %>

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
    <title>Gulali HRIS - Laporan >> Payroll</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="laporan" runat="server" />
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
                        <h4 class="page-title">Payroll</h4>
                    </div>
                </div>
                <!-- end row -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div id="basicwizard" class=" pull-in">
                                <!-- tab active/inactive -->
                                <ul class="nav nav-tabs navtab-wizard nav-justified bg-muted">
                                    <li <asp:Literal ID="class_perbulan" runat="server"></asp:Literal>>
                                        <asp:Literal ID="link_perbulan" runat="server"></asp:Literal>Laporan Detail Payroll Perbulan</a>
                                    </li>
                                    <li <asp:Literal ID="class_perperiode" runat="server"></asp:Literal>>
                                        <asp:Literal ID="link_perperiode" runat="server"></asp:Literal>Summary Perperiode</a>
                                    </li>
                                </ul>
                                <div class="tab-content b-0 m-b-0">
                                    <asp:UpdatePanel runat="server" ID="updatePanelTable" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                        <div class="btn-group m-t-15" style="margin-right: 10px;" id="div_date_perbulan" runat="server">
                                                            <asp:TextBox ID="date_perbulan" runat="server" class="form-control" placeholder="Perbulan" Visible="true" OnTextChanged="date_perbulan_TextChanged" AutoPostBack="true" autocomplete="off" />
                                                        </div>
                                                        <div class="btn-group m-t-15" style="margin-right: 10px;" id="div_date_perperiode_from" runat="server" visible="false">
                                                                <asp:TextBox ID="date_perperiode_from" runat="server" class="form-control" placeholder="Periode Mulai" Visible="true" AutoPostBack="true" OnTextChanged="date_perperiode_from_TextChanged" autocomplete="off" />
                                                            </div>
                                                        <div class="btn-group m-t-15" style="margin-right: 10px;" id="div_date_perperiode_to" runat="server" visible="false">
                                                            <asp:TextBox ID="date_perperiode_to" runat="server" class="form-control" placeholder="Periode Selesai" Visible="true" AutoPostBack="true" OnTextChanged="date_perperiode_to_TextChanged" autocomplete="off" />
                                                        </div>

                                                        <div class="btn-group m-t-15" style="margin-right: 10px;" id="div_cari" runat="server" visible="false">
                                                            <asp:Button runat="server" Text="Cari" CssClass="btn btn-primary" ID="btnGo" OnClick="btnGo_Click" AutoPostBack="true" />
                                                        </div>
                                                        <div class="btn-group pull-right m-t-15" id="div_download" runat="server" visible="false">
                                                            <asp:Button runat="server" Text="Download Ke Excel" CssClass="btn btn-primary" OnClick="btnDownload_Click" ID="btnDownload" AutoPostBack="true" />
                                                        </div>
                                                        <table style="width: 100%;">
                                                            <%--komen--%>
                                                            <tr hidden="hidden">
                                                                <td style="width: 45%; text-align: left;">
                                                                    <div class="div-button-link" style="cursor: pointer;">
                                                                        <asp:Button Style="color: #033363;" class="btn btn-primary" ID="Export" runat="server"
                                                                            Text="Export to Excel" name="Export" OnClick="BtnExportExcel" AccessKey="s"/>
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
                                                            <%--komen--%>
                                                            <tr>
                                                                <td>&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Literal ID="summary_total" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="overflow-x: auto; width: 100%; height: 240px;" id="div_table" runat="server">
                                                            <asp:Literal ID="table" runat="server"/>
                                                        </div>
                                                        <asp:Literal ID="lable" runat="server" />
                                                        <asp:Literal ID="modal" runat="server" />
                                                    </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnGo" />
                                            <asp:PostBackTrigger ControlID="btnDownload" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end row -->
                <ucfooter:footer ID="footer" runat="server" />
            </div>
            <!-- end container -->
        </div>
    </form>
</body>
</html>
<script>
    function pageLoad() {
        $('#date_perbulan').unbind();
        $('#date_perbulan').datepicker({
            format: 'MM-yyyy',
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        });
    
        $('#date_perperiode_from').unbind();
        $('#date_perperiode_from').datepicker({
            format: 'MM-yyyy',
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        });
    
        $('#date_perperiode_to').unbind();
        $('#date_perperiode_to').datepicker({
            format: 'MM-yyyy',
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        }
        );
    }

    //tanpa postback
    //jQuery('#date_periode').datepicker({
    //    format: 'MM-yyyy',
    //    viewMode: "months",
    //    minViewMode: "months",
    //    autoclose: true,
    //    orientation: "auto top",
    //    todayHighlight: true
    //});
</script>
