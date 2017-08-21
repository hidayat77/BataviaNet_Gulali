<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_user_kalender_cuti_add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Gulali HRIS - User >> Data Pribadi >> Cuti >> Add</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="personal_data" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title">Tambah Cuti</h4>
                </div>
            </div>
            <div class="row">
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="sm" runat="server" />
                    <div class="col-sm-8">
                        <asp:Label ID="note" runat="server"></asp:Label>
                        <div class="card-box">
                            <div class="row">
                                <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-horizontal" role="form" runat="server">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Tanggal</label>
                                                <div class="col-md-8">
                                                    <div class="input-daterange input-group" id="date-range">
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="from" runat="server" autocomplete="off" CssClass="form-control" />
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:Image ID="imgfrom" runat="server" ImageUrl="/assets/images/icons/picker.png" />
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="to" runat="server" autocomplete="off" CssClass="form-control" />
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:Image ID="imgto" runat="server" ImageUrl="/assets/images/icons/picker.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="div_from2" runat="server" visible="false">
                                                    <label class="col-md-3 control-label">
                                                        <asp:Label ID="lbfrom2" runat="server" Text=""></asp:Label>
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="from2" runat="server" SkinID="tgl" autocomplete="off" />
                                                    </div>
                                                </div>
                                                <div class="form-group" id="tr_half" runat="server">
                                                    <label class="col-md-3 control-label"></label>
                                                    <div class="col-md-8">
                                                        <asp:CheckBox ID="cbhalf" runat="server" OnCheckedChanged="cbhalf_CheckedChanged"
                                                            AutoPostBack="true" />&nbsp;
                                            <asp:Label ID="lbhalfday" runat="server" Text="Setengah Hari"></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbpagi" runat="server" GroupName="half" Visible="false" />
                                                        <asp:Label ID="lbpagi" runat="server" Text="Pagi" Visible="false" />
                                                        <asp:RadioButton ID="rbsiang" runat="server" GroupName="half" Visible="false" />
                                                        <asp:Label ID="lbsiang" runat="server" Text="Siang" Visible="false" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Jenis Cuti</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddl_type" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Lampiran</label>
                                                    <div class="col-md-8">
                                                        <asp:FileUpload ID="medical" runat="server" />
                                                        <asp:Literal ID="error_file" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Staff Pengganti</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddl_employee" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="" Text="-- Pilih Karyawan --" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Catatan</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtremarks" TextMode="MultiLine" autocomplete="off" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">&nbsp;</label>
                                                <div class="col-md-8">
                                                    <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="BtnSubmit" AccessKey="s" />
                                                    <asp:Literal ID="href_cancel" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Submit" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <!-- end row -->
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <div class="text-center card-box">
                                <div>
                                    <div class="profile-info-detail" style="text-align: left;">
                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold">Nama</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13">: <asp:Literal ID="full_name" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>

                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold">Departemen</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13">: <asp:Literal ID="department" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>

                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold">Divisi</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13">: <asp:Literal ID="division" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>

                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold">Sisa Cuti</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13">: <asp:Literal ID="leave_balanced" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                    </div>
                    <!-- end row -->
                </form>
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>
<ucscript:script ID="script" runat="server" />
<script>
    jQuery('#from').datepicker({
        format: 'dd-MM-yyyy',
        autoclose: true,
        todayHighlight: true
    });

    jQuery('#to').datepicker({
        format: 'dd-MM-yyyy',
        autoclose: true,
        todayHighlight: true
    });
</script>
</html>
