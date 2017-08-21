<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setting_cuti_tambah" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setup >> Cuti >> Kurang</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body style="background: none;">
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="setup" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <div class="container">
                <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!-- Page-Title -->
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="btn-group pull-right m-t-15">
                                    <asp:Literal ID="link_href" runat="server"></asp:Literal>
                                </div>
                                <h4 class="page-title">Cuti Kurang</h4>
                            </div>
                        </div>
                        <!-- end row -->
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="card-box">
                                    <%--<h4 class="header-title m-t-0 m-b-30">Data Pribadi</h4>--%>
                                    <div class="row">
                                        <div class="col-lg-7">
                                            <div class="form-horizontal" role="form" runat="server">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Pilih Karyawan</label>
                                                    <div class="col-md-8">
                                                        <div class="btn-group">
                                                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Semua Karyawan" AccessKey="s" OnClick="selected_1" />
                                                            <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="Berdasarkan" AccessKey="s" OnClick="selected_2" />
                                                            <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="Terpilih" AccessKey="s" OnClick="selected_3" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--BERDASARKAN--%>
                                                <div class="form-group" id="Select_1" runat="server">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddl_department" class="form-control" runat="server" OnSelectedIndexChanged="selected_department" AutoPostBack="true">
                                                            <asp:ListItem Value="" Text="-- Pilih Departemen --" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="Select_2" runat="server">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddl_division" class="form-control" runat="server" OnSelectedIndexChanged="selected_division" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="Select_3" runat="server">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddl_employee" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%--BERDASARKAN--%>

                                                <%--TERPILIH--%>
                                                <div class="form-group" id="tr_radio" runat="server">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-8">
                                                        <div class="col-md-3">
                                                            <div class="radio" style="padding-left: 10px;">
                                                                <asp:RadioButton ID="radio_department" runat="server" GroupName="radio_terpilih" Text="Departemen" OnCheckedChanged="radio_1" AutoPostBack="true" Checked="true" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="radio">
                                                                <asp:RadioButton ID="radio_division" runat="server" GroupName="radio_terpilih" Text="Divisi" OnCheckedChanged="radio_2" AutoPostBack="true" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="radio">
                                                                <asp:RadioButton ID="radio_employee" runat="server" GroupName="radio_terpilih" Text="Karyawan" OnCheckedChanged="radio_3" AutoPostBack="true" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="tr_department" runat="server" visible="true">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txt_multiple_department" class="form-control" runat="server" placeholder="Pilih Departemen" TextMode="MultiLine" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnview_department" CssClass="btn btn-primary" runat="server" Text="Pilih Departemen" OnClick="btnview_Click_department" />
                                                    </div>
                                                </div>
                                                <div class="form-group" id="tr_division" runat="server" visible="true">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txt_multiple_division" class="form-control" runat="server" placeholder="Pilih Divisi" TextMode="MultiLine" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnview_division" CssClass="btn btn-primary" runat="server" Text="Pilih Divisi" OnClick="btnview_Click_division" />
                                                    </div>
                                                </div>
                                                <div class="form-group" id="tr_employee" runat="server" visible="true">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txt_multiple_employee" class="form-control" runat="server" placeholder="Pilih Karyawan" TextMode="MultiLine" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnview_employee" CssClass="btn btn-primary" runat="server" Text="Pilih Karyawan" OnClick="btnview_Click_employee" />
                                                    </div>
                                                </div>
                                                <%--TERPILIH--%>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Jumlah</label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txt_value" class="form-control" runat="server" placeholder="Jumlah Cuti" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Catatan</label>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txt_desc" class="form-control" runat="server" placeholder="Catatan" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-8">
                                                        <div class="inputwrapperbutton" style="width: 95%;">
                                                            <%--<asp:Button ID="Submit" runat="server" Text="Submit" name="submit" AccessKey="s"
                                                                    OnClick="Submit_Click" />
                                                            --%><asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" AccessKey="s" OnClick="Submit_Click" />

                                                            <asp:Button ID="Cancel" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Batal" name="cancel" AccessKey="s" OnClick="Cancel_Click" />
                                                            <%--<asp:Button ID="Cancel" runat="server" Text="Cancel" name="cancel" AccessKey="s"
                                                                    OnClick="Cancel_Click" />--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnview_department" />
                        <asp:PostBackTrigger ControlID="btnview_division" />
                        <asp:PostBackTrigger ControlID="btnview_employee" />
                        <asp:PostBackTrigger ControlID="Submit" />
                        <asp:PostBackTrigger ControlID="Cancel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <ucfooter:footer ID="footer" runat="server" />
    </form>
</body>
</html>
