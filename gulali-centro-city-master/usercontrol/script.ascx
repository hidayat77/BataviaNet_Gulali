<%@ Control Language="C#" AutoEventWireup="true" CodeFile="script.ascx.cs" Inherits="usercontrol_script" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#basicwizard').bootstrapWizard({ 'tabClass': 'nav nav-tabs navtab-wizard nav-justified bg-muted' });

        $('#progressbarwizard').bootstrapWizard({
            onTabShow: function (tab, navigation, index) {
                var $total = navigation.find('li').length;
                var $current = index + 1;
                var $percent = ($current / $total) * 100;
                $('#progressbarwizard').find('.bar').css({ width: $percent + '%' });
            },
            'tabClass': 'nav nav-tabs navtab-wizard nav-justified bg-muted'
        });

        $('#btnwizard').bootstrapWizard({ 'tabClass': 'nav nav-tabs navtab-wizard nav-justified bg-muted', 'nextSelector': '.button-next', 'previousSelector': '.button-previous', 'firstSelector': '.button-first', 'lastSelector': '.button-last' });

        var $validator = $("#commentForm").validate({
            rules: {
                emailfield: {
                    required: true,
                    email: true,
                    minlength: 3
                },
                namefield: {
                    required: true,
                    minlength: 3
                },
                urlfield: {
                    required: true,
                    minlength: 3,
                    url: true
                }
            }
        });

        $('#rootwizard').bootstrapWizard({
            'tabClass': 'nav nav-tabs navtab-wizard nav-justified bg-muted',
            'onNext': function (tab, navigation, index) {
                var $valid = $("#commentForm").valid();
                if (!$valid) {
                    $validator.focusInvalid();
                    return false;
                }
            }
        });
    });

</script>

<!-- Magnific popup -->
<script type="text/javascript" src="/assets/plugins/magnific-popup/dist/jquery.magnific-popup.min.js"></script>


<script type="text/javascript">
    $(window).load(function () {
        var $container = $('.portfolioContainer');
        $container.isotope({
            filter: '*',
            animationOptions: {
                duration: 750,
                easing: 'linear',
                queue: false
            }
        });

        $('.portfolioFilter a').click(function () {
            $('.portfolioFilter .current').removeClass('current');
            $(this).addClass('current');

            var selector = $(this).attr('data-filter');
            $container.isotope({
                filter: selector,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });
            return false;
        });
    });
    $(document).ready(function () {
        $('.image-popup').magnificPopup({
            type: 'image',
            closeOnContentClick: true,
            mainClass: 'mfp-fade',
            gallery: {
                enabled: true,
                navigateByImgClick: true,
                preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
            }
        });
    });
</script>
