<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_usr_management_detail_view" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Setup >> User Management >> Detail</title>
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
                        <div class="btn-group pull-right m-t-15">
                            <asp:Literal ID="link_back" runat="server"></asp:Literal>
                        </div>
                    <h4 class="page-title">Detail User</h4>
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
                                    <div class="form-horizontal" role="form">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Username</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="username" class="form-control" disabled="disabled" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Role</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddl_groupCMS" class="form-control" disabled="disabled" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Nama Karyawan</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddl_Employee_Name" class="form-control" runat="server" disabled="disabled">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Block User</label>
                                            <div class="col-md-8">
                                                <asp:CheckBox ID="cb_block" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group" id="div_isAdmin" runat="server" visible="false">
                                            <label class="col-md-4 control-label">isAdmin ?</label>
                                            <div class="col-md-8">
                                                <asp:CheckBox ID="isAdmin" runat="server" />
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
