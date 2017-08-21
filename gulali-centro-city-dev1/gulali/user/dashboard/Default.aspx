<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Dashboard Page</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="dashboard" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <h4 class="page-title">Dashboard Staff</h4>

                <div class="col-sm-6">
                    <div class="bg-picture card-box">
                        <div class="profile-info-name">
                            <asp:Literal ID="link_foto" runat="server"></asp:Literal>
                            <div class="profile-info-detail" style="padding-bottom: 30px;">
                                <div style="float: left; width: 30%;">
                                    <h4 class="text-muted m-b-5 font-13 font-bold">Nama</h4>
                                    <h4 class="text-muted m-b-5 font-13 font-bold">Departemen</h4>
                                    <h4 class="text-muted m-b-5 font-13 font-bold">Divisi</h4>
                                    <h4 class="text-muted m-b-5 font-13 font-bold">Sisa Cuti</h4>
                                </div>
                                <div style="float: right; width: 70%;">
                                    <h4 class="text-muted m-b-5 font-13">:
                                        <asp:Literal ID="nama" runat="server"></asp:Literal></h4>
                                    <h4 class="text-muted m-b-5 font-13">:
                                        <asp:Literal ID="departemen" runat="server"></asp:Literal></h4>
                                    <h4 class="text-muted m-b-5 font-13">:
                                        <asp:Literal ID="divisi" runat="server"></asp:Literal></h4>
                                    <h4 class="text-muted m-b-5 font-13">:
                                        <asp:Literal ID="balance" runat="server"></asp:Literal></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-3">
                    <div class="card-box" style="overflow: auto">
                        <div class="col-sm-12">
                            <div class="panel-group m-b-0" id="accordion1" role="tablist"
                                aria-multiselectable="true">
                                <div class="panel panel-default bx-shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a role="button" data-toggle="collapse"
                                                data-parent="#accordion1" href="#faq1">Peraturan Kantor
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="faq1" class="panel-collapse collapse in">
                                        <div class="panel-body">
                                            Peraturan Kantor >> Isi
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default bx-shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse"
                                                data-parent="#accordion1" href="#faq2">Pengumuman
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="faq2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            Pengumuman >> Isi
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default bx-shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse"
                                                data-parent="#accordion1" href="#faq3">Hari Libur
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="faq3" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            Hari Libur >> Isi
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-3">
                    <div class="card-box" style="overflow: auto">
                        <div class="col-sm-12">
                            <div class="panel-group m-b-0" id="accordion2" role="tablist"
                                aria-multiselectable="true">
                                <div class="panel panel-default bx-shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a role="button" data-toggle="collapse"
                                                data-parent="#accordion2" href="#faq4">Cuti
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="faq4" class="panel-collapse collapse in">
                                        <div class="panel-body">
                                            Cuti >> Isi
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default bx-shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse"
                                                data-parent="#accordion2" href="#faq5">Surat Peringatan
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="faq5" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            Surat Peringatan >> Isi
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default bx-shadow-none">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a class="collapsed" role="button" data-toggle="collapse"
                                                data-parent="#accordion2" href="#faq6">Kontrak
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="faq6" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            Kontrak >> Isi
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>
</html>

