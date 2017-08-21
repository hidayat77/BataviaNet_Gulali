<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_keuangan_pinjaman" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Keuangan >> Pinjaman >> Detail</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="keuangan" runat="server" />
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
                        <h4 class="page-title">Detail Pinjaman</h4>
                    </div>
                </div>
                <!-- end row -->

                <div class="row">
                    <asp:UpdatePanel runat="server" ID="updatePanelTable" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-sm-12">
                                <div class="card-box">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-lg-7">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Karyawan</label>
                                                        <div class="col-md-5">
                                                            <asp:DropDownList class="form-control" ID="ddl_employee" runat="server" disabled="disabled">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList class="form-control" ID="ddl_cicilan_bulan" runat="server" AutoPostBack="true" disabled="disabled">
                                                                <asp:ListItem Value="">-- Lama Cicilan --</asp:ListItem>
                                                                <asp:ListItem Value="1">1 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="2">2 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="3">3 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="4">4 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="5">5 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="6">6 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="7">7 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="8">8 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="9">9 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="10">10 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="11">11 Bulan</asp:ListItem>
                                                                <asp:ListItem Value="12">12 Bulan</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Tanggal</label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="tanggal_mulai" runat="server" class="form-control" placeholder="Tanggal Mulai" autocomplete="off" disabled="disabled"/>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="tanggal_selesai" runat="server" class="form-control" placeholder="Tanggal Selesai" autocomplete="off" disabled="disabled"/>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Pokok Pinjaman</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txt_pinjaman" runat="server" AutoPostBack="True" class="form-control" placeholder="Jumlah"  autocomplete="off" disabled="disabled"/>
                                                        </div>
                                                    </div>
                                                     <div class="form-group">
                                                        <label class="col-md-4 control-label">Terbilang</label>
                                                        <div class="col-md-8">
                                                            <i><asp:TextBox ID="txt_terbilang" runat="server" AutoPostBack="True" class="form-control" placeholder="Terbilang" autocomplete="off" disabled="disabled"/></i>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jumlah Angsuran Perbulan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txt_jumlah_perbulan" runat="server" AutoPostBack="True" class="form-control" placeholder="Jumlah" disabled="disabled"/>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Keperluan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txt_keperluan" runat="server" AutoPostBack="True" class="form-control" placeholder="Keperluan" autocomplete="off" disabled="disabled"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
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
        $('#tanggal_mulai').unbind();
        $('#tanggal_mulai').datepicker({
            format: 'dd-MM-yyyy',
            //viewMode: "months",
            //minViewMode: "months",
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        });

        $('#tanggal_selesai').unbind();
        $('#tanggal_selesai').datepicker({
            format: 'dd-MM-yyyy',
            //viewMode: "months",
            //minViewMode: "months",
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        });
    }
</script>

