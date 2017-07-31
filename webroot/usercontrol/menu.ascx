<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu.ascx.cs" Inherits="usercontrol_menu" %>
<div class="navbar-custom">
    <div class="container">
        <div id="navigation2">
            <!-- Navigation Menu-->
            <ul class="navigation-menu">
                <asp:Literal ID="dashboard" runat="server"></asp:Literal>
                <asp:Literal ID="data_karyawan" runat="server"></asp:Literal>
                <asp:Literal ID="kalender" runat="server"></asp:Literal>
                <asp:Literal ID="keuangan" runat="server"></asp:Literal>
                <asp:Literal ID="laporan" runat="server"></asp:Literal>
                <asp:Literal ID="setup" runat="server"></asp:Literal>
                <asp:Literal ID="personal_data" runat="server"></asp:Literal>
                <asp:Literal ID="department" runat="server"></asp:Literal>
            </ul>
            <!-- End navigation menu  -->
        </div>
    </div>
</div>

<script>
    $(document).ready(function () {
        $('.navbar-toggle').click(function (a) {
            $('#navigation2').slideToggle();
            a.preventDefault;
        });
        $('.menu-down').click(function (a) {
            if ($(this).siblings().hasClass('menu-opened')) {
                $('.submenu').slideUp();
                $(this).siblings().removeClass('menu-opened');
            }
            else {
                $('.submenu').slideUp();
                $(this).siblings().slideToggle();
                $(this).siblings().addClass('menu-opened');
            }
            a.preventDefault;
        });
    });
</script>