define(['storageHelper', 'knockout', 'loginRepository'], function (storageHelper, ko, loginRepository) {
    var loginModel = function (options) {
        var self = this;
        var checkLoggedIn = function () {
            var token = storageHelper.readCookie("token");
            if (token != null && token.length > 0) {
                return true;
            }

            return false;
        };
        self.isLoggedIn = ko.observable(checkLoggedIn());
        self.logout = function () {
            var ajaxLogout = function () {
                return $.ajax({
                    url: '/login/logout',
                    type: "Post",
                    contentType: 'application/json; charset=utf-8',
                    error: function (data) {
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                    }
                });
            };
            return ajaxLogout().done(function (result) {
                storageHelper.eraseCookie("token");
                $(location).attr('href', '/');
            });
        };
    };

    return loginModel;
});