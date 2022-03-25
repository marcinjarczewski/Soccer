define(['jquery', 'knockout', 'moment', 'pikaday', 'dropzone', "/js/plugins/i18n.js!/nls/translation.js"], function ($, ko, moment, pikaday, dropzone, translations) {
    ko.bindingHandlers.spinner = {
        init: function (element, valueAccessor, allBindings) {
            var deferred = $.Deferred();
            element.spinner = deferred.promise();
            setTimeout(function () {
                var options = {};
                options.color = $(element).css("color");

                $.extend(options, ko.bindingHandlers.spinner.defaultOptions, ko.unwrap(allBindings.get("spinnerOptions")));

                deferred.resolve(new Spinner(options));
            }, 30);
        },
        update: function (element, valueAccessor, allBindingsAccessor) {
            var result = ko.unwrap(valueAccessor());

            element.spinner.done(function (spinner) {
                var isSpinning = result;
                if (isSpinning) {
                    $(element).show();
                    spinner.spin(element);
                } else {
                    if (spinner.el) { //don't stop first time
                        spinner.stop();
                    }

                    $(element).hide();
                }
            });
        },
        defaultOptions: {
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
        }
    };
    ko.bindingHandlers.pikaday = {
        init: function (element, valueAccessor, allBindings) {
            var minDate = moment(ko.unwrap(allBindings.get('minDate') || null)),
                maxDate = moment(ko.unwrap(allBindings.get('maxDate') || null)),
                format = ko.unwrap(allBindings.get('format')),
                defaultDate = ko.unwrap(allBindings.get('defaultDate')),
                showTime = ko.unwrap(allBindings.get('showTime') || false),
                inputFormats = ko.unwrap(allBindings.get('inputFormats'));
            element.pikaday = new pikaday({
                field: element,
                use24hour: true,
                timeLabel: translations.pikaday.hour,
                format: format,
                inputFormats: inputFormats,
                defaultDate: moment(defaultDate).toDate(),
                yearRange: [1920, 2020],
                clearInvalidInput: true,
                showTime: showTime,
                minDate: minDate.isValid() ? minDate.toDate() : undefined,
                maxDate: maxDate.isValid() ? maxDate.toDate() : undefined,
                onSelect: function (date) {
                    valueAccessor()(date);
                },
                onClose: function (date) {
                    if ($(element).val() === '') {
                        valueAccessor()(null);
                    }
                },
                firstDay: 1,
                i18n: translations.pikaday
            });
        },
        update: function (element, valueAccessor, allBindings) {
            var date = ko.unwrap(valueAccessor());
            element.pikaday.setDate(moment(date).format());
        }
    };

    ko.bindingHandlers.date = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            let value = valueAccessor;
            if (typeof (value) === "function") {
                value = value();
            }
            if (typeof (value) === "function") {
                value = value();
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
                return moment(x).format("YYYY.MM.DD HH:mm")
            }

            function reverseFormat(x, seperator) {
                if (x == null) {
                    return "";
                }

                return moment(x);
            }
        },
        update: function (element, valueAccessor) {
            let x = valueAccessor;
            if (typeof (x) === "function") {
                x = x();
            }
            if (typeof (x) === "function") {
                x = x();
            }
            if (x == null || x == "" || x.startsWith('00')) {
                $(element).text("");
                return true;
            }
            $(element).text(moment(x).format("YYYY.MM.DD HH:mm"));
            return true;
        }
    };

    ko.bindingHandlers.touchSpin = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var allBindings = allBindingsAccessor(),
                value = ko.utils.unwrapObservable(valueAccessor()),
                touchSpinOptions = allBindings.touchSpinOptions || {},
                defaults = ko.bindingHandlers.touchSpin.defaults,
                $element = $(element);

            var customOptions = ko.toJS(touchSpinOptions);
            if (customOptions.step <= 0) {
                customOptions.step = 1;
            }
            var options = $.extend(false, { initval: value }, defaults, customOptions);

            ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);
            $element.TouchSpin(options);
            ko.bindingHandlers.touchSpin.updateEnableState(element, allBindings);
        },
        update: function (element, valueAccessor, allBindingsAccessor) {
            var allBindings = allBindingsAccessor(),
                $element = $(element);

            if (allBindings.touchSpinOptions && allBindings.touchSpinOptions.step) {
                $element.trigger("touchspin.updatesettings", { step: allBindings.touchSpinOptions.step });
            }

            ko.bindingHandlers.value.update(element, valueAccessor);
            ko.bindingHandlers.touchSpin.updateEnableState(element, allBindings);
        },
        updateEnableState: function (element, allBindings) {
            var $element = $(element),
                $plus = $element.parent().find(".bootstrap-touchspin-up"),
                $minus = $element.parent().find(".bootstrap-touchspin-down");

            if (allBindings.enable !== undefined) {
                if (ko.utils.unwrapObservable(allBindings.enable) === true) {
                    $plus.removeAttr('disabled');
                    $minus.removeAttr('disabled');
                    // $element.trigger("touchspin.updatesettings", { mousewheel: true });
                } else {
                    $plus.attr('disabled', 'disabled');
                    $minus.attr('disabled', 'disabled');
                    // $element.trigger("touchspin.updatesettings", { mousewheel: false });
                }
            }

            if (allBindings.disable !== undefined) {
                if (ko.utils.unwrapObservable(allBindings.disable) === false) {
                    $plus.removeAttr('disabled');
                    $minus.removeAttr('disabled');
                    // $element.trigger("touchspin.updatesettings", { mousewheel: true });
                } else {
                    $plus.attr('disabled', 'disabled');
                    $minus.attr('disabled', 'disabled');
                    // $element.trigger("touchspin.updatesettings", { mousewheel: false });
                }
            }
        },
        defaults: {
            min: 1,
            max: 1000000000,
            initval: "",
            step: 1,
            decimals: 0,
            stepinterval: 100,
            forcestepdivisibility: 'round',  // none | floor | round | ceil
            stepintervaldelay: 500,
            prefix: "",
            postfix: "",
            prefix_extraclass: "",
            postfix_extraclass: "",
            booster: true,
            boostat: 10,
            maxboostedstep: false,
            mousewheel: false, // Aguarda fixar ou incluir o disabled corretamente
            buttondown_class: "btn btn-default",
            buttonup_class: "btn btn-default"
        }
    };

    ko.bindingHandlers.dropzone = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var value = ko.unwrap(valueAccessor()),
                object = allBindingsAccessor().object;

            var options = {
                maxFileSize: 15,
                createImageThumbnails: false,
            };

            $.extend(options, value);
            $(element).addClass('dropzone');
            object(new Dropzone(element, options)); // jshint ignore:line
        }
    };

    ko.bindingHandlers.tooltip = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var $element = $(element),
                value = valueAccessor(),
                placement = allBindingsAccessor().placement;

            $element.tooltip({
                trigger: 'hover',
                title: value,
                placement: placement || 'top'
            });

            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).tooltip("destroy");
            });
        }
    };

    var summernoteText = "";
    ko.bindingHandlers.summernote = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var allBindings = allBindingsAccessor();

            var toolbar = {
                toolbar: [
                    ['headline', ['style']],
                    ['style', ['bold', 'underline', 'clear']],
                    ['alignment', ['ul', 'ol', 'paragraph', 'lineheight']],
                    ["links", ["link", "picture", "video"]],
                    ["misc", ["codeview"]]
                ]
            };

            var summernoteConfig = ko.utils.unwrapObservable(allBindings.summernote);
            summernoteConfig = (typeof summernoteConfig === 'object') ? summernoteConfig : {};
            var options = $.extend(toolbar, summernoteConfig);
            $(element).summernote(options);

            $(element).summernote('code', allBindings.value());
            summernoteText = allBindings.value();

            $(element).on('summernote.change', function (we, contents, $editable) {
                summernoteText = contents;
                allBindings.value(contents);
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor) {
            var $element = $(element);
            var allBindings = allBindingsAccessor();
            var value = allBindings.value();
            if (summernoteText !== value)
                $element.summernote('code', value);
        }
    };

    ko.bindingHandlers.enterkey = {
        init: function (element, valueAccessor, allBindings, viewModel) {
            var callback = valueAccessor();
            $(element).keypress(function (event) {
                var keyCode = (event.which ? event.which : event.keyCode);
                if (keyCode === 13) {
                    callback.call(viewModel);
                    return false;
                }
                return true;
            });
        }
    };

    ko.bindingHandlers.typeahead = {
        update: function (element, valueAccessor, allBindings) {

            var templateName = ko.unwrap(allBindings().templateName);
            var mapping = ko.unwrap(allBindings().mappingFunction);
            var onSelect = allBindings.get("onSelectFunction");
            var displayedProperty = ko.unwrap(allBindings().displayKey);
            var user_typeahead_options = ko.unwrap(allBindings().typeaheadOpts) || {};
            var value = allBindings.get("value");

            var url = ko.unwrap(valueAccessor());
            var remoteFilter = ko.unwrap(allBindings.get("remoteFilter"));

            var remoteData = {
                url: url,
                prepare: function (query, settings) {
                    if (allBindings.has("authToken")) {
                        settings.beforeSend = function (xhr) {
                            xhr.setRequestHeader("Authorization", ko.unwrap(allBindings().authToken));
                        };
                        settings.data = { q: query };
                    }
                    settings.url += '?search=' + query;
                    return settings;
                }
            };

            if (remoteFilter) {
                remoteData.filter = remoteFilter;
            }

            var resultsLimit = allBindings.get("limit") || 10;

            var suggestions = new Bloodhound({
                datumTokenizer: function (token) {
                    return Bloodhound.tokenizers.whitespace(token);
                },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                remote: remoteData,
                limit: resultsLimit
            });

            suggestions.initialize();

            $(element).typeahead("destroy");

            var typeaheadOpts = {
                source: suggestions.ttAdapter(),
                displayKey: displayedProperty || function (item) {
                    return item;
                }
            };

            if (templateName) {
                typeaheadOpts.templates = {
                    suggestion: function (item) {
                        var temp = document.createElement("div");
                        var model = mapping ? mapping(item) : item;
                        ko.renderTemplate(templateName, model, null, temp, "replaceChildren");

                        return temp;
                    }
                };
            }

            $(element)
                .typeahead($.extend({
                    hint: true,
                    highlight: true
                }, user_typeahead_options), typeaheadOpts)
                .on("typeahead:selected typeahead:autocompleted", function (e, suggestion) {
                    if (onSelect) {
                        onSelect(value, suggestion, e);
                    } else if (value && ko.isObservable(value)) {
                        value(suggestion);
                    }
                });
        }
    };

    $calendars = [];
    ko.bindingHandlers.fullCalendar = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            element.innerHTML = "";
            var events = valueAccessor(),
                limit = allBindingsAccessor().eventLimit() || false,
                resize = allBindingsAccessor().eventResize,
                drop = allBindingsAccessor().eventDrop,
                click = allBindingsAccessor().eventClick,
                receive = allBindingsAccessor().eventReceive;

            var calendar = $(element);
            calendar.fullCalendar({
                defaultDate: "2018-06-25",
                header: {
                    left: 'prev today',
                    center: 'title',
                    right: 'next '
                },
                events: events,
                //eventLimit: limit,
                editable: true,
                droppable: true,
                drop: drop,
                eventResize: function (event, delta, revertFunc) {
                    return resize(calendar, event, delta, revertFunc);
                },
                eventDrop: function (event, delta, revertFunc, jsEvent, ui, view) {
                    return drop(calendar, event, delta, revertFunc, jsEvent, ui, view);
                },
                eventClick: function (event, jsEvent, view) {
                    return click(calendar, event, jsEvent, view);
                },
                eventReceive: function (event) {
                    return receive(calendar, event);
                }
            });

            $calendars.push(calendar);
            calendar.attr("id", $calendars.length - 1);
        },
        update: function (element, valueAccessor, allBindingsAccessor) {

        }
    };

    ko.bindingHandlers.draggable = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var $element = $(element),
                value = valueAccessor();

            $element.draggable(value);
        }
    };

    ko.bindingHandlers.eventData = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var $element = $(element),
                value = valueAccessor();

            $element.data('event', value);
        }
    };

    ko.bindingHandlers.boolean = {
        update: function (element, valueAccessor, allBindings) {
            var bool = ko.utils.unwrapObservable(valueAccessor()),
                trueText = allBindings.get('trueText') || 'Tak',
                falseText = allBindings.get('falseText') || 'Nie';
            $(element).text(bool ? trueText : falseText);
        },
    };

});