<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_usr_management_detail_add" %>

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
    <title>Gulali HRIS - Setup >> User Management >> Add</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="setup" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title">Tambah User</h4>
                </div>
            </div>
            <div class="row">
                <form id="form1" runat="server">
                    <asp:ToolkitScriptManager ID="sm" runat="server" />
                    <div class="col-sm-12">
                        <asp:Label ID="note" runat="server"></asp:Label>
                        <div class="card-box">
                            <div class="row">
                                <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-lg-6">
                                            <div class="form-horizontal" role="form" runat="server">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Username</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="username" class="form-control" runat="server" placeholder="Username" autocomplete="off" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Role</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddl_groupCMS" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Nama Karyawan</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddl_Employee_Name" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Password</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="password" class="form-control" placeholder="Password" TextMode="password" runat="server" />
                                                        <asp:RequiredFieldValidator ID="passwordr" ControlToValidate="password" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Ulangi Password</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="confirm" class="form-control" placeholder="Ulangi Password" TextMode="password" runat="server" />
                                                        <asp:CompareValidator ControlToCompare="password" runat="server" ID="compare_pass"
                                                            ControlToValidate="confirm" Type="String" EnableClientScript="false" />
                                                        <asp:Label ID="Alert_y" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                    <div class="col-md-8">
                                                        <div class="checkbox">
                                                            <asp:CheckBox ID="isAdmin" runat="server" />
                                                            <label for="control-label">isAdmin ?</label>
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
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Submit" />
                                    </Triggers>
                                </asp:UpdatePanel>
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

