<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="usercontrol_header" %>
<div class="topbar-main">
    <div class="container">

        <!-- LOGO -->
        <div class="topbar-left">
            <asp:Literal ID="logo" runat="server"></asp:Literal>
        </div>
        <!-- End Logo container-->
        <div class="menu-extras">
            <ul class="nav navbar-nav navbar-right pull-right">
                <li>
                    <div class="notification-box">
                        <ul class="list-inline m-b-0">
                            <li style="font-size: 15px; color: #ffffff; display: block; line-height: 60px;">Welcome
                                <asp:Literal ID="name" runat="server"></asp:Literal>
                            </li>
                        </ul>
                    </div>
                </li>

                <li>
                    <!-- Notification -->
                    <div class="notification-box">
                        <ul class="list-inline m-b-0">
                            <li>
                                <a href="javascript:void(0)" class="right-bar-toggle">
                                    <i class="zmdi zmdi-notifications-none"></i>
                                </a>
                                <div class="noti-dot" id="dot_notif" runat="server" visible="false">
                                    <span class="dot"></span>
                                    <span class="pulse"></span>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <!-- End Notification bar -->
                </li>

                <li class="dropdown user-box">
                    <a href="" class="dropdown-toggle waves-effect waves-light profile " data-toggle="dropdown" aria-expanded="true">
                        <asp:Image ID="Image1" class="img-circle user-img" runat="server" />
                        <div class="user-status away"><i class="zmdi zmdi-dot-circle"></i></div>
                    </a>

                    <ul class="dropdown-menu">
                        <li><a href="/gulali/profile/"><i style="padding-right: 10px;" class="fa fa-user-o"></i>Profil</a></li>
                        <li><a href="/gulali/log-activity/"><i style="padding-right: 10px;" class="fa fa-pencil-square-o"></i>Log Aktifitas</a></li>
                        <li id="implementor" runat="server" visible="false"><a href="/gulali/setting/"><i style="padding-right: 10px;" class="fa fa-gear"></i>Setting</a></li>
                        <li><a href="/logout.aspx"><i style="padding-right: 10px;" class="fa fa-power-off"></i>Logout</a></li>
                    </ul>
                </li>
            </ul>
            <div class="menu-item">
                <!-- Mobile menu toggle-->
                <a class="navbar-toggle">
                    <div class="lines">
                        <span></span>
                        <span></span>
                        <span></span>
                    </div>
                </a>
                <!-- End mobile menu toggle-->
            </div>
        </div>

    </div>


    <%--NOTIFICATION--%>
    <div class="side-bar right-bar" id="sdbar">
        <a href="javascript:void(0);" class="right-bar-toggle">
            <i class="zmdi zmdi-close-circle-o"></i>
        </a>
        <h4 class="">Notifikasi</h4>
        <div class="notification-list nicescroll">
            <ul class="list-group list-no-border user-list">
                <asp:Literal ID="notifikasi" runat="server" />
            </ul>
            <h4 style="text-align: center !important; text-transform: none !important;"><i><a href="/gulali/notification/">Lihat Semua</a></i></h4>
        </div>

    </div>
    <%--NOTIFICATION--%>
</div>
<script>
    $('.right-bar-toggle').click(function (a) {
        $('#sdbar').toggleClass('sidebar-showed');
        if ($('#sdbar').hasClass('sidebar-showed')) {
            $('#sdbar').css('right', 0);
        }
        else {
            $('#sdbar').css('right', '-266px');
        }
    });
</script>
