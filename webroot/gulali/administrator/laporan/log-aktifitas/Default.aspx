﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_laporan_log_aktifitas" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Laporan >> Log Aktifitas</title>
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
                        <h4 class="page-title">Log Aktifitas</h4>
                    </div>
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-sm-12">
                        <div class="card-box table-responsive" style="min-height:300px">
                            <asp:UpdatePanel runat="server" ID="updatePanelTable" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="padding: 10px !important; float: right;">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                            <tr>
                                                <td style="padding-right: 10px;">
                                                    <asp:Button ID="ExportExcel_New" Style="outline: none; color: #033363;"
                                                        class="btn btn-primary" runat="server" Text="Export Excel" name="ExportExcel" OnClick="BtnExportExcel_New" AccessKey="s" />
                                                </td>
                                                <td style="padding-right: 10px;">
                                                    <!-- search panel -->
                                                    <div>
                                                        <div style="float: left;">
                                                            <asp:DropDownList ID="ddl_username" class="form-control" runat="server">
                                                                <asp:ListItem Value="0" Text="-- Semua Username --" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="input-daterange input-group" id="date-range">
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="from" runat="server" class="form-control" placeholder="From" />
                                                            </div>
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="to" runat="server" autocomplete="off" CssClass="form-control" placeholder="To" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td style="padding-right: 10px;">
                                                    <asp:Button ID="Go" Style="outline: none; color: #033363;"
                                                        class="btn btn-primary" runat="server" Text="Go" name="Go" OnClick="BtnGo" AccessKey="s" /></td>

                                                <td style="text-align: center; padding-right: 20px;">
                                                    <asp:Button class="btn" ID="previous2" runat="server" OnClick="previous2_Click" Text="<<" />
                                                    <asp:Button class="btn" ID="previous" runat="server" OnClick="previous_Click" Text="<" />
                                                </td>
                                                <td style="text-align: center;">page
                                                                    <asp:TextBox ID="pagenum" runat="server" AutoPostBack="True" Width="25px" OnTextChanged="page_enter" />
                                                    of
                                                    <asp:Literal ID="count_page" runat="server" />
                                                </td>
                                                <td style="text-align: center; padding-left: 20px;">
                                                    <asp:Button class="btn" ID="next" runat="server" OnClick="next_Click" Text=">" />
                                                    <asp:Button class="btn" ID="next2" runat="server" OnClick="next2_Click" Text=">>" />
                                                </td>
                                                <td style="text-align: center;" id="td_hide" runat="server" visible="false">showing
                                                        <asp:Literal ID="pagesum" Text="10" runat="server"></asp:Literal>
                                                    per page
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="clear: both;">&nbsp;</div>
                                    <asp:Literal ID="table" runat="server" />
                                    <asp:Literal ID="lable" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ExportExcel_New" />
                                    <asp:PostBackTrigger ControlID="Go" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </form>
</body>
<script>
    jQuery('#from').datepicker({
        format: 'dd-MM-yyyy',
        autoclose: true,
        todayHighlight: true
    });

    jQuery('#to').datepicker({
        format: 'dd-MM-yyyy',
        autoclose: true,
        todayHighlight: true
    });
</script>
</html>
