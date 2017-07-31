<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_data_karyawan_history" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setting >> Data Karyawan >> History >> Kontrak</title>
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
                        <div class="btn-group pull-right m-t-15">
                            <asp:Literal ID="link_href" runat="server"></asp:Literal>
                        </div>
                        <h4 class="page-title">Kontrak - <asp:Literal ID="nama_employee" runat="server"/></h4>
                    </div>
                </div>
                <div class="row">
                    <asp:UpdatePanel runat="server" ID="updatePanelTable" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-sm-12">
                                <div class="card-box">
                                    <div class="row">
                                        <div style="padding: 10px !important; float: right;">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                                <tr>
                                                    <td style="padding-right: 10px;">
                                                        <asp:Button runat="server" Text="Tambah" CssClass="btn btn-primary" ID="btnadds" OnClick="add" AutoPostBack="true" />
                                                    </td>
                                                    <td style="padding-right: 10px;">
                                                        <!-- search panel -->
                                                        <div>
                                                            <div style="float: left;">
                                                                <asp:DropDownList class="form-control" ID="ddl_search" runat="server">
                                                                    <asp:ListItem Value="Position" Text="Posisi" Selected="True" />
                                                                    <asp:ListItem Value="Remarks" Text="Catatan" />
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div style="float: left;">
                                                                <asp:TextBox ID="search_text" class="form-control" runat="server" AutoPostBack="True" AutoComplete="off" placeholder="Cari" OnTextChanged="btnSearch_Click" />
                                                            </div>
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
                                                        <asp:Literal ID="pagesum" Text="10" runat="server"></asp:Literal>
                                                        per page
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-sm-12" style="padding-bottom: 10px;">
                                            
                                            <%--<asp:Literal ID="btnadd" runat="server" />--%>
                                        </div>
                                        <div class="col-sm-12">
                                            <asp:Literal ID="lable" runat="server" />
                                            <div>
                                                <asp:Literal ID="table" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:PostBackTrigger ControlID="btnview_department" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <ucfooter:footer ID="footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
