<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_perusahaan" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setup >> Perusahaan</title>
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
                        <h4 class="page-title">Perusahaan</h4>
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
                                            <li class="active">
                                                <asp:Literal ID="tab_perusahaan" runat="server"></asp:Literal>
                                            </li>
                                            <li class="">
                                                <asp:Literal ID="tab_organisasi" runat="server"></asp:Literal></li>
                                            <li class="">
                                                <asp:Literal ID="tab_posisi" runat="server"></asp:Literal></li>
                                            <li class="">
                                                <asp:Literal ID="tab_peraturan_kantor" runat="server"></asp:Literal></li>
                                        </ul>
                                        <div class="tab-content b-0 m-b-0">
                                            <div class="row">
                                                <div style="padding: 10px !important;">
                                                    <asp:Label ID="link_added" runat="server" />
                                                    <div class="col-lg-6">
                                                        <div class="form-horizontal" role="form" runat="server">
                                                            <div class="form-group">
                                                                <label class="col-md-4 control-label">Nama Perusahaan</label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="company_name" runat="server" autocomplete="off" placeholder="Company Name" class="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-4 control-label">Tentang Perusahaan</label>
                                                                <div class="col-md-8">
                                                                    <FCKeditorV2:FCKeditor ID="about" runat="server" Height="400px" Width="600px"></FCKeditorV2:FCKeditor>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-4 control-label">Logo</label>
                                                                <div class="col-md-8">
                                                                    <asp:Image ID="logo_gulali_img" runat="server" Style="width: 50%; background-color: #033363; height: auto" />
                                                                    <br />
                                                                    <br />
                                                                    <asp:FileUpload ID="logo_gulali_file_upload" runat="server"></asp:FileUpload>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-4 control-label">Logo gelap</label>
                                                                <div class="col-md-8">
                                                                    <asp:Image ID="logo_dark_img" runat="server" Style="width: 50%; background-color: #033363; height: auto" />
                                                                    <br />
                                                                    <br />
                                                                    <asp:FileUpload ID="logo_dark_file_upload" runat="server"></asp:FileUpload>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-4 control-label">Ikon</label>
                                                                <div class="col-md-8">
                                                                    <asp:Image ID="ikon_img" runat="server" Style="width: 30%; background-color: #033363; height: auto" />
                                                                    <br />
                                                                    <br />
                                                                    <asp:FileUpload ID="ikon_file_upload" runat="server"></asp:FileUpload>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-4 control-label">Footer</label>
                                                                <div class="col-md-8">
                                                                    <FCKeditorV2:FCKeditor ID="footer_content" runat="server" Height="400px" Width="600px"></FCKeditorV2:FCKeditor>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-4 control-label">&nbsp;</label>
                                                                <div class="col-md-8">
                                                                    <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="btnSubmit" AccessKey="s" />
                                                                    <asp:Literal ID="href_cancel" runat="server"></asp:Literal>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
