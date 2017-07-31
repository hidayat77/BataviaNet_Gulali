<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_data_karyawan_history" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setting >> Data Karyawan >> History >> Kontrak >> Tambah</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="data-karyawan" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <div class="container">

                <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <div class="btn-group pull-right m-t-15">
                            <asp:Literal ID="link_href" runat="server"></asp:Literal>
                        </div>
                        <h4 class="page-title">Tambah Kontrak - <asp:Literal ID="nama_employee" runat="server"/></h4>
                    </div>
                </div>
                <div class="row">
                    <asp:UpdatePanel runat="server" ID="updatePanelTable" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-sm-12">
                                <div class="card-box">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-horizontal" role="form" runat="server">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Periode Kontrak</label>
                                                    <div class="col-md-8">
                                                        <div class="input-daterange input-group" id="date-range">
                                                            <span class="input-group-addon b-0">Dari</span>
                                                            <asp:TextBox ID="contract_start" runat="server" class="form-control" placeholder="Dari" />
                                                            <span class="input-group-addon b-0">Sampai</span>
                                                            <asp:TextBox ID="contract_end" runat="server" class="form-control" placeholder="Sampai" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Jabatan</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddl_jabatan" class="form-control" runat="server">
                                                            <asp:ListItem Value="" Text="-- Pilih Jabatan --" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Berkas</label>
                                                    <div class="col-md-8">
                                                        <asp:FileUpload ID="FileUpload_berkas" runat="server"></asp:FileUpload>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Catatan</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="remarks" class="form-control" runat="server" TextMode="MultiLine" placeholder="Catatan"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-8">
                                                        <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="Simpan" OnClick="BtnSubmit" AccessKey="s" />
                                                        <asp:Button ID="Back" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Batal" name="Batal" OnClick="BtnCancel" AccessKey="s" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- end col -->
                                    </div>
                                    <!-- end row -->
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Submit" />
                            <asp:PostBackTrigger ControlID="Back" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <ucfooter:footer ID="footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
<script>
    jQuery('#contract_start').datepicker({
        format: 'dd MM yyyy',
        autoclose: true,
        todayHighlight: true
    });

    jQuery('#contract_end').datepicker({
        format: 'dd MM yyyy',
        autoclose: true,
        todayHighlight: true
    });
</script>