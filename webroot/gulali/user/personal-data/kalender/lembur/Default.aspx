<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_user_kalender_lembur" %>

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
    <title>Gulali HRIS - User >> Data Pribadi >> Lembur</title>
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
                                    <li class="active">
                                        <asp:Literal ID="tab_lembur" runat="server"></asp:Literal></li>
                                    <li class="">
                                        <asp:Literal ID="tab_kehadiran" runat="server"></asp:Literal></li>
                                </ul>
                            </div>
                            <div class="tab-content b-0 m-b-0">
                                    <div class="row">
                                        <div class="form-group clearfix">
                                            <div class="col-sm-12">
                                                <div id="datatable-buttons_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                                                    <div style="padding: 10px !important; float: right;">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                                            <tr>
                                                                <td style="padding-right: 20px;"><a class="btn btn-primary" href="add/">Tambah</a></td>
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
                                                                <td style="text-align: center;">showing
                                                    <%--<input name="pagesum" type="text" id="textfield5" size="5" onKeyPress="checkEnter2(event,1,'',this.value,'');" value="20"  style="text-align:center;"/>--%>
                                                                    <asp:TextBox ID="pagesum" runat="server" AutoPostBack="True" Width="25px" Text="10" OnTextChanged="page_enter" />
                                                                    per page
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="clear: both;">&nbsp;</div>
                                                    <asp:Literal ID="table" runat="server" />
                                                    <asp:Literal ID="note" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <!-- end col -->
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
