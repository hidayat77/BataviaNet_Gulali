<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_payroll_component_detail" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setup >> Payroll >> Payroll Component >> Detail</title>
    <ucmeta:meta ID="meta" runat="server" />
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
                        <h4 class="page-title">Detail Fulltime</h4>
                    </div>
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-sm-12">
                        <div class="card-box">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-horizontal" role="form" runat="server">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Kode</label>
                                            <div class="col-md-6">
                                             <asp:TextBox ID="txtKode" class="form-control" runat="server" disabled=""></asp:TextBox>
                                            </div>
                                         </div>

                                         <div class="form-group">
                                            <label class="col-md-3 control-label">Nama</label>
                                            <div class="col-md-6">
                                             <asp:TextBox ID="txtNama" class="form-control" runat="server" disabled=""></asp:TextBox>
                                            </div>
                                         </div>

                                         <div class="form-group">
                                            <label class="col-md-3 control-label">Toleransi Telat</label>
                                            <div class="col-md-6">
                                             <asp:TextBox ID="txtToleransiTelat" class="form-control" runat="server" disabled=""></asp:TextBox>
                                            </div>
                                         </div>

                                         <div class="form-group">
                                            <label class="col-md-3 control-label">Durasi Istirahat</label>
                                            <div class="col-md-6">
                                             <asp:TextBox ID="txtDurasi" class="form-control" runat="server" disabled=""></asp:TextBox>
                                            </div>
                                         </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Jam Masuk</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtJamMasuk" class="form-control" runat="server" placeholder="Jam" disabled=""></asp:TextBox>
                                            </div>

                                            <label class="col-md-2 control-label">Jam Keluar</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtJamKeluar" runat="server" class="form-control" placeholder="Jam" disabled=""/>
                                                </div>
                                         </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->

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
        //$('#tanggal_mulai').unbind();
        $('#tanggal_mulai').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        });

        //$('#tanggal_selesai').unbind();
        $('#tanggal_selesai').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            orientation: "auto top",
            todayHighlight: true
        });

        //$('#txtJamMasuk').unbind();
        $('#txtJamMasuk').timepicker({
            //orientation: "auto top",
            showMeridian: false
        });

        //$('#txtJamKeluar').unbind();
        $('#txtJamKeluar').timepicker({
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

