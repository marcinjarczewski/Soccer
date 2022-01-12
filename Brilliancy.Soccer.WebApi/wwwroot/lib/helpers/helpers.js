(function () {
    var Helpers = function () {
    };

    Helpers.prototype.date = function viewDate(date, format) {
        if (format === null
            || format === undefined) {
            format = 'YYYY-MM-DD';
        }

        if (date != null) {
            return moment(date).format(format);
        }
        return '';
    };

    Helpers.prototype.rewriteFilter = function viewDate(filter) {
        var rewrited = {};
        var keys = Object.keys(filter);
        for (var i = 0; i < keys.length; i++) {
            var val = filter[keys[i]];
            rewrited[keys[i]] = val;
            if (val === "true") {
                rewrited[keys[i]] = true;
            }
            if (val === "false") {
                rewrited[keys[i]] = false;
            }
            // use val
        }
        return rewrited;
    };

    Helpers.prototype.getFromListByValue = function viewDate(list, value) {
        if (!value) {
            return null;
        }
        var elemValue = value.value;
        if (!elemValue) {
            return null;
        }
        for (var i = 0; i < list.length; i++) {
            if (list[i].value == elemValue) {
                return list[i];
            }
        }

        return null;
    };

    Helpers.prototype.spinnerOpts = function spinnerOpts(opts) {
        return {
            lines: 13,
            length: 56,
            width: 5,
            radius: 42,
            scale: 0.25,
            corners: 1,
            color: '#000',
            opacity: 0.25,
            rotate: 0,
            direction: 1,
            speed: 1,
            trail: 60,
            fps: 20,
            zIndex: 2e9,
            className: 'spinner',
            top: '50%',
            left: '50%',
            shadow: false,
            hwaccel: false,
            position: 'absolute'
        };
    };

    Helpers.prototype.validOpts = function validOpts(opts) {
        return {
            grouping: {
                deep: true
            },
            messagesOnModified: true,
            errorClass: 'has-error',
            insertMessages: false
        };
    };


    Helpers.prototype.showError = function validOpts(data) {
        if (!data) {
            return helpers.log('nieznany błąd', 'error');
        }
        if (data == null || data.responseJSON == null || data.responseJSON.exceptionMessage == null) {
            return helpers.log(ko.toJSON(data), 'error');
        }
        helpers.log(ko.toJSON(data.responseJSON.exceptionMessage), 'error');
    };


    Helpers.prototype.valid = function validOpts(self, show) {
        var result = ko.validation.group(self, { deep: true });
        if (show == true) {
            if (typeof (self.sendModel) !== 'undefined' && !self.sendModel.isValid()) {
                result.showAllMessages(true);
                return false;
            }
            else if (typeof (self.model) !== 'undefined' && !self.model.isValid()) {
                result.showAllMessages(true);
                return false;
            }
        }
        else {
            result.showAllMessages(false);
        }
        return true;
    };

    Helpers.prototype.log = function validOpts(message, type) {
        if (type === 'error') {
            toastr.error(message);
        } else if (type === 'info') {
            toastr.info(message);
        } else if (type === 'success') {
            toastr.success(message);
        } else {
            toastr.info(message);
        }
    };

    Helpers.prototype.messageBox = function validOpts(title, message, alwaysVisible) {
        if (alwaysVisible) {
            return bootbox.dialog({
                closeButton: false,
                title: title,
                message: message,
                buttons: {
                }
            });
        }
        if (message) {
            return bootbox.alert({
                title: title,
                message: message,
            });
        }

        return bootbox.alert({
            message: title,
        });
    };

    Helpers.prototype.showOverlay = function () {
        $("#overlay").removeClass("hidden");
    };

    Helpers.prototype.hideOverlay = function () {
        $("#overlay").addClass("hidden");
    };

    Helpers.prototype.getLayout = function (goBack) {
        var ajaxLogout = function () {
            return $.ajax({
                url: "/" + 'login/logout',
                type: "Post",
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                //data: options,
                success: function (data) {
                    StorageHelper.eraseCookie("token");
                    $(location).attr('href', '/logowanie');
                },
                error: function (data) {
                    helpers.log(ko.toJSON(data.responseJSON.message), 'error');
                    StorageHelper.eraseCookie("token");
                    location.reload();
                },
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", StorageHelper.readCookie("token"));
                }
            });
        }
        return {
            goBack: goBack,
            showGoBack: ko.observable(false),
            login: ko.observable(StorageHelper.readLocalStorage("login")),
            logout: function () { ajaxLogout(); }
        };
    };

    Helpers.prototype.confirm = function validOpts(message, callback) {
        bootbox.confirm({
            message: message,
            size: "small",
            buttons: {
                confirm: {
                    label: 'Tak',
                    className: 'btn-info'
                },
                cancel: {
                    label: 'Nie',
                    className: 'btn-default'
                }
            },
            callback: callback
        });
    };

    if (typeof window === "object" && typeof window.document === "object") {
        window.helpers = Helpers;
        window.helpers = new Helpers();
    }
})();