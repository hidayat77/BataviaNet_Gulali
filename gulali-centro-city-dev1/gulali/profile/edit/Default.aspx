<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_update_profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Profil Page - Edit</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title">Edit Profil</h4>
                </div>
            </div>
            <div class="row">
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="sm" runat="server" />
                    <div class="col-sm-12">
                        <asp:Label ID="note" runat="server"></asp:Label>
                        <div class="card-box">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-horizontal" role="form" runat="server">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Username</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="user_name" class="form-control" ReadOnly="true" disabled="" runat="server" placeholder="Username"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Nama</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="full_name" class="form-control" ReadOnly="true" disabled="" runat="server" placeholder="fullname"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">HP</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="phone_number" class="form-control" runat="server" placeholder="HP" disabled="disabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel runat="server" ID="update_profile" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <div class="col-md-4">&nbsp;</div>
                                                    <div class="col-md-8">
                                                        <div class="checkbox">
                                                            <asp:CheckBox ID="checkbox_password" runat="server" OnCheckedChanged="check_password" AutoPostBack="true" />
                                                            <label for="control-label">Ubah Sandi</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group tr_password" id="tr_password" runat="server" visible="false">
                                                    <label class="col-md-4 control-label">Password Baru</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="password" class="form-control" placeholder="Password Baru" TextMode="password" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group tr_conf_password" id="tr_conf_password" runat="server" visible="false">
                                                    <label class="col-md-4 control-label">Ulangi Password</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="confirm" class="form-control" placeholder="Ulangi Password" TextMode="password" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group" id="tr_old_password" runat="server" visible="false">
                                                    <label class="col-md-4 control-label">Password Lama</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="oldpassword" class="form-control" placeholder="Password Lama" TextMode="password" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-8">
                                                        <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="save_Click" AccessKey="s" />
                                                        <asp:Button ID="Back" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Batal" name="cancel" OnClick="BtnCancel" AccessKey="s" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="Submit" />
                                                <asp:PostBackTrigger ControlID="Back" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <!-- end col -->

                                <div class="col-lg-6">
                                    <div class="form-horizontal" role="form">
                                        <div style="width: 100%; text-align: center">
                                            <div style="width: 200px; margin-left: -100px; left: 50%; position: relative;">
                                                <asp:Image ID="Image1" class="img-thumbnail" runat="server" />
                                            </div>
                                            <div class="p-t-10" style="text-align: center;">
                                                <asp:FileUpload ID="Employee_Photo" runat="server" accept="image/*" Style="display: inline-block; background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;"></asp:FileUpload><br />
                                                <h4 class="text-muted m-b-5 font-13">*JPG/JPEG file, max 200kb</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end col -->
                            </div>
                            <!-- end row -->
                        </div>
                    </div>
                </form>
            </div>
            <!-- end row -->
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>
<script>
    function HideLabel() {
        document.getElementById('<%= note.ClientID %>').style.display = "none";
        document.getElementById('<%= note.ClientID %>').appendChild = "";
    }
    setTimeout("HideLabel();", 30000);
</script>
</html>

