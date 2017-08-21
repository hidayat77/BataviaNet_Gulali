<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_data_karyawan_history" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head id="head" runat="server">
    <title>Gulali HRIS - Setting >> Data Karyawan >> History</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="data-karyawan" runat="server" />
    </header>
    <form id="form" runat="server">
        <asp:ToolkitScriptManager ID="sm" runat="server" />
        <div class="wrapper">
            <div class="container">

                <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <div class="btn-group pull-right m-t-15">
                            <asp:Literal ID="link_href" runat="server"></asp:Literal>
                        </div>
                        <h4 class="page-title">History - <asp:Literal ID="nama_employee" runat="server"/></h4>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="text-center card-box">
                        <div>
                            <img src="/assets/images/icons/contract.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                            <asp:Literal ID="href_kontrak" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="text-center card-box">
                        <div>
                            <img src="/assets/images/icons/warning-letter.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                            <asp:Literal ID="href_surat_peringatan" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="text-center card-box">
                        <div>
                            <img src="/assets/images/icons/loan.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                            <asp:Literal ID="href_pinjaman" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="text-center card-box">
                        <div>
                            <img src="/assets/images/icons/salary.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                            <asp:Literal ID="href_gaji" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="text-center card-box">
                        <div>
                            <img src="/assets/images/icons/exit-clearance.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                            <asp:Literal ID="href_exit_clearance" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            <ucfooter:footer ID="footer" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
