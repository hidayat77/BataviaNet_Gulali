<%@ Control Language="C#" AutoEventWireup="true" CodeFile="meta.ascx.cs" Inherits="usercontrol_meta" %>

<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">

<link href="/assets/plugins/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet">

<!--Morris Chart CSS -->
<link rel="stylesheet" href="/assets/plugins/morris/morris.css">

<link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="/assets/css/core.css" rel="stylesheet" type="text/css" />
<link href="/assets/css/components.css" rel="stylesheet" type="text/css" />
<link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
<link href="/assets/css/pages.css" rel="stylesheet" type="text/css" />
<link href="/assets/css/menu.css" rel="stylesheet" type="text/css" />
<link href="/assets/css/responsive.css" rel="stylesheet" type="text/css" />
<link href="//assets/css/gulali.css" rel="stylesheet" type="text/css" />
<link href="/assets/css/gulali_page_setting.css" rel="stylesheet" type="text/css" />
<link href="/assets/plugins/timepicker/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css"/>

<script src="/assets/js/modernizr.min.js"></script>

<!-- popup foto -->
<link rel="stylesheet" href="/assets/plugins/magnific-popup/dist/magnific-popup.css"/>

<!-- jQuery  -->
<script src="/assets/js/jquery.min.js"></script>
<script src="/assets/js/bootstrap.min.js"></script>
<script src="/assets/js/detect.js"></script>
<script src="/assets/js/fastclick.js"></script>

<script src="/assets/js/jquery.slimscroll.js"></script>
<script src="/assets/js/jquery.blockUI.js"></script>
<script src="/assets/js/waves.js"></script>
<script src="/assets/js/wow.min.js"></script>
<script src="/assets/js/jquery.nicescroll.js"></script>
<script src="/assets/js/jquery.scrollTo.min.js"></script>
<script src="/assets/plugins/timepicker/bootstrap-timepicker.min.js"></script>
<script src="/assets/plugins/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
<script src="/assets/plugins/bootstrap-daterangepicker/daterangepicker.js"></script>

<!-- Modals -->
<link href="/assets/plugins/custombox/dist/custombox.min.css" rel="stylesheet">

<!-- KNOB JS -->
<!--[if IE]>
    <script type="text/javascript" src="/assets/plugins/jquery-knob/excanvas.js"></script>
    <![endif]-->
<script src="/assets/plugins/jquery-knob/jquery.knob.js"></script>

<!--Morris Chart-->
<script src="/assets/plugins/morris/morris.min.js"></script>
<script src="/assets/plugins/raphael/raphael-min.js"></script>

<!-- Dashboard init -->
<script src="/assets/pages/jquery.dashboard.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.core.js"></script>
<script src="/assets/js/jquery.app.js"></script>

<!-- Sweet Alert js -->
<script src="//assets/plugins/bootstrap-sweetalert/sweet-alert.min.js"></script>
<script src="//assets/pages/jquery.sweet-alert.init.js"></script>

<!-- Form wizard -->
<script src="/assets/plugins/bootstrap-wizard/jquery.bootstrap.wizard.js"></script>
<script src="/assets/plugins/jquery-validation/dist/jquery.validate.min.js"></script>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="vw96.object type" content="document" />
<meta name="language" content="english, EN" />
<meta name="copyright" content="1999 - 2008 " />
<meta name="publisher" content="Batavianet - Gulali" />
<meta name="author" content="Batavianet - Gulali" />
<meta name="revisit-after" content="7 Days" />
<meta name="robots" content="NOINDEX,NOFOLLOW" />
<meta name="distribution" content="Global" />
<meta name="charset" content="ISO-8859-1" />
<meta name="expires" content="never" />
<meta name="rating" content="General" />

<!-- App Favicon -->
<asp:Literal ID="ikon" runat="server"></asp:Literal>
<link href="/assets/plugins/bootstrap-sweetalert/sweet-alert.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function confirmdel(url) {
        if (confirm("Apakah anda yakin ?")) location.href = url;
        else return false;
    }
    function confirm_del_notif(url) {
        if (confirm("Are you sure delete this notification?")) location.href = url;
        else return false;
    }
    function confirm_out(url) {
        if (confirm("Are You Sure?")) location.href = url;
        else return false;
    }
</script>

<script type="text/javascript">
     function MoneyFormat(Num) { //function to add commas to textboxes
        Num += '';
        Num = Num.replace('.', ''); Num = Num.replace('.', ''); Num = Num.replace('.', '');
        Num = Num.replace('.', ''); Num = Num.replace('.', ''); Num = Num.replace('.', '');
        x = Num.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1))
            x1 = x1.replace(rgx, '$1' + '.' + '$2');
        return x1 + x2;
    }
    function NPWPFormat(input) {
        // Strip all characters from the input except digits
        input = input.replace(/\D/g, '');

        // Trim the remaining input to ten characters, to preserve phone number format
        input = input.substring(0, 15);

        // Based upon the length of the string, we add formatting as necessary
        var size = input.length;
        if (size == 0) {
            input = input;
        }
        else if (size < 3) {
            input = input;
        }
        else if (size < 6) {
            input = input.substring(0, 2) + '.' + input.substring(2, 5);
        }
        else if (size < 9) {
            input = input.substring(0, 2) + '.' + input.substring(2, 5) + '.' + input.substring(5, 8);
        }
        else if (size < 10) {
            input = input.substring(0, 2) + '.' + input.substring(2, 5) + '.' + input.substring(5, 8) + '.' + input.substring(8, 9);
        }
        else if (size < 13) {
            input = input.substring(0, 2) + '.' + input.substring(2, 5) + '.' + input.substring(5, 8) + '.' + input.substring(8, 9) + '-' + input.substring(9, 12);
        }
        else {
            input = input.substring(0, 2) + '.' + input.substring(2, 5) + '.' + input.substring(5, 8) + '.' + input.substring(8, 9) + '-' + input.substring(9, 12) + '.' + input.substring(12, 15);
        }
        return input;
    }
</script>
