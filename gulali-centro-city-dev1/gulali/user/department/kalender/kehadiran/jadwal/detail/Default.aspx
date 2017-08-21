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
    <title>Gulali HRIS - Setup >> Kehadiran - Detail</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="setup" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title">Detail Kehadiran</h4>
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
                                                        <label class="col-md-4 control-label">Kode Kehadiran</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="Kode_Kehadiran" class="form-control" runat="server" placeholder="Kode" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Kehadiran</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="nama_kehadiran" class="form-control" runat="server" placeholder="Nama Kehadiran" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Toleransi Telat</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="toleransi_telat" class="form-control" runat="server" placeholder="Menit" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                        </div>
                                                         <label class="col-md-4 control-label">Jumlah Jam Istirahat</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="Jumlah_jam_istirahat" class="form-control" runat="server" placeholder="Menit" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Jam Mulai</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="jam_mulai" class="form-control" runat="server" placeholder="jam" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                        </div>
                                                         <label class="col-md-2 control-label">Jam Selesai</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="jam_selesai" class="form-control" runat="server" placeholder="jam" autocomplete="off" disabled="disabled"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
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
    //jQuery('#tanggal_lahir').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    //jQuery('#start_of_employment').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    //jQuery('#contract_start').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    //jQuery('#contract_end').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    //jQuery('#dep_dateofbirth1').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //jQuery('#dep_dateofbirth2').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //jQuery('#dep_dateofbirth3').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //jQuery('#dep_dateofbirth4').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //jQuery('#dep_dateofbirth5').datepicker({
    //    format: 'dd-MM-yyyy',
    //    autoclose: true,
    //    todayHighlight: true
    //});

    function Convert() {
        var Namadepan = jQuery("#nama_depan").val();
        var Namatengah = jQuery("#nama_tengah").val();
        var Namabelakang = jQuery("#nama_belakang").val();
        var NamaLengkap = Namadepan + " " + Namatengah + " " + Namabelakang;

        //var kilometers = meters / 1000;
        jQuery("#nama_lengkap").val(NamaLengkap);
    }

    function pageLoad() {
        $('#tanggal_lahir').unbind();
        $('#tanggal_lahir').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#start_of_employment').unbind();
        $('#start_of_employment').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#contract_start').unbind();
        $('#contract_start').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#contract_end').unbind();
        $('#contract_end').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth1').unbind();
        $('#dep_dateofbirth1').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth2').unbind();
        $('#dep_dateofbirth2').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth3').unbind();
        $('#dep_dateofbirth3').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth4').unbind();
        $('#dep_dateofbirth4').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });

        $('#dep_dateofbirth5').unbind();
        $('#dep_dateofbirth5').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });
    }
    <%--function HideLabel() {
        document.getElementById('<%= note.ClientID %>').style.display = "none";
        document.getElementById('<%= note.ClientID %>').appendChild = "";
    }
    setTimeout("HideLabel();", 30000);--%>
</script>
</html>

