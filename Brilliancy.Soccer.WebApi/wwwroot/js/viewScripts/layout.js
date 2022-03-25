define(['jquery', 'storageHelper', 'toastr', "/js/plugins/i18n.js!/nls/translation.js"],
    function ($, storageHelper, toastr, translations) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-full-width",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "0",
            "hideDuration": "00",
            "timeOut": "0",
            "extendedTimeOut": "0",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        var cookies = storageHelper.readLocalStorage("cookies");
        if (cookies == null) {
            $('#cookie-line1').text(translations?.cookies?.line1);
            $('#cookie-line2').text(translations?.cookies?.line2);
            $('#cookie-line3').text(translations?.cookies?.line3);
            $('#cookie-line4').text(translations?.cookies?.line4);
            $('#cookies-id').removeClass("d-none");
            $(".body-content").css("heigth", window.innerHeight - $('#cookies-id').height() - $(".navbar").parent().height());
        }
        $('#cookiesOk').on('click', function () {
            storageHelper.writeLocalStorage("cookies", "ok");
            $('#cookies-id').addClass("d-none");
            $(".body-content").css("heigth", window.innerHeight - $('#cookies-id').height() - $(".navbar").parent().height());
        });
        setInterval(function () { $('#loading-body').hide(); $('#loading-header').hide();}, 600);
    });