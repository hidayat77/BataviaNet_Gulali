<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_data_karyawan" %>

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
    <title>Gulali HRIS - Data Karyawan</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="data-karyawan" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <div class="container">
                <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="page-title">Data Karyawan</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div id="basicwizard" class=" pull-in">
                                <!-- tab active/inactive -->
                                <ul>
                                    <li <asp:Literal ID="class_active" runat="server"></asp:Literal>>
                                        <asp:Literal ID="link_active" runat="server"></asp:Literal>Active</a>
                                    </li>
                                    <li <asp:Literal ID="class_inactive" runat="server"></asp:Literal>>
                                        <asp:Literal ID="link_inactive" runat="server"></asp:Literal>Inactive</a>
                                    </li>
                                </ul>

                                <div class="tab-content b-0 m-b-0">
                                    <div class="row">
                                        <div class="form-group clearfix">
                                            <div class="col-sm-12">
                                                <div id="datatable-buttons_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">

                                                    <div style="padding: 10px !important; float: right;">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                                            <tr>
                                                                <td style="padding-right: 10px;">
                                                                    <a class="btn btn-primary" href="add/">Tambah</a>
                                                                </td>
                                                                <td style="padding-right: 10px;">
                                                                    <!-- search panel -->
                                                                    <div style="text-align: right;">
                                                                        <asp:DropDownList class="form-control" ID="ddl_search" runat="server">
                                                                            <asp:ListItem Value="Employee_Full_Name" Text="Nama" Selected="True" />
                                                                            <asp:ListItem Value="Employee_Address" Text="Alamat" />
                                                                            <asp:ListItem Value="Department_Name" Text="Departemen" />
                                                                            <asp:ListItem Value="Division_Name" Text="Divisi" />
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="search_text" class="form-control" runat="server" AutoPostBack="True" AutoComplete="off" placeholder="Cari" OnTextChanged="btnSearch_Click" />
                                                                    </div>
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
                                                        <asp:Literal ID="pagesum" Text="20" runat="server"></asp:Literal>
                                                                    per page
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="clear:both;">&nbsp;</div>
                                                    <asp:Literal ID="table" runat="server" />
                                                    <asp:Literal ID="lable" runat="server" />
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
            <ucfooter:footer ID="footer" runat="server" />
            </div>
            <!-- end row -->
        </div>
    </form>
</body>
<ucscript:script id="script" runat="server" />
</html>