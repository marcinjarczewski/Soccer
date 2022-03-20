define(['jquery', 'storageHelper', 'messageQueue', 'amplify', 'baseRepository'], function ($, storageHelper, messageQueue, amplify, baseRepository) {
    var apiUrl = '/login/';
    var loginRepository = {
        login: function (userName, password) {
            baseRepository.spinner(true);
            var options = "grant_type=password&login=" + userName + "&password=" + password;
            var callback = function (result) {
                baseRepository.spinner(false);
                storageHelper.writeLocalStorage("login", userName);
                storageHelper.createCookie("token", result.token_type + " " + result.access_token, 15);
                messageQueue.addMessage("Login successful", "success");
                $(location).attr('href', '/login/test');
            }
            return amplify.request({
                resourceId: "login",
                data: options,
                success: callback
            });
        },
        logout: function () {
            return amplify.request({
                resourceId: "logout"
            }).done(function () {
                storageHelper.eraseCookie("token");
                $(location).attr('href', '/');
            });
        },
        changeLanguage: function (data, callback) {
            return amplify.request({
                resourceId: "changeLanguage",
                data: data,
                success: callback,
            });
        },
        lostPassword: function (data, callback) {
            return amplify.request({
                resourceId: "lostPassword",
                data: data,
                success: callback,
            });
        },
        changePassword: function (data, callback) {
            return amplify.request({
                resourceId: "changePassword",
                data: data,
                success: callback,
            });
        },
        register: function (data, callback) {
            return amplify.request({
                resourceId: "register",
                data: data,
                success: callback,
            });
        },
        init: function () {
            amplify.request.define("login", "ajax", {
                url: apiUrl + 'login',
                type: "Post",
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                decoder: "globalDecoder"
            });
            amplify.request.define("logout", "ajax", {
                url: apiUrl + "logout",
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("changeLanguage", "ajax", {
                url: apiUrl + "changeLanguage",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("lostPassword", "ajax", {
                url: "/login/LostPassword",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
            });
            amplify.request.define("changePassword", "ajax", {
                url: "/login/changePassword",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
            });
            amplify.request.define("register", "ajax", {
                url: apiUrl + "register",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder"
            });

            amplify.request.define("getRoles", "ajax", {
                url: apiUrl + "login/getroles",
                dataType: "json",
                type: "POST"
            });
        }
    };
    loginRepository.init();
    return loginRepository;
});