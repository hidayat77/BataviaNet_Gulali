<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_component_setting_edit" %>

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
    <title>Gulali HRIS - Setup >> Payroll >> Component >> Add</title>
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
                    <div class="col-lg-12">
                        <div class="card-box">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-horizontal" role="form" runat="server">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Code</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtcode" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Name</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtComponentname" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Kind</label>
                                            <div class="col-md-8" id="div_mode1" runat="server">
                                                <asp:DropDownList CssClass="form-control" ID="ddl_kind_mode1" runat="server">
                                                    <asp:ListItem Value="Pokok" Text="Pokok" Selected="True" />
                                                    <asp:ListItem Value="Tunjangan" Text="Tunjangan" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-8" id="div_mode2" runat="server" visible="false">
                                                <asp:TextBox ID="txtkind" class="form-control" runat="server" AutoComplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-md-8" id="div_mode3" runat="server" visible="false">
                                                <asp:TextBox ID="txtkind2" class="form-control" runat="server" AutoComplete="off" Text="Potongan" ReadOnly="true" disabled=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Type</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txttype" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="text-align: center">
                                            <asp:Button ID="PTKPSubmit" runat="server" class="btn btn-primary" Text="Simpan" name="submit" AccessKey="s" OnClick="Submit_Click" />
                                            <asp:Button ID="ParameterCancel" runat="server" class="btn btn-primary" Text="Batal" name="cancel" AccessKey="s" OnClick="Cancel_Click" />
                                        </div>
                                    </div>
                                </div>
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
