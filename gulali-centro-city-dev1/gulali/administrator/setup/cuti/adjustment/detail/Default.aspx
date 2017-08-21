<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_cuti" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setup >> Cuti</title>
    <ucmeta:meta ID="meta" runat="server" />
    <script type="text/javascript">
        function changeStatus(id, x, y) {
            var url = "changeStatus.aspx?id=" + id + "&x=" + x + "&y=" + y;
            window.location = url;
        }
    </script>
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
                        <h4 class="page-title">Detail Cuti Periode
                            <asp:Literal ID="periode_show" runat="server" /></h4>
                    </div>
                </div>
                <!-- end row -->
                <div class="card-box">
                    <%--<h4 class="header-title m-t-0 m-b-30">Data Pribadi</h4>--%>
                    <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row" id="div_table_show" runat="server">
                                <div class="col-lg-7">
                                    <div class="form-horizontal" role="form" runat="server">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status</label>
                                            <div class="col-md-8">
                                                <div class="col-md-2">
                                                    <div class="radio" style="padding-left: 10px;">
                                                        <asp:RadioButton ID="radio_all" runat="server" GroupName="radio_terpilih" Text="Semua" Checked="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="radio">
                                                        <asp:RadioButton ID="radio_increase" runat="server" GroupName="radio_terpilih" Text="Penambahan" />
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="radio">
                                                        <asp:RadioButton ID="radio_decrease" runat="server" GroupName="radio_terpilih" Text="Pengurangan" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">View Data</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddl_selected" class="form-control" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="-- Semua --" />
                                                    <asp:ListItem Value="1" Text="Karyawan" />
                                                    <asp:ListItem Value="2" Text="Divisi" />
                                                    <asp:ListItem Value="3" Text="Departemen" />
                                                    <asp:ListItem Value="4" Text="Dibuat Oleh" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="Search" class="form-control" runat="server" placeholder="Cari" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date Periode</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="tanggal_mulai" class="form-control" runat="server" placeholder="Tanggal Mulai" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="tanggal_selesai" class="form-control" runat="server" placeholder="Tanggal Selesai" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">&nbsp;</label>
                                            <div class="col-md-8">
                                                <div class="inputwrapperbutton" style="width: 95%;">
                                                    <asp:Button ID="Btn_Search" runat="server" class="btn btn-primary" Text="Cari" name="submit" AccessKey="s" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both;">&nbsp;</div>
                                <div style="padding: 10px !important; float: right;" id="div_pagging" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>
                                            <%--<td style="padding-right: 10px;">
                                        <asp:Button ID="ExportExcel_New" Style="outline: none; color: #033363;"
                                            class="btn btn-primary" runat="server" Text="Export Excel" name="ExportExcel" OnClick="BtnExportExcel_New" AccessKey="s" />
                                    </td>
                                    <td style="padding-right: 10px;">
                                        <!-- search panel -->
                                        <div>
                                            <div style="float: left;">
                                                <asp:DropDownList class="form-control" ID="ddl_search" runat="server">
                                                    <asp:ListItem Value="Employee_Full_Name" Text="Nama" Selected="True" />
                                                    <asp:ListItem Value="Employee_Address" Text="Alamat" />
                                                    <asp:ListItem Value="Department_Name" Text="Departemen" />
                                                    <asp:ListItem Value="Division_Name" Text="Divisi" />
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left;">
                                                <asp:TextBox ID="search_text" class="form-control" runat="server" AutoPostBack="True" AutoComplete="off" placeholder="Cari" OnTextChanged="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </td>--%>

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
                                            <td style="text-align: center;" id="td_hide" runat="server" visible="false">showing
                                                        <asp:Literal ID="pagesum" Text="10" runat="server"></asp:Literal>
                                                per page
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Literal ID="table" runat="server" />
                                <asp:Literal ID="lable" runat="server" />
                            </div>










                            <div class="row" id="div_detail_show" runat="server" visible="false">
                                <div class="col-lg-7">
                                    <div class="form-horizontal" role="form" runat="server">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Nama Karyawan</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txt_employee" class="form-control" runat="server" placeholder="Cari" autocomplete="off" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Divisi</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txt_division" class="form-control" runat="server" placeholder="Cari" autocomplete="off" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Department</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txt_department" class="form-control" runat="server" placeholder="Tanggal Mulai" autocomplete="off" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Value</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txt_value" class="form-control" runat="server" placeholder="Cari" autocomplete="off" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Dibuat Oleh</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txt_create_by" class="form-control" runat="server" placeholder="Cari" autocomplete="off" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Tanggal</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txt_date" class="form-control" runat="server" placeholder="Cari" autocomplete="off" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txt_status" class="form-control" runat="server" placeholder="Cari" autocomplete="off" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Catatan</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txt_desc" class="form-control" runat="server" placeholder="Catatan" TextMode="MultiLine" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">&nbsp;</label>
                                            <div class="col-md-8">
                                                <div class="inputwrapperbutton" style="width: 95%;">
                                                    <asp:Button ID="Button_back" runat="server" class="btn btn-primary" Text="Kembali" name="submit" AccessKey="s" OnClick="Button_back_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Btn_Search" />
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
<ucscript:script ID="script" runat="server" />
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
