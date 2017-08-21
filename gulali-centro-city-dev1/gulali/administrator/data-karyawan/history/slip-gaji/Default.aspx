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
    <title>Gulali HRIS - Setting >> Data Karyawan >> History >> Slip Gaji</title>
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
                        <h4 class="page-title">Slip Gaji - <asp:Literal ID="nama_employee" runat="server"/></h4>
                    </div>
                </div>
                <div class="row">
                     <asp:UpdatePanel runat="server" ID="updatePanelTable" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-sm-12" style="padding-bottom: 15px;">
                                <div class="btn-group">
                                    <%--<button type="button" class="btn btn-info dropdown-toggle waves-effect waves-light" data-toggle="dropdown" aria-expanded="false">Pilih Tahun <span class="caret"></span></button>
                                    <ul class="dropdown-menu" role="menu">
                                        <asp:Literal ID="year" runat="server"/>
                                    </ul>--%>

                                    <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control" OnSelectedIndexChanged="load_year" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="-- Pilih Tahun  --" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Literal ID="table" runat="server"/>
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
