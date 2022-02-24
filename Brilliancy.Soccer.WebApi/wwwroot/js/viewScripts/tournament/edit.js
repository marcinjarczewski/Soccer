define(['knockoutWithAddons', 'knockoutMapping', 'messageQueue', 'globalModel', 'helpers',
    'tournamentRepository', 'playerRepository', 'matchRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, messageQueue, globalModel, helpers, tournamentRepository, playerRepository, matchRepository, translations) {
        var ViewModel = function (options) {
            var mapping = {
                name: {
                    create: function (options) {
                        return ko.validatedObservable(options.data).extend({
                            required: { message: translations.validation.fieldEmpty }
                        });
                    }
                },
                address: {
                    create: function (options) {
                        return ko.validatedObservable(options.data).extend({
                            required: { message: translations.validation.fieldEmpty }
                        });
                    }
                }
            };

            var matchMapping = {
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

            vm.model = ko.validatedObservable(mappings.fromJS(options.json, mapping));
            vm.newMatch = ko.validatedObservable(mappings.fromJS(ko.toJS(vm.model().emptyMatch), matchMapping));
            vm.addPlayer = function () {
                vm.model().players.push(ko.toJS(vm.model().emptyPlayer));
            };
            vm.removePlayer = function (data) {
                vm.model().players.remove(data);
            };
            vm.editTournament = function () {
                if (vm.modelErrors().length > 0) {
                    vm.modelErrors.showAllMessages();
                    return false;
                }

                let dataObject = ko.toJS(vm.model);
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
                return tournamentRepository.edit(dataObject, callback);
            };

            vm.addMatch = function () {
                if (vm.newMatchErrors().length > 0) {
                    vm.newMatchErrors.showAllMessages();
                    return false;
                }

                let dataObject = ko.toJS(vm.newMatch);
                vm.isBusy(true);
                let callback = function (result) {
                    debugger;
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
                dataObject.tournamentId = vm.model().id();
                return matchRepository.add({ homeTeamName: dataObject.homeTeamName, awayTeamName: dataObject.awayTeamName, tournamentId: dataObject.tournamentId }, callback);
            };

            vm.editPlayers = function () {
                let validation = ko.validation.group(vm, { deep: true });
                if (validation().length == 0) {
                    let dataObject = ko.toJS(vm.model().players);
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
                    return playerRepository.edit({ players: dataObject, tournamentId: vm.model().id() }, callback);
                }
                else {
                    validation.showAllMessages(true);
                }
            };

            vm.newMatchErrors = ko.validation.group([
                vm.newMatch().homeTeamName,
                vm.newMatch().awayTeamName]);

            vm.modelErrors = ko.validation.group([
                vm.model().name,
                vm.model().address
            ]);
            return vm;
        }

        return ViewModel;
    });