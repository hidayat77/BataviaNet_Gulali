<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_user_kalender_cuti_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Gulali HRIS - Kalender >> Cuti</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="kalender" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title">Detail Cuti</h4>
                </div>
            </div>
            <div class="row">
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="sm" runat="server" />
                    <div class="col-sm-8">
                        <asp:Label ID="note" runat="server"></asp:Label>
                        <div class="card-box">
                            <div class="row">
                                <div class="form-horizontal" role="form" runat="server">
                                    <div class="form-group" id="div_date" runat="server">
                                        <label class="col-md-3 control-label">Tanggal</label>
                                        <div class="col-md-8">
                                            <div class="input-daterange input-group" id="date-range">
                                                <div style="float: left;">
                                                    <asp:TextBox ID="from" runat="server" CssClass="form-control" Enabled="false" />
                                                </div>
                                                <div style="float: left;">
                                                </div>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="to" runat="server" CssClass="form-control" Enabled="false" />
                                                </div>
                                                <div style="float: left;">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="div_date_cb" runat="server" visible="false">
                                        <label class="col-md-3 control-label">Tanggal</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="to_cb" Enabled="false" runat="server" CssClass="form-control" Width="50%" />
                                        </div>
                                    </div>
                                    <div class="form-group" id="div_half" runat="server">
                                        <label class="col-md-3 control-label"></label>
                                        <div class="col-md-8">
                                            <asp:CheckBox ID="cbhalf" runat="server" Enabled="false" />&nbsp;
                                            <asp:Label ID="lbhalfday" runat="server" Text="Setengah Hari"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbpagi" runat="server" GroupName="half" Visible="false" Enabled="false" />
                                            <asp:Label ID="lbpagi" runat="server" Text="Pagi" Visible="false" />
                                            <asp:RadioButton ID="rbsiang" runat="server" GroupName="half" Visible="false" Enabled="false" />
                                            <asp:Label ID="lbsiang" runat="server" Text="Siang" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Jenis Cuti</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddl_type" runat="server" CssClass="form-control" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="div_upload_doc" runat="server">
                                        <label class="col-md-3 control-label">Lampiran</label>
                                        <div class="col-md-8">
                                            <asp:Literal ID="medical" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Staff Pengganti</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddl_replacement_staff" runat="server" CssClass="form-control" Enabled="false">
                                                <asp:ListItem Value="" Text="-- Pilih Karyawan --" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Catatan</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtremarks" TextMode="MultiLine" runat="server" CssClass="form-control" Enabled="false" />
                                        </div>
                                    </div>
                                    <div class="form-group" id="div_hrdremarks" runat="server" visible="false">
                                        <label class="col-md-3 control-label">
                                            Catatan<br />
                                            (HRD)</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="hrdremarks" TextMode="MultiLine" runat="server" CssClass="form-control" autocomplete="off" />
                                            <asp:Literal ID="hrdremarksby" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group" style="text-align: center !important; width: 100%;">
                                        <label class="col-md-3 control-label">&nbsp;</label>
                                        <div class="col-md-8">
                                            <div id="div_approve" visible="false" runat="server" style="float: left">
                                                <asp:Button ID="Approve" runat="server" Style="border: 1px solid #10c469 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #10c469 !important; border-radius: 2px; padding: 6px 14px;" Text="Review" name="Review" OnClick="BtnReview" AccessKey="s" />
                                            </div>
                                            <div id="div_reject" visible="false" runat="server" style="float: left">
                                                <asp:Button ID="Reject" runat="server" Style="border: 1px solid #ff5b5b !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #ff5b5b !important; border-radius: 2px; padding: 6px 14px;" Text="Reject" name="Reject" OnClick="BtnReject" AccessKey="s" />
                                            </div>
                                            <div style="float: left">
                                                <asp:Button ID="Back" runat="server" Style="border: 1px solid #5b69bc !important; background-color: rgba(91, 105, 188, 0.15) !important; color: #5b69bc !important; border-radius: 2px; padding: 6px 14px; cursor: pointer;" Text="Back" name="Back" OnClick="BtnBack" AccessKey="s" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
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

                                    <div>
                                        <hr />
                                    </div>

                                    <div class="profile-info-detail" style="text-align: left;">
                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold" id="div_spv" runat="server" visible="false">Supervisor</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13" id="h4_spv" runat="server" visible="false">: <asp:Literal ID="spv" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>


                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold" id="div_spv_remarks" runat="server" visible="false">Remarks</h4>
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <h4 class="text-muted m-b-5 font-13" id="h4_spv_remarks" runat="server" visible="false">
                                                    <asp:TextBox ID="spv_remarks" runat="server" TextMode="MultiLine" Enabled="false" CssClass="form-control"></asp:TextBox></h4>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="profile-info-detail" style="text-align: left;">
                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold" id="div_lm" runat="server" visible="false">HRD</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13" id="h4_lm" runat="server" visible="false">: <asp:Literal ID="lm" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>


                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold" id="div_lm_remarks" runat="server" visible="false">Remarks</h4>
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <h4 class="text-muted m-b-5 font-13" id="h4_lm_remarks" runat="server" visible="false">
                                                    <asp:TextBox ID="lm_remarks" runat="server" TextMode="MultiLine" Enabled="false" CssClass="form-control"></asp:TextBox></h4>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="profile-info-detail" style="text-align: left;">
                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold" id="div_cancel" runat="server" visible="false">Cancel</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13" id="h4_cancel" runat="server" visible="false">: <asp:Literal ID="cancel" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>


                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold" id="div_cancel_remarks" runat="server" visible="false">Remarks</h4>
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <h4 class="text-muted m-b-5 font-13" id="h4_cancel_remarks" runat="server" visible="false">
                                                    <asp:TextBox ID="cancel_remarks" runat="server" TextMode="MultiLine" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </h4>
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
<script>
    function HideLabel() {
        document.getElementById('<%= note.ClientID %>').style.display = "none";
        document.getElementById('<%= note.ClientID %>').appendChild = "";
    }
    setTimeout("HideLabel();", 30000);
</script>
</html>
