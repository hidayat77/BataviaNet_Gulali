<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_general" %>

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
    <title>Gulali HRIS - Kalender >> Kehadiran</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="department" runat="server" />
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
                        <h4 class="page-title">Jadwal Kehadiran Tanggal <asp:Literal ID="showtanggal" runat="server"></asp:Literal></h4>
                    </div>
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-sm-12">
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="padding: 10px !important;">
                                        <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="form-group clearfix">
                                                        <div id="datatable-buttons_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                                                            <div style="padding: 10px !important; float: right;">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                                                    <tr>
                                                                        <td style="padding-right: 10px;">
                                                                            <asp:TextBox ID="search_text" class="form-control" runat="server" AutoPostBack="True" AutoComplete="off" placeholder="Cari" OnTextChanged="btnSearch_Click" />
                                                                        </td>
                                                                        <td style="padding-right: 10px;" id="div_add" runat="server">
                                                                            <a class="btn btn-primary" href="add/">Tambah</a>
                                                                        </td>
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
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
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
<ucscript:script ID="script" runat="server" />
</html>

