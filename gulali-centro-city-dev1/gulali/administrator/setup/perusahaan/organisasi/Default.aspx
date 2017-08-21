<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_organisasi" %>

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
    <title>Gulali HRIS - Setup >> Organisasi >> Department</title>
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
                        <h4 class="page-title">Organisasi</h4>
                    </div>
                </div>
                <!-- end row -->

                <div class="row">
                    <div class="col-sm-12">
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div id="basicwizard" class=" pull-in">
                                        <ul>
                                            <li class="">
                                                <asp:Literal ID="tab_perusahaan" runat="server"></asp:Literal>
                                            </li>
                                            <li class="active">
                                                <asp:Literal ID="tab_organisasi" runat="server"></asp:Literal></li>
                                            <li class="">
                                                <asp:Literal ID="tab_posisi" runat="server"></asp:Literal></li>
                                            <li class="">
                                                <asp:Literal ID="tab_peraturan_kantor" runat="server"></asp:Literal></li>
                                        </ul>
                                        <div style="clear: both;">&nbsp;</div>
                                        <div style="padding: 10px !important;">
                                            <div class="col-sm-1">
                                                <div class="div-button-link">
                                                    <a href="add/" class="btn btn-primary">Tambah</a>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="div-button-link">
                                                    <a href="/" class="btn btn-primary">Struktur Organisasi
                                                    </a>
                                                </div>
                                            </div>
                                            <div style="clear: both;">&nbsp;</div>
                                            <asp:Literal ID="table" runat="server" />
                                            <asp:Literal ID="note" runat="server" />
                                        </div>
                                    </div>
                                    <i>*Klik nama departemen untuk menambah divisi</i>
                                    <br />
                                    <br />
                                </div>

                            </div>
                        </div>
                    </div>
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
