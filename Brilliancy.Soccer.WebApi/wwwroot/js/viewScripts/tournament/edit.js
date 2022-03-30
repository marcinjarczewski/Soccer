define(['knockoutWithAddons', 'knockoutMapping', 'messageQueue', 'globalModel', 'helpers',
    'tournamentRepository', 'playerRepository', 'matchRepository', 'authenticationRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, messageQueue, globalModel, helpers, tournamentRepository, playerRepository, matchRepository, authenticationRepository, translations) {
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
                days: [
                    {
                        name: translations.days.monday,
                        value: 1
                    },
                    {
                        name: translations.days.tuesday,
                        value: 2
                    },
                    {
                        name: translations.days.wednesday,
                        value: 3
                    },
                    {
                        name: translations.days.thursday,
                        value: 4
                    },
                    {
                        name: translations.days.friday,
                        value: 5
                    },
                    {
                        name: translations.days.saturday,
                        value: 6
                    },
                    {
                        name: translations.days.sunday,
                        value: 7
                    }]
            };
          
            vm.showModal = ko.observable(false);
            vm.invitedPlayer = ko.observable({
                id: ko.observable(null),
                email: ko.validatedObservable('').extend({
                     required: { message: translations.validation.fieldEmpty }, email: { message: translations.validation.email }
                })
            });
            vm.translations = translations.tournamentEdit;
            vm.model = ko.validatedObservable(mappings.fromJS(options.json, mapping));         
            vm.selectedDay = ko.observable(vm.days.find(el => el.value == vm.model().defaultDayOfTheWeek()));
            vm.notAdmins = ko.observableArray();
            vm.model().players().forEach(function (player) {
                if (!player.userId() || player.userId() == null) {
                    return;
                }
                for (var i = 0; i < vm.model().admins().length; i++) {
                    if (vm.model().admins()[i].id() == player.userId()) {
                        return;
                    }
                }
                vm.notAdmins.push({
                    id: player.userId(),
                    name: player.firstName() + " " + player.nickName() + " " + player.lastName()
                });
            });
            vm.newMatch = ko.validatedObservable(mappings.fromJS(ko.toJS(vm.model().emptyMatch), matchMapping));
            vm.tournamentLogo = {
                filesRequestUrl: location.protocol + "//" + location.host + '/file/tournamentLogo',
                dropzone: ko.observable(),
                initFunc: function () {
                    this.on("success",
                        function (file, result) {
                            if (result.isSuccess) {
                                vm.model().logoId(result.data.id);
                                vm.model().logoUrl(result.data.url);
                            }
                        });
                    this.on("addedfile", function () {
                        if (this.files[1] != null) {
                            this.removeFile(this.files[0]);
                        }
                    });
                }
            };
            vm.addPlayer = function () {
                vm.model().players.push((mappings.fromJS(ko.toJS(vm.model().emptyPlayer))));
            };
            vm.removePlayer = function (data) {
                vm.model().players.remove(data);
            };
            vm.addAdmin = function (data) {
                let dataObject = {
                    tournamentId: vm.model().id(),
                    id: data.id
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
                        helpers.log(result.message, 'success');
                        vm.model().admins.push({ id: data.id, name: data.name });
                        vm.notAdmins.remove(data);
                    }
                    return true;
                };
                return tournamentRepository.addAdmin(dataObject, callback);
            };
            vm.removeAdmin = function (data) {
                let dataObject = {
                    tournamentId: vm.model().id(),
                    id: data.id
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
                        helpers.log(result.message, 'success');
                        vm.notAdmins.push({ id: data.id, name: data.name });
                        vm.model().admins.remove(data);
                    }
                    return true;
                };
                return tournamentRepository.removeAdmin(dataObject, callback);
            };

            vm.editTournament = function () {
                if (vm.modelErrors().length > 0) {
                    vm.modelErrors.showAllMessages();
                    return false;
                }

                let dataObject = ko.toJS(vm.model);
                dataObject.defaultDayOfTheWeek = vm.selectedDay()?.value;
                vm.globalModel.isBusy(true);
                let callback = function (result) {
                    vm.globalModel.spinner(false);
                    vm.globalModel.isBusy(false);
                    if (!result.isSuccess) {
                        helpers.log(result.message, 'error');
                        return false;
                    }
                    else {
                        messageQueue.addMessage(translations.tournamentEdit.edited, 'success');
                        window.location.reload();
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
                    vm.globalModel.spinner(false);
                    vm.isBusy(false);
                    if (!result.isSuccess) {
                        helpers.log(result.message, 'error');
                        return false;
                    }
                    else {
                        messageQueue.addMessage(translations.tournamentEdit.matchCreated, 'success');
                        $(location).attr('href', '/match/edit/' + result.data);
                    }
                    return true;
                };
                dataObject.tournamentId = vm.model().id();
                return matchRepository.add({ homeTeamName: dataObject.homeTeamName, awayTeamName: dataObject.awayTeamName, tournamentId: dataObject.tournamentId }, callback);
            };

            vm.editPlayers = function () {
                if (vm.modelErrors().length > 0) {
                    vm.modelErrors.showAllMessages();
                    return false;
                }

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
                        messageQueue.addMessage(translations.tournamentEdit.playersSaved, 'success');
                        $(location).attr('href', '/tournament/edit/' + vm.model().id());
                    }
                    return true;
                };
                return playerRepository.edit({ players: dataObject, tournamentId: vm.model().id() }, callback);
            };

            vm.cancelInvite = function () {
                vm.showModal(false);
            }

            vm.inviteAsPlayer = function () {
                if (vm.inviteError().length > 0) {
                    vm.inviteError.showAllMessages();
                    return false;
                }

                let dataObject = {
                    playerId: vm.invitedPlayer().id(),
                    email: vm.invitedPlayer().email()
                };
                vm.globalModel.isBusy(true);
                vm.globalModel.spinner(true);
                let callback = function (result) {
                    vm.globalModel.spinner(false);
                    vm.globalModel.isBusy(false);
                    if (!result.isSuccess) {
                        helpers.log(result.message, 'error');
                    }
                    else {
                        helpers.log(result.message, 'success');
                    }
                    vm.showModal(false);
                    return true;
                };
                return authenticationRepository.invitePlayer(dataObject, callback);
            };

            vm.inviteAsAdmin = function () {
                if (vm.inviteError().length > 0) {
                    vm.inviteError.showAllMessages();
                    return false;
                }

                let dataObject = {
                    playerId: vm.invitedPlayer().id(),
                    email: vm.invitedPlayer().email()
                };
                vm.globalModel.isBusy(true);
                vm.globalModel.spinner(true);
                let callback = function (result) {
                    vm.globalModel.spinner(false);
                    vm.globalModel.isBusy(false);
                    if (!result.isSuccess) {
                        helpers.log(result.message, 'error');
                    }
                    else {
                        helpers.log(result.message, 'success');
                    }
                    vm.showModal(false);
                    return true;
                };
                return authenticationRepository.inviteAdmin(dataObject, callback);
            };

            vm.invitePlayer = function (data) {
                vm.showModal(true);
                vm.invitedPlayer().id(data.id());
                vm.invitedPlayer().email('');
            };

            vm.newMatchErrors = ko.validation.group([
                vm.newMatch().homeTeamName,
                vm.newMatch().awayTeamName]);

            vm.modelErrors = ko.validation.group([
                vm.model().name,
                vm.model().address
            ]);

            vm.inviteError = ko.validation.group([
                vm.invitedPlayer().email
            ]);

            return vm;
        }

        return ViewModel;
    });