<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_cuti" %>

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
    <title>Gulali HRIS - Setup >> Cuti</title>
    <ucmeta:meta ID="meta" runat="server" />
    <script type="text/javascript">
        function changeStatus(id, x, y) {
            var url = "changeStatus.aspx?id=" + id + "&x=" + x + "&y=" + y;
            window.location = url;
        }
    </script>
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
                        <h4 class="page-title">Cuti</h4>
                    </div>
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-sm-12">
                        <div class="card-box table-responsive" style="min-height: 400px; padding-top: 0px !important;">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div id="basicwizard" class=" pull-in">
                                        <!-- tab master/adj -->
                                        <ul>
                                            <li class="">
                                                <asp:Literal ID="tab_master" runat="server"></asp:Literal>
                                            </li>
                                            <li class="active">
                                                <asp:Literal ID="tab_adj" runat="server"></asp:Literal></li>
                                        </ul>
                                        <div class="tab-content b-0 m-b-0">
                                            <div class="row">
                                                <div class="form-group clearfix">
                                                    <div id="datatable-buttons_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                                                        <div style="padding: 10px !important; float: left;">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                                                <tr>
                                                                    <td style="padding-right: 10px;">
                                                                        <a class="btn btn-primary" href="increase/">Penambahan</a>
                                                                    </td>
                                                                    <td style="padding-right: 10px;">
                                                                        <a class="btn btn-primary" href="decrease/">Pengurangan</a>
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
                                    </div>
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

