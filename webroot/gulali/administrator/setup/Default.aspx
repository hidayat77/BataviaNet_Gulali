<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Setup" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Administrator - Setup Page</title>
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
                    <h4 class="page-title">Setup</h4>
                </div>
            </div>
            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/dashboard.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="dashboard/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Dashboard</a>
                    </div>
                </div>
            </div>

            <!--<div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/announcement.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="pengumuman/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Pengumuman</a>
                    </div>
                </div>
            </div>-->

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/company.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="perusahaan/perusahaan/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Perusahaan</a>
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/employee.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="data-karyawan/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Data Karyawan</a>
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/leave.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="cuti/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Cuti</a>
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/holiday.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="hari-libur-nasional/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Hari Libur</a>
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/payroll.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="payroll/payroll-component/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Payroll</a>
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/user.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="user-management/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 8px; color: white !important;">User Management</a>
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/general.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="general/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">General</a>
                    </div>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/import.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="import/data-karyawan/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Import</a>
                    </div>
                </div>
            </div>
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>
</html>

