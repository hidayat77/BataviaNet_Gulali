<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_log_aktifitas_detail" %>

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
    <title>Gulali HRIS - Log Aktifitas > Detail</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" runat="server" />
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
                        <h4 class="page-title">Detail Log Aktifitas</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card-box p-b-0" style="padding-top: 0px !important;">
                            <div id="basicwizard" class=" pull-in">
                                <div class="tab-content b-0 m-b-0">
                                    <div class="row">

                                        <div>
                                            <div class="col-sm-6" style="float: left;">
                                                <div class="form-horizontal" role="form" runat="server">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Nama</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_nama_karyawan" class="form-control" ReadOnly="true" disabled="" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Username</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_username" class="form-control" ReadOnly="true" disabled="" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Role</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_role" class="form-control" ReadOnly="true" disabled="" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">IP</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_IP" class="form-control" ReadOnly="true" disabled="" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Host</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_Host" class="form-control" ReadOnly="true" disabled="" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" style="float: left;">
                                                <div class="form-horizontal" role="form" runat="server">

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Tanggal</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_date" class="form-control" ReadOnly="true" disabled="" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Aktifitas</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_Activity" class="form-control" ReadOnly="true" disabled="" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Keterangan</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txt_Description" class="form-control" ReadOnly="true" disabled="" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-horizontal" role="form" runat="server">
                                                <div>
                                                    <table class="table table-striped table-bordered dataTable no-footer dtr-inline" style="width: 100%; margin: 0px auto">
                                                        <tr>
                                                            <th id="td_head_before" runat="server">Before</th>
                                                            <th id="td_head_after" runat="server">After</th>
                                                        </tr>
                                                        <tr>
                                                            <td id="td_before" runat="server" style="padding:20px;">
                                                                <asp:Literal ID="before" runat="server"></asp:Literal></td>
                                                            <td id="td_after" runat="server" style="padding:20px;">
                                                                <asp:Literal ID="after" runat="server"></asp:Literal></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- end col -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
                <ucfooter:footer ID="footer" runat="server" />
            </div>
            <!-- end row -->
        </div>
    </form>
</body>
<ucscript:script ID="script" runat="server" />
</html>
