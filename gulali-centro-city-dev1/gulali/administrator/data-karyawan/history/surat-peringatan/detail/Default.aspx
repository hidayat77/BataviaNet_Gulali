<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_surat_peringatan_detail" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setting >> Data Karyawan >> History >> Surat Peringatan >> Detail</title>
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
                        <h4 class="page-title">Detail Surat Peringatan -
                            <asp:Literal ID="nama_employee" runat="server" /></h4>
                    </div>
                </div>
                <div class="row">
                    <asp:UpdatePanel runat="server" ID="updatePanelTable" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-sm-12">
                                <div class="card-box">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-horizontal" role="form" runat="server">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Jenis Peringatan</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddl_type_warning" class="form-control" runat="server" disabled="disabled">
                                                            <asp:ListItem Value="SP1" Text="SP1" />
                                                            <asp:ListItem Value="SP2" Text="SP2" />
                                                            <asp:ListItem Value="SP3" Text="SP3" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Berkas</label>
                                                    <div class="col-md-8">
                                                        <asp:Literal ID="file" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Catatan</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="remarks" class="form-control" runat="server" TextMode="MultiLine" placeholder="Catatan" disabled="disabled"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Dibuat Oleh</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="createby" class="form-control" runat="server" disabled="disabled"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- end col -->
                                    </div>
                                    <!-- end row -->
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <ucfooter:footer ID="footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
