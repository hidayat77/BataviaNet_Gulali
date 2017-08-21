<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_keuangan_payroll" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Keuangan >> Payroll >> Tambah</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body style="background: none;">
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="keuangan" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="container">
                        <!-- Page-Title -->
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="btn-group pull-right m-t-15">
                                    <asp:Literal ID="link_href" runat="server"></asp:Literal>
                                </div>
                                <h4 class="page-title">Payroll Generate</h4>
                            </div>
                        </div>
                        <!-- end row -->
                        <div class="row">

                            <div class="col-sm-12">
                                <div class="card-box">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-6" id="filter_pertama" runat="server">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Tanggal Mulai</label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="tanggal_mulai" runat="server" AutoPostBack="True" class="form-control" placeholder="Tanggal" />
                                                        </div>
                                                        <label class="col-md-1 control-label">Jam</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="jam_mulai" runat="server" class="form-control" placeholder="Jam" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Tanggal Selesai</label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="tanggal_selesai" runat="server" AutoPostBack="True" class="form-control" placeholder="Tanggal" />
                                                        </div>
                                                        <label class="col-md-1 control-label">Jam</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="jam_selesai" runat="server" class="form-control" placeholder="Jam" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6" id="filter_kedua" runat="server">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Hari Kerja Aktif</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txt_hari_kerja" runat="server" class="form-control" placeholder="Hari Kerja" />
                                                        </div>
                                                        <label class="col-md-2 control-label">Pilih Bulan</label>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList class="form-control" ID="ddl_bulan" runat="server">
                                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Tingkatkan Bonus</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txt_bonus" runat="server" class="form-control" placeholder="Bonus" />
                                                        </div>
                                                        <label class="col-md-2 control-label">&nbsp;</label>
                                                        <div class="col-md-3">
                                                            <asp:Button class="btn btn-primary" ID="btn_generate" runat="server" Text="Hitung Penggajian" OnClick="BtnSubmit" />
                                                            <asp:Button class="btn btn-primary" ID="btn_save" runat="server" Text="Simpan" Visible="false" OnClick="BtnSubmit_Final" />
                                                            <%--<a class="btn btn-primary" href="add/">Hitung Penggajian</a>--%>
                                                        </div>
                                                    </div>
                                                    <asp:Literal ID="Literal1" runat="server" Visible="true" />
                                                    <asp:Literal ID="Literal2" runat="server" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end container -->

                    <div style="overflow-y: auto; height: auto; width: 100%;" id="div_table" runat="server" visible="false">
                        <div class="card-box" style="overflow-y: auto; height: auto; width: 100%; padding-left:5px; padding-right:5px;">
                            <asp:Literal ID="thead" runat="server" Visible="true" />
                            <asp:Literal ID="table" runat="server" Visible="false" />
                            <asp:PlaceHolder ID="PH" runat="server" EnableViewState="true" />
                        </div>
                    </div>
                    <asp:Label ID="preview" runat="server" />
                    <!--content-->
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_generate" />
                    <asp:PostBackTrigger ControlID="btn_save" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <ucfooter:footer ID="footer" runat="server" />
    </form>
</body>
</html>
<script>
    function pageLoad() {
        $('#tanggal_mulai').unbind();
        $('#tanggal_mulai').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        });

        $('#tanggal_selesai').unbind();
        $('#tanggal_selesai').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        });

        $('#jam_mulai').unbind();
        $('#jam_mulai').timepicker({
            //orientation: "auto top",
            showMeridian: false
        });

        $('#jam_selesai').unbind();
        $('#jam_selesai').timepicker({
            //orientation: "auto top",
            showMeridian: false
        });
    }
    <%--function HideLabel() {
        document.getElementById('<%= note.ClientID %>').style.display = "none";
        document.getElementById('<%= note.ClientID %>').appendChild = "";
    }
    setTimeout("HideLabel();", 30000);--%>
</script>
