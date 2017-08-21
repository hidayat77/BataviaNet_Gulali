<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_Test.aspx.cs" Inherits="_DefaultTest" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Dashboard Page</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
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
                    <h4 class="page-title">Dashboard HRD</h4>

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
                                    <br />
                                </div>
                                <div>
                                    <p> Test Pakai data User Devita (Emp_Id = 3)</p>
                                    <asp:Button ID="btnTest1" runat="server" OnClick="btnTest1_Click" Text="Button" />
                                    <br />
                                    <br />
                                    <asp:Literal ID="lit_test1" runat="server"></asp:Literal>

                                    <br />

                                    <br />
                                    <asp:Button ID="btnTest2" runat="server" OnClick="btnTest2_Click" Text="Button" />
                                    <br />
                                    <br />
                                    <asp:Literal ID="lit_test2" runat="server"></asp:Literal>

                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="card-box" style="overflow: auto">
                            <div class="col-sm-12">
                                <div class="panel-group m-b-0" id="accordion1" role="tablist"
                                    aria-multiselectable="true">
                                    <div class="panel panel-default bx-shadow-none" id="div_hari_libur" runat="server">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion1" href="#faq3">
                                                    <asp:Literal ID="title_hari_libur" runat="server"></asp:Literal>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="faq3" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <asp:Literal ID="data_hari_libur" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default bx-shadow-none" id="div_peraturan_kantor" runat="server">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a role="button" data-toggle="collapse" data-parent="#accordion1" href="#faq1">
                                                    <asp:Literal ID="title_peraturan_kantor" runat="server"></asp:Literal>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="faq1" class="panel-collapse collapse in">
                                            <div class="panel-body">
                                                <asp:Literal ID="data_peraturan_kantor" runat="server"></asp:Literal>
                                                <asp:Literal ID="modal" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-default bx-shadow-none" id="div_pengumuman_kantor" runat="server">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse"
                                                    data-parent="#accordion1" href="#faq2">
                                                    <asp:Literal ID="title_pengumuman" runat="server"></asp:Literal>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="faq2" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <asp:Literal ID="data_pengumuman" runat="server"></asp:Literal>
                                                <asp:Literal ID="modal2" runat="server"></asp:Literal>
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
<ucscript:script ID="script" runat="server" />
</form>
</html>