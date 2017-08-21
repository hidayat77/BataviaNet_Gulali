<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_laporan" %>

<%@ Register Src="~/usercontrol/meta.ascx" TagName="meta" TagPrefix="ucmeta" %>
<%@ Register Src="~/usercontrol/header.ascx" TagName="header" TagPrefix="ucheader" %>
<%@ Register Src="~/usercontrol/menu.ascx" TagName="menu" TagPrefix="ucmenu" %>
<%@ Register Src="~/usercontrol/footer.ascx" TagName="footer" TagPrefix="ucfooter" %>

<!DOCTYPE html>
<html>
<head>
    <title>Gulali HRIS - Administrator - Report Page</title>
    <ucmeta:meta ID="meta" runat="server" />
</head>
<body>
    <!-- Navigation Bar-->
    <header id="topnav">
        <ucheader:header ID="header" runat="server" />
        <ucmenu:menu ID="menu" moduleMenu="laporan" runat="server" />
    </header>
    <!-- End Navigation Bar-->
    <div class="wrapper">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <h4 class="page-title">Laporan</h4>
                </div>
            </div>

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/employee.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="karyawan/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Karyawan</a>
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
                        <img src="/assets/images/icons/payroll.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="payroll/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Payroll</a>
                    </div>
                </div>
            </div>

            <!--<div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/SPT.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="e-spt/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">e-SPT</a>
                    </div>
                </div>
            </div>-->

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/payslip.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="slip-gaji/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Slip Gaji</a>
                    </div>
                </div>
            </div>

            <!--<div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/recruitment.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="recruitment/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Recruitment</a>
                    </div>
                </div>
            </div>-->

            <!--<div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/travel.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="travel/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Travel</a>
                    </div>
                </div>
            </div>-->

            <div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/log.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="log-aktifitas/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 18px; color: white !important;">Log Aktifitas</a>
                    </div>
                </div>
            </div>

            <%--<div class="col-md-2">
                <div class="text-center card-box">
                    <div>
                        <img src="/assets/images/icons/user.png" class="img-circle thumb-xl img-thumbnail m-b-10" alt="profile-image">
                        <a href="user-management/" style="background-color: #59a9f8 !important; border: 1px solid #59a9f8 !important; border-radius: 2em; padding: 6px 8px; color: white !important;">User Management</a>
                    </div>
                </div>
            </div>--%>
            <ucfooter:footer ID="footer" runat="server" />
        </div>
        <!-- end container -->
    </div>
</body>
</html>

