﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_master_type_cuti_add" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setup >> Master Tipe Cuti >> Add</title>
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
                        <h4 class="page-title">Tambah Tipe Cuti</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Label ID="note" runat="server"></asp:Label>
                        <div class="card-box" style="min-height: 300px">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-horizontal" role="form" runat="server">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Tipe</label>
                                            <div class="col-md-8">
                                                <div style="float: left;">
                                                    <asp:TextBox ID="type" runat="server" autocomplete="off" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Jumlah</label>
                                            <div class="col-md-8">
                                                <div style="float: left;">
                                                    <asp:TextBox ID="value" runat="server" autocomplete="off" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Lama Bekerja</label>
                                            <div class="col-md-8">
                                                <div style="float: left;">
                                                    <asp:TextBox ID="long_working" runat="server" autocomplete="off" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">&nbsp;</label>
                                            <div class="col-md-8">
                                                <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="BtnSubmit" AccessKey="s" />
                                                <asp:Literal ID="href_cancel" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end col -->
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
    </form>
</body>
</html>