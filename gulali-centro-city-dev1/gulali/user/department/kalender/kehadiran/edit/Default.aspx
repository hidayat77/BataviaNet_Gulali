<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_data_karyawan_add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Gulali HRIS - Kalender >> Kehadiran - Edit</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="department" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="btn-group pull-right m-t-15">
                        <asp:Literal ID="link_href" runat="server"></asp:Literal>
                    </div>
                    <h4 class="page-title">Ubah Jadwal</h4>
                </div>
            </div>
            <div class="row">
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="sm" runat="server" />
                    <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-sm-12">
                                <div class="form-horizontal" role="form" runat="server">
                                    <div class="card-box">
                                        <%--<h4 class="header-title m-t-0 m-b-30">Data Pribadi</h4>--%>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Karyawan</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="Nama_karyawan" class="form-control" runat="server" placeholder="Nama Karyawan" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Tanggal</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txt_tanggal" class="form-control" runat="server" placeholder="Tanggal" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jadwal Shifting</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList class="form-control" ID="ddl_jadwal" runat="server" OnTextChanged="ddl_jadwal_SelectedTextChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="">-- Pilih Jadwal --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jam Masuk</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txt_masuk" class="form-control" runat="server" placeholder="Jam" disabled="disabled" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                        <label class="col-md-2 control-label">Jam Keluar</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txt_keluar" class="form-control" runat="server" placeholder="Jam" disabled="disabled" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                     <div class="form-group" id="div_employe_replace" runat="server" visible="false">
                                                        <label class="col-md-4 control-label">Nama Karyawan Pengganti</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList class="form-control" ID="ddl_employee" runat="server" AutoPostBack="true" AppendDataBoundItems="True">
                                                                <asp:ListItem Value="">-- Pilih Karyawan --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
                                                            <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="BtnUpdate" AccessKey="s" />
                                                            <asp:Button ID="Back" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Batal" name="cancel" OnClick="BtnCancel" AccessKey="s" />
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
                            <asp:PostBackTrigger ControlID="Submit" />
                            <asp:PostBackTrigger ControlID="Back" />
                        </Triggers>
                    </asp:UpdatePanel>
                </form>
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>

<script>
    function pageLoad() {
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
</script>
</html>

