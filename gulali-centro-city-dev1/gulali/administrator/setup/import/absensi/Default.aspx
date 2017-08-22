<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_admin_setup_general" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>--%>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>
<%@ Register Src="~/usercontrol/script.ascx" TagName="script" TagPrefix="ucscript" %>

<!DOCTYPE html>
<html>
<script type="text/javascript">

    function goToURL() {
        location.href = '/gulali/administrator/setup/import/data-karyawan/';
    };
</script>

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
                                    <div id="basicwizard2" class=" pull-in">
                                        <ul>
                                            <li class="active">
                                                <asp:Literal ID="tab_absensi" runat="server"></asp:Literal>
                                            </li>
                                            <li class="">
                                                <%--<asp:Literal ID="tab_datakaryawan" runat="server"></asp:Literal>--%>
                                                <a href="#DataKaryawan" onclick="goToURL(); return false;">DataKaryawan</a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div id="basicwizard" class="">

                                        <%--<div style="padding: 10px !important;">
                                            <asp:UpdatePanel runat="server" ID="updatePanel1" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="save" />
                                                    <asp:PostBackTrigger ControlID="clean" />
                                                    <asp:PostBackTrigger ControlID="Import" />
                                                    <asp:PostBackTrigger ControlID="back" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>--%>
                                        <div style="padding: 10px !important;">
                                            <div class="col-lg-7">
                                                <div id="Div1" class="form-horizontal" role="form" runat="server">
                                                    <div id="formfile" runat="server" class="form-group">
                                                        <label class="col-md-4 control-label">File Excel</label>
                                                        <div class="col-md-8">
                                                            <asp:FileUpload ID="fileUpl" runat="server"></asp:FileUpload>
                                                            <%-- <input type="file" id="fileUpl" runat="server" class="txt" name="file" />--%>
                                                        </div>
                                                    </div>
                                                    <td style="width: 15%; padding-right: 2%;">
                                                        <div class="inputwrapperbutton">
                                                        </div>
                                                        <div class="inputwrapperbutton">
                                                        </div>
                                                    </td>
                                                    <td style="width: 15%; padding-right: 2%;">
                                                        <div class="inputwrapperbutton">
                                                        </div>
                                                        <div class="inputwrapperbutton">
                                                        </div>
                                                    </td>
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">&nbsp;</label>
                                                        <div class="col-md-8">
                                                            <div class="inputwrapperbutton" style="width: 95%;">
                                                                <asp:Button ID="save" runat="server" OnClick="save_Click" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Save" name="Save" AccessKey="s" />
                                                                <asp:Button ID="clean" runat="server" OnClick="clean_Click" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Clean" name="Clean" AccessKey="s" />

                                                                <asp:Button ID="import" runat="server" OnClick="import_Click" Style="border: 1px solid #188ae2 !important; background-color: rgba(24, 138, 226, 0.15) !important; color: #188ae2 !important; border-radius: 2px; padding: 6px 14px;" Text="Import" name="Import" AccessKey="s" />
                                                                <asp:Button ID="back" runat="server" OnClick="back_Click" Style="border: 1px solid #f9c851 !important; background-color: rgba(249, 200, 81, 0.15) !important; color: #f9c851 !important; border-radius: 2px; padding: 6px 14px;" Text="Cancel" name="Cancel" AccessKey="s" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-5">
                                                <div class="btn-group pull-right m-t-15">
                                                    <asp:Literal ID="link_download_template" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="widgetcontent">
                                            <table cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="fail" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <h4 runat="server" id="h2StatusUpload" visible="false" style="border-top: 1px dashed gray; padding-top: 10px">Status Upload :</h4>
                                            <p class="feed">
                                                <asp:Label ID="feed" runat="server"></asp:Label>
                                            </p>
                                            <asp:Table ID="gagal" runat="server">
                                            </asp:Table>
                                            <asp:Literal ID="test" runat="server" />
                                            <!--widgetcontent-->

                                            <div style="padding-top: 10px">
                                                <strong>
                                                    <asp:Label ID="total1" runat="server" Font-Size="X-Large">Total Rows : </asp:Label>
                                                    <asp:Label ID="total2" runat="server" Font-Size="X-Large"></asp:Label>
                                                </strong>
                                            </div>

                                            <div style="text-align: right; padding-left: 50%; padding-bottom: 10px;" id="pagging" runat="server" visible="false">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="text-align: center;">
                                                            <asp:Button class="btn" ID="previous2" runat="server" OnClick="previous2_Click" Text="<<" />
                                                            <asp:Button class="btn" ID="previous" runat="server" OnClick="previous_Click" Text="<" />
                                                        </td>
                                                        <td style="text-align: center;">page

                                                                        <asp:TextBox ID="pagenum" runat="server" Width="25px" OnTextChanged="page_enter" />
                                                            of

                                                                        <asp:Literal ID="count_page" runat="server" />
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:Button class="btn" ID="next" runat="server" OnClick="next_Click" Text=">" />
                                                            <asp:Button class="btn" ID="next2" runat="server" OnClick="next2_Click" Text=">>" />
                                                        </td>
                                                        <td style="text-align: center;">showing

                                                                        <asp:TextBox ID="pagesum" runat="server" Width="25px" Text="50" OnTextChanged="page_enter" AutoPostBack="false" Enabled="false" />
                                                            per page
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="text-align: right; padding-left: 50%; padding-bottom: 10px;" id="paggingImp" runat="server" visible="false">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="text-align: center;">
                                                            <asp:Button class="btn" ID="prevImp2" runat="server" OnClick="prevImp2_Click" Text="<<" />
                                                            <asp:Button class="btn" ID="prevImp" runat="server" OnClick="prevImp_Click" Text="<" />
                                                        </td>
                                                        <td style="text-align: center;">page

                                                                        <asp:TextBox ID="pagenumImp" runat="server" Width="25px" OnTextChanged="page_enter_imp" />
                                                            of

                                                                        <asp:Literal ID="count_pageImp" runat="server" />
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:Button class="btn" ID="nextImp" runat="server" OnClick="nextImp_Click" Text=">" />
                                                            <asp:Button class="btn" ID="nextImp2" runat="server" OnClick="nextImp2_Click" Text=">>" />
                                                        </td>
                                                        <td style="text-align: center;">showing

                                                                        <asp:TextBox ID="pagesumImp" runat="server" Width="25px" Text="50" OnTextChanged="page_enter_imp" AutoPostBack="false" Enabled="false" />
                                                            per page
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="width: 100%; overflow-x: auto;">
                                                <asp:Literal runat="server" ID="table"></asp:Literal>
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