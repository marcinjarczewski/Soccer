(function ($) {
    StorageHelper = {
        createCookie: function (name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toUTCString();
            }
            document.cookie = name + "=" + value + expires + "; path=/";
        },
        writeLocalStorage: function (name, object) {
            localStorage[name] = JSON.stringify(object);
        },
        readLocalStorage: function (name) {
            var dataText = localStorage[name],
                data;

            if (dataText && dataText != "undefined") {
                data = JSON.parse(dataText);
                return data;
            };

            return null;
        },
        writeSessionStorage: function (name, object) {
            sessionStorage[name] = JSON.stringify(object);
        },
        readSessionStorage: function (name) {
            var dataText = sessionStorage[name],
                data;

            if (dataText && dataText != "undefined") {
                data = JSON.parse(dataText);
                return data;
            };

            return null;
        },
        readCookie: function (name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
            }
            return null;
        },
        eraseCookie: function (name) {
            StorageHelper.createCookie(name, "", -1);
        }
    };
}(jQuery));