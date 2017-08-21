<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_component_setting" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setup >> Payroll >> Payroll Component >> Component Setting</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="report" runat="server" />
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
                        <h4 class="page-title">Komponen Setting</h4>
                    </div>
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-sm-12">
                        <div class="card-box table-responsive">
                            <div id="datatable-buttons_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                                <div style="padding: 10px !important; float: right;">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>
                                            <td>
                                                <div class="div-button-link">
                                                    <a href="add/?mode=1" class="btn btn-primary" style="text-decoration: none;">Regular Income</a>
                                                    <a href="add/?mode=2" class="btn btn-primary" style="text-decoration: none;">Irregular Income</a>
                                                    <a href="add/?mode=3" class="btn btn-primary" style="text-decoration: none;">Deduction </a>
                                                </div>
                                            </td>
                                            <td style="padding-right: 10px;">
                                                <asp:TextBox ID="search_text" runat="server" class="form-control" AutoPostBack="True" OnTextChanged="btn_search_Click" placeholder="Code or Component Name" />
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
                                            <td style="text-align: center;" id="td_hide" runat="server">showing
                                                <asp:TextBox ID="pagesum" runat="server" AutoPostBack="True" Width="25px" Text="10" OnTextChanged="page_enter" />
                                                <%--<asp:Literal ID="pagesum" Text="10" runat="server"></asp:Literal>--%>
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
                </div>
                <!-- end col -->
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
    </form>
</body>
</html>
