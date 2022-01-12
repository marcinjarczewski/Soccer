(function () {
    ko.extenders.localStore = function (target, options) {
        var store = amplify.store.localStorage;
        var value = store(options.key);

        if (value !== null && value !== undefined) {
            value = $.parseJSON(value);
            target(value);
        }

        if (options.extendFunc !== undefined) {
            options.extendFunc(target);
        }

        ko.watch(target, function () {
            store(options.key, ko.toJSON(target));
        });

        return target;
    };

    ko.bindingHandlers.decimal = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var observable = valueAccessor();
            var value = observable;
            if (typeof(value) === "function")
            {
                value = observable();
            }

            var interceptor = ko.computed({
                read: function () {
                    return formatWithComma(value,2);
                },
                write: function (newValue) {
                    return reverseFormat(newValue,2);
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

    ko.validation.registerExtenders();

}());