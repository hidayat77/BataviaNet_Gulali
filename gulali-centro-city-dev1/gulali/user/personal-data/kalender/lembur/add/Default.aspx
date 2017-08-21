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
    <title>Gulali HRIS - User >> Data Pribadi >> Lembur >> Add</title>
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
                    <div class="btn-group pull-right m-t-15">
                        <%--<asp:Literal ID="link_href" runat="server"></asp:Literal>--%>
                    </div>
                    <h4 class="page-title">Tambah Lembur</h4>
                </div>
            </div>
            <div class="row">
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="sm" runat="server" />
                    <div class="col-sm-12">
                        <asp:Label ID="note" runat="server"></asp:Label>
                        <div class="card-box">
                            <div class="row">
                                <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-horizontal" role="form" runat="server">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Bulan Periode</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="date_periode" runat="server" CssClass="form-control" placeholder="Periode" AutoPostBack="true" OnTextChanged="date_periode_TextChanged" />
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
                                                        <th style="text-align: center;">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="table" runat="server" />
                                                    <tr role="row">
                                                        <td style="text-align: center; width:5%">&nbsp;</td>
                                                        <td style="text-align: center; width:50%">
                                                            <asp:TextBox ID="txt_desk_lembur" runat="server" CssClass="form-control" placeholder="Deskripsi Lembur" AutoPostBack="true" autocomplete="off"/>
                                                        </td>
                                                        <td style="text-align: center; width:7%;">
                                                            <asp:DropDownList ID="ddl_day" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: center; width:10%">
                                                            <asp:TextBox ID="jam_mulai" runat="server" class="form-control" placeholder="Jam"/>
                                                        </td>
                                                        <td style="text-align: center; width:10%">
                                                            <asp:TextBox ID="jam_selesai" runat="server" class="form-control" placeholder="Jam" OnTextChanged="jam_selesai_TextChanged"/>
                                                        </td>
                                                        <td style="text-align: center; width:10%">
                                                            <asp:TextBox ID="jam_total" runat="server" class="form-control" placeholder="Jam" disabled="disabled"/>
                                                        </td>
                                                        <td style="text-align: center; width:8%">
                                                            <asp:Button Style="border: 1px solid #10c469 !important; background-color: #f4f8fb !important; color: #10c469 !important; border-radius: 2px; padding: 6px 14px;" ID="Submit_rows" runat="server" text="+" OnClick="Submit_rows_Click"/>
                                                            <%--<a style="font-size:25px; padding-top:5px;"><i class="fa fa-plus" onclick="" style="color:#10c469;cursor:pointer;"></i></a>--%>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>



                                        <div class="form-horizontal" role="form" runat="server">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Catatan</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txt_desc" class="form-control" runat="server" placeholder="Catatan" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">&nbsp;</label>
                                                <div class="col-md-8">
                                                    <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="Submit_Click" AccessKey="s" />
                                                    <asp:Button ID="Cancel" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px; cursor:pointer;" Text="Batal" name="cancel" OnClick="Cancel_Click" AccessKey="s" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Submit_rows" />
                                        <asp:PostBackTrigger ControlID="Submit" />
                                        <asp:PostBackTrigger ControlID="Cancel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <!-- end row -->
                        </div>
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
    function pageLoad() {
        $('#date_periode').unbind();
        $('#date_periode').datepicker({
            format: 'MM-yyyy',
            viewMode: "months",
            minViewMode: "months",
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
</script>
</html>
