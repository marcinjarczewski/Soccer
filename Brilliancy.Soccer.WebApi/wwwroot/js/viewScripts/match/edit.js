define(['knockoutWithAddons', 'knockoutMapping', 'moment', 'messageQueue', 'globalModel', 'helpers',
     'matchRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, moment, messageQueue, globalModel, helpers, matchRepository, translations) {
        var ViewModel = function (options) {
            var mapping = {
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

            var vm = {
                globalModel: globalModel(),
                isBusy: ko.observable(false),
            };
            vm.translations = translations.matchEdit;
            vm.model = ko.validatedObservable(mappings.fromJS(options.json, mapping));
            vm.addHomePlayer = function (data) {
                vm.model().homePlayers.push(mappings.fromJS(ko.toJS(data)));
                vm.model().availablePlayers.remove(data);
            };
            vm.addAwayPlayer = function (data) {
                vm.model().awayPlayers.push(mappings.fromJS(ko.toJS(data)));
                vm.model().availablePlayers.remove(data);
            };
            vm.removeHomePlayer = function (data) {
                vm.model().availablePlayers.push(mappings.fromJS(ko.toJS(data)));
                vm.model().homePlayers.remove(data);
            };
            vm.removeAwayPlayer = function (data) {
                vm.model().availablePlayers.push(mappings.fromJS(ko.toJS(data)));
                vm.model().awayPlayers.remove(data);
            };
      
            vm.matchErrors = ko.validation.group([
                vm.model().homeTeamName,
                vm.model().awayTeamName]);

            vm.editCreating = function () {
                debugger;
                if (vm.matchErrors().length > 0) {
                    vm.matchErrors.showAllMessages();
                    return false;
                }

                let dataObject = {
                    homePlayers: ko.toJS(vm.model().homePlayers()),
                    awayPlayers: ko.toJS(vm.model().awayPlayers()),
                    id: vm.model().id(),
                    homeTeamName: vm.model().homeTeamName(),
                    awayTeamname: vm.model().awayTeamName(),
                    date: moment(vm.model().date()).format('YYYY.MM.DDThh:mm:ss')
                };
                vm.isBusy(true);
                let callback = function (result) {
                    vm.globalModel.spinner(false);
                    vm.isBusy(false);
                    if (!result.isSuccess) {
                        helpers.log(result.message, 'error');
                        return false;
                    }
                    else {
                        messageQueue.addMessage(translations.tournamentCreate.created, 'success');
                        $(location).attr('href', '/login/test');
                    }
                    return true;
                };
                return matchRepository.editCreating(dataObject, callback);
            };

            return vm;
        }

        return ViewModel;
    });