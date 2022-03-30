define(['knockoutWithAddons', 'knockoutMapping', 'moment', 'messageQueue', 'globalModel', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, moment, messageQueue, globalModel, translations) {
        let ViewModel = function (options) {
            let mapping = {
                homeTeamName: {
                    create: function (options) {
                        return ko.validatedObservable(options.data).extend({
                            required: { message: translations.validation.fieldEmpty }
                        });
                    }
                },
                awayTeamName: {
                    create: function (options) {
                        return ko.validatedObservable(options.data).extend({
                            required: { message: translations.validation.fieldEmpty }
                        });
                    }
                }
            };

            let vm = {
                globalModel: globalModel(),
                isBusy: ko.observable(false),
                isTournamentAdmin: options.isTournamentAdmin
            };
            vm.translations = translations.matchEdit;
            vm.model = ko.validatedObservable(mappings.fromJS(options.json, mapping));

            let start = 0;
            if (vm.model().startDate()) {
                start = parseInt((moment() - moment(vm.model().startDate())) / 1000)
            }
            vm.timer = ko.observable(start);

            function setTimer() {
                vm.timer(vm.timer() + 1);
            }

            vm.timerEvent = setInterval(setTimer, 1000);
            return vm;
        }

        return ViewModel;
    });