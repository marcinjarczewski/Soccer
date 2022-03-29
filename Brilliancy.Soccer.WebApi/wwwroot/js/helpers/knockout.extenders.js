define(['knockout', 'knockoutValidation', "/js/plugins/i18n.js!/nls/translation.js"], function (ko, koVal, translations) {
    ko.bindingHandlers.timer = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var observable = valueAccessor();
            var value = observable;
            if (typeof (value) === "function") {
                value = observable();
            }

            var interceptor = ko.computed({
                read: function () {
                    return formatTime(value, ':');
                },
                write: function (newValue) {
                    return reverseFormat(newValue, ':');
                }
            });

            if (element.tagName == 'INPUT')
                ko.applyBindingsToNode(element, {
                    value: interceptor
                });
            else
                ko.applyBindingsToNode(element, {
                    text: interceptor
                });

            function formatTime(x, seperator) {
                if (x == null) {
                    return "";
                }
                let seconds = x % 60;
                let minutes = x / 60;
                return Math.floor(minutes) + seperator + seconds.toString().padStart(2, '0');
            }

            function reverseFormat(x, seperator) {
                if (x == null) {
                    return "";
                }
                var temp = x.split(separator);
                if (temp.length == 1) {
                    return 0;
                }

                return temp[0] * 60 + seperator + temp[1].toString().padStart(2, '0');
            }
        },
        update: function (element, valueAccessor) {
            let x = valueAccessor()();
            let seconds = x % 60;
            let minutes = x / 60;
            $(element).text(Math.floor(minutes) + ':' + seconds.toString().padStart(2, '0'));
            return true;
        }
    };

    ko.bindingHandlers.mainBody = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            return true;
        },
        update: function (element, valueAccessor) {
            let value = valueAccessor();
            if (typeof (value) === "function") {
                value = value();
            }
            if (value) {
                $(element).removeClass("visible");
                $(element).addClass("my-hidden");
                setTimeout(function () { $(element).addClass("hidden");}, 450);
            }
            else {
                $(element).removeClass("my-hidden");
                $(element).addClass("visible");
                setTimeout(function () {
                    $(element).removeClass("hidden");
                }, 450);
            }
            return true;
        }
    };

    ko.bindingHandlers.modalBody = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            return true;
        },
        update: function (element, valueAccessor) {
            let value = valueAccessor();
            if (typeof (value) === "function") {
                value = value();
            }
            if (value) {
                $(element).removeClass("my-hidden");
                $(element).addClass("visible");
                setTimeout(function () { $(element).removeClass("hidden"); }, 450);
            }
            else {
                $(element).removeClass("visible");
                $(element).addClass("my-hidden");
                setTimeout(function () { $(element).addClass("hidden"); }, 450);
            }
            return true;
        }
    };

    ko.bindingHandlers.decimal = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var observable = valueAccessor();
            var value = observable;
            if (typeof (value) === "function") {
                value = observable();
            }

            var interceptor = ko.computed({
                read: function () {
                    return formatWithComma(value, 2);
                },
                write: function (newValue) {
                    return reverseFormat(newValue, 2);
                }
            });

            if (element.tagName == 'INPUT')
                ko.applyBindingsToNode(element, {
                    value: interceptor
                });
            else
                ko.applyBindingsToNode(element, {
                    text: interceptor
                });

            function formatWithComma(x, precision, seperator) {
                if (x == null) {
                    return "";
                }
                var options = {
                    precision: precision || 2,
                    seperator: seperator || '.'
                };
                var formatted = parseFloat(x, 10).toFixed(options.precision);
                var regex = new RegExp(
                    '^(\\d+)[^\\d](\\d{' + options.precision + '})$');
                formatted = formatted.replace(
                    regex, '$1' + options.seperator + '$2');
                return formatted;
            }

            function reverseFormat(x, precision, seperator) {
                if (x == null) {
                    return "";
                }
                var options = {
                    precision: precision || 2,
                    seperator: seperator || ','
                };
                var regex = new RegExp(
                    '^(\\d+)[^\\d](\\d+)$');
                var formatted = x.replace(regex, '$1.$2');
                return parseFloat(formatted);
            }
        },
        update: function (element, valueAccessor) {
            return true;
        }
    };

    ko.validation.rules['mustCheck'] = {
        validator: function (val, params) {
            return val === params;
        },
        message: translations.validation.checkbox
    };

    ko.validation.rules['hour'] = {
        validator: function (val) {
            if (!val) {
                return true;
            }
            if (val.length != 5) {
                return false;
            }
            if (val[2] != ':') {
                return false;
            }
            if (isNaN(val.substring(0, 2))) {
                return false;
            }
            if (isNaN(val.substring(2, 2))) {
                return false;
            }
            let hour = parseInt(val.substring(0, 2));
            if (hour < 0 || hour > 23) {
                return false;
            }
            let minutes = parseInt(val.substring(3, 5));
            if (minutes < 0 || minutes > 59) {
                return false;
            }
            debugger;
            return true;
        },
        message: translations.validation.hour
    };

    ko.validation.rules['equalPasswords'] = {
        validator: function (val, params) {
            return val === params;
        },
        message: translations.validation.equalPasswords
    };
    ko.validation.registerExtenders();

});