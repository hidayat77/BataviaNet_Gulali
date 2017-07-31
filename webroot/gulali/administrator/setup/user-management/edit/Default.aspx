<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_usr_management_edit" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setup >> User Management >> User Group >> Edit</title>
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
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="page-title">Edit User Group</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-horizontal" role="form" runat="server">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Username</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="username" class="form-control" runat="server" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">&nbsp;</label>
                                            <div class="col-md-8">
                                                <asp:Button ID="Submit" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Simpan" name="submit" OnClick="BtnSubmit" AccessKey="s" />
                                                <asp:Literal ID="href_cancel" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both;">&nbsp;</div>
                                <asp:Literal ID="table" runat="server" />
                                <asp:Literal ID="lable" runat="server" />
                            </div>
                        </div>
                    </div>
                    <!-- end col -->
                    <ucfooter:footer ID="footer" runat="server" />
                </div>
                <!-- end row -->
            </div>
        </div>
    </form>
</body>
</html>
