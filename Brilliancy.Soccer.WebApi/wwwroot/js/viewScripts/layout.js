define(['jquery', 'storageHelper', 'toastr', "/js/plugins/i18n.js!/nls/translation.js"],
    function ($, storageHelper, toastr, translations) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-full-width",
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
            $('#cookies-id').removeClass("d-none");
            $(".body-content").css("heigth", window.innerHeight - $('#cookies-id').height() - $(".navbar").parent().height());
        }
        $('#cookiesOk').on('click', function () {
            storageHelper.writeLocalStorage("cookies", "ok");
            $('#cookies-id').addClass("d-none");
            $(".body-content").css("heigth", window.innerHeight - $('#cookies-id').height() - $(".navbar").parent().height());
        });
    });