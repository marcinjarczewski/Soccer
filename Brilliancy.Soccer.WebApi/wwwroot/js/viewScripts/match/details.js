define(['knockoutWithAddons', 'knockoutMapping', 'moment', 'messageQueue', 'globalModel', 'matchRepository',  "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, moment, messageQueue, globalModel, matchRepository, translations) {
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

            vm.lastMatchUpdate = moment().format();
            vm.updateMatch = function () {
                let dataObject = {
                    id: vm.model().id(),
                    lastUpdate: vm.lastMatchUpdate
                };
                let callback = function (result) {
                    if (result.isSuccess && result.data != null) {
                        if (result.data.shouldUpdate) {
                            vm.model().homeGoals(result.data.model.homeGoals);
                            vm.model().awayGoals(result.data.model.awayGoals);
                            vm.lastMatchUpdate = moment().format();

                            vm.model().homeGoalsList.removeAll();
                            result.data.model.homeGoalsList.forEach(function (item) {
                                vm.model().homeGoalsList.push(mappings.fromJS(ko.toJS(item)));
                            });

                            vm.model().awayGoalsList.removeAll();
                            result.data.model.awayGoalsList.forEach(function (item) {
                                vm.model().awayGoalsList.push(mappings.fromJS(ko.toJS(item)));
                            });

                            let start = 0;
                            if (result.data.model.startDate) {
                                start = parseInt((moment() - moment(result.data.model.startDate)) / 1000)
                            }
                            vm.timer(start);
                        }
                    }

                    setTimeout(vm.updateMatch, 100);
                    return true;
                };
                return matchRepository.updateLiveMatch(dataObject, callback);
            }
            setTimeout(vm.updateMatch, 1500);

            return vm;
        }

        return ViewModel;
    });