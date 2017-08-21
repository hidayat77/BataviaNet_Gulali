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
    <title>Gulali HRIS - Kalender >> Kehadiran - Add</title>
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
                    <h4 class="page-title">Tambah Jadwal</h4>
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
                                        <div class="clearfix">
                                            <div class="pull-right">
                                                <asp:Button ID="BtnSave" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="BtnSave_Click" AccessKey="s" />
                                                <asp:Button ID="BtnClear" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Bersihkan" name="cancel" OnClick="BtnClear_Click" AccessKey="s" />
                                            </div>
                                        </div>
                                        <hr>
                                        <%--<h4 class="header-title m-t-0 m-b-30">Data Pribadi</h4>--%>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div id="datatable-buttons_wrappers" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                                                    <asp:Calendar ID="Calendar" runat="server" DayNameFormat="Full" BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" Height="400px" NextPrevFormat="FullMonth" OnDayRender="Calendar_DayRender" Width="100%">
                                                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                        <WeekendDayStyle BackColor="#fcf8e3" ForeColor="#ff0000" />
                                                        <TodayDayStyle BackColor="#f3f3f3" />
                                                        <OtherMonthDayStyle ForeColor="#999999" />
                                                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" VerticalAlign="Bottom" CssClass="nextprev" ForeColor="#71b6f9" />
                                                        <DayHeaderStyle BackColor="#ebeff2" CssClass="dayheader" />
                                                        <TitleStyle BackColor="white" BorderColor="white" BorderWidth="2px" Font-Bold="True"
                                                            Font-Size="12pt" />
                                                    </asp:Calendar>
                                                </div>
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-2 control-label">Keterangan</label>
                                                        <div class="col-md-4" style="padding-top:10px;">
                                                            <asp:Literal ID="legend_calendar" runat="server"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6" style="padding-top: 40px;">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Nama Karyawan</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList class="form-control" ID="ddl_employee" runat="server" AutoPostBack="true">
                                                                <asp:ListItem Value="">-- Pilih Karyawan --</asp:ListItem>
                                                            </asp:DropDownList>
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
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Tambah Berdasarkan</label>
                                                        <div class="col-md-2">
                                                            <div class="radio">
                                                                <asp:RadioButton ID="radio_hari" runat="server" GroupName="rdr" Text="Perhari" OnCheckedChanged="radio_1" AutoPostBack="true" Checked="true" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="radio">
                                                                <asp:RadioButton ID="radio_periode" runat="server" GroupName="rdr" Text="Perperiode" OnCheckedChanged="radio_2" AutoPostBack="true" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="div_tgl_perhari" runat="server">
                                                        <label class="col-md-4 control-label">Tanggal</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="tanggal" class="form-control" runat="server" placeholder="Tanggal" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="div_tgl_perperiode" runat="server" visible="false">
                                                        <label class="col-md-4 control-label">Mulai</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="tanggal_mulai" class="form-control" runat="server" placeholder="Tanggal" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                        <label class="col-md-2 control-label">Selesai</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="tanggal_selesai" class="form-control" runat="server" placeholder="Tanggal" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
                                                            <asp:Button ID="Add" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Tambahkan" name="submit" OnClick="BtnSubmit" AccessKey="s" />
                                                            <%--<asp:Button ID="Back" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Batal" name="cancel" OnClick="BtnCancel" AccessKey="s" />--%>
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
                            <asp:PostBackTrigger ControlID="Add" />
                            <asp:PostBackTrigger ControlID="BtnSave" />
                            <asp:PostBackTrigger ControlID="BtnClear" />
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
        $('#tanggal').unbind();
        $('#tanggal').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });
        $('#tanggal_mulai').unbind();
        $('#tanggal_mulai').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });
        $('#tanggal_selesai').unbind();
        $('#tanggal_selesai').datepicker({
            format: 'dd-MM-yyyy',
            autoclose: true,
            todayHighlight: true
        });
    }
</script>
</html>

