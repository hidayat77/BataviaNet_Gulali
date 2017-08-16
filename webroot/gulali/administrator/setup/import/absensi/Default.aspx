<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_general" %>

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
    <title>Gulali HRIS - Setup >> Import</title>
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
                        <h4 class="page-title">Import</h4>
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
                                                <asp:Literal ID="tab_datakaryawan" runat="server"></asp:Literal>
                                            </li>
                                            <li class="active">
                                                <asp:Literal ID="tab_absensi" runat="server"></asp:Literal>
                                            </li>
                                        </ul>
                                        <div style="padding: 10px !important;">
                                            <!-- isi -->
                                            <!-- Menu Import -->
                                            <div>
                                                <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-lg-7">
                                                            <div id="Div1" class="form-horizontal" role="form" runat="server">
                                                                <div class="form-group">
                                                                    <label class="col-md-4 control-label">File Excel</label>
                                                                    <div class="col-md-8">
                                                                        <asp:FileUpload ID="fileUpl" runat="server"></asp:FileUpload>
                                                                        <%--<input type="file" id="file" runat="server" class="txt" name="file" />--%>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-4 control-label">&nbsp;</label>
                                                                    <div class="col-md-8">
                                                                        <div class="inputwrapperbutton" style="width: 95%;">
                                                                            <asp:Button ID="save" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Save" name="Save" OnClick="save_Click" AccessKey="s" Visible="False" />
                                                                            <asp:Button ID="clean" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Clean" name="Clean" OnClick="clean_Click" AccessKey="s" Visible="False" />

                                                                            <asp:Button ID="import" runat="server" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Import" name="Import" OnClick="import_Click" AccessKey="s" />
                                                                            <asp:Button ID="back" runat="server" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Cancel" name="Cancel" OnClick="back_Click" AccessKey="s" />
                                                                            <%--<asp:Button ID="Cancel" runat="server" Text="Cancel" name="cancel" AccessKey="s"
                                                                    OnClick="Cancel_Click" />--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="save" />
                                                        <asp:PostBackTrigger ControlID="clean" />
                                                        <asp:PostBackTrigger ControlID="Import" />
                                                        <asp:PostBackTrigger ControlID="back" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row" style="overflow: auto;">
                                <div class="col-sm-12">
                                    <asp:Literal ID="table" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Literal ID="lable" runat="server" />
                                    <asp:Literal ID="litHdn" runat="server" Visible="False" />
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