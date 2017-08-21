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
    <title>Gulali HRIS - User >> Data Pribadi >> Lembur >> Detail</title>
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
                    <h4 class="page-title">Detail Lembur</h4>
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
                                                <label class="col-md-3 control-label">Bulan Periode</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="date_periode" runat="server" CssClass="form-control" placeholder="Periode" disabled="disabled" />
                                                </div>
                                                <div class="col-md-3">
                                                </div>
                                            </div>
                                        </div>
                                        <div id="datatable-buttons_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                                            <div style="padding: 10px !important; float: right;">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="5" id="tbl_padding" runat="server" visible="false">
                                                    <tr>
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
                                                        <td style="text-align: center;">showing
                                                    <%--<input name="pagesum" type="text" id="textfield5" size="5" onKeyPress="checkEnter2(event,1,'',this.value,'');" value="20"  style="text-align:center;"/>--%>
                                                            <asp:TextBox ID="pagesum" runat="server" AutoPostBack="True" Width="25px" Text="10" OnTextChanged="page_enter" />
                                                            per page
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="clear: both;">&nbsp;</div>
                                            <table id="datatable-editable" class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid" aria-describedby="datatable-buttons_info" style="width: 100%">
                                                <thead>
                                                    <tr role="row">
                                                        <th style="text-align: center;">No</th>
                                                        <th style="text-align: center;">Deskripsi Lembur</th>
                                                        <th style="text-align: center;">Tanggal</th>
                                                        <th style="text-align: center;">Jam Mulai</th>
                                                        <th style="text-align: center;">Jam Selesai</th>
                                                        <th style="text-align: center;">Total Jam</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="table" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="form-horizontal" role="form" runat="server">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Catatan</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_desc" class="form-control" runat="server" placeholder="Catatan" TextMode="MultiLine" disabled="disabled"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group" style="text-align: center !important; width: 100%;">
                                                <label class="col-md-3 control-label">&nbsp;</label>
                                                <div class="col-md-8">
                                                    <div style="float: left">
                                                        <asp:Button ID="Back" runat="server" Style="border: 1px solid #5b69bc !important; background-color: rgba(91, 105, 188, 0.15) !important; color: #5b69bc !important; border-radius: 2px; padding: 6px 14px; cursor: pointer;" Text="Back" name="Back" OnClick="BtnBack" AccessKey="s" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Back" />
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
                                                <h4 class="text-muted m-b-5 font-13">:
                                                    <asp:Literal ID="full_name" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>

                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold">Departemen</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13">:
                                                    <asp:Literal ID="department" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>

                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold">Divisi</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13">:
                                                    <asp:Literal ID="division" runat="server"></asp:Literal></h4>
                                            </div>
                                        </div>

                                        <div style="width: 100%; float: left;">
                                            <div style="float: left; width: 30%;">
                                                <h4 class="m-b-5 font-13 font-bold">Jumlah Jam Lembur</h4>
                                            </div>
                                            <div style="float: left;">
                                                <h4 class="text-muted m-b-5 font-13">:
                                                    <asp:Literal ID="lembur_balanced" runat="server"></asp:Literal></h4>
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
                                                <h4 class="text-muted m-b-5 font-13" id="h4_spv" runat="server" visible="false">:
                                                    <asp:Literal ID="spv" runat="server"></asp:Literal></h4>
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
                                                <h4 class="text-muted m-b-5 font-13" id="h4_lm" runat="server" visible="false">:
                                                    <asp:Literal ID="lm" runat="server"></asp:Literal></h4>
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
                                                <h4 class="text-muted m-b-5 font-13" id="h4_cancel" runat="server" visible="false">:
                                                    <asp:Literal ID="cancel" runat="server"></asp:Literal></h4>
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
