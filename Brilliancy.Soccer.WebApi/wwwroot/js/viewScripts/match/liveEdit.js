define(['knockoutWithAddons', 'knockoutMapping', 'moment', 'messageQueue', 'globalModel', 'helpers',
     'matchRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, moment, messageQueue, globalModel, helpers, matchRepository, translations) {
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

            vm.showModal = ko.observable(false);
            vm.showHomeTeam = ko.observable(true);
            vm.isOwngoal = ko.observable(false);
            vm.newGoal = ko.observable(mappings.fromJS(ko.toJS(vm.model().emptyGoal)));
            vm.newGoalStep = ko.observable(1);
            vm.selectScorer = function (data) {
                vm.newGoal().scorerId(data.id());
                vm.newGoal().scorerPlayerName(data.firstName() + " " + data.nickName() + " " + data.lastName());
                vm.newGoalStep(2);
            }

            vm.selectAssist = function (data) {
                vm.newGoal().assistId(data.id());
                vm.newGoal().assistPlayerName(data.firstName() + " " + data.nickName() + " " + data.lastName());
                vm.newGoalStep(3);
            }

            vm.ownGoal = function () {
                vm.newGoalStep(1);
                vm.isOwngoal(!vm.isOwngoal());
                vm.showHomeTeam(!vm.showHomeTeam());
            }

            vm.stepBack = function () {
                vm.newGoalStep(vm.newGoalStep() -1);
            }

            vm.cancelNewGoal = function () { 
                vm.newGoalStep(1);
                vm.isOwngoal(false);
                vm.showModal(false);
            }

            vm.noAssist = function () {
                vm.newGoalStep(3);
                vm.newGoal().assistPlayerName('');
                vm.newGoal().assistId(null);
            }

            vm.removeHomeGoal = function (data) {
                let dataObject = {
                    id: vm.model().id(),
                    goal: {
                        id: data.id()
                    }
                };
                vm.globalModel.isBusy(true);
                let callback = function (result) {
                    vm.globalModel.spinner(false);
                    vm.globalModel.isBusy(false);
                    vm.model().homeGoalsList.remove(data);
                    vm.model().homeGoals(vm.model().homeGoalsList().length);
                    if (!result.isSuccess) {
                        helpers.log(result.message, 'error');
                        return false;
                    }
                    else {
                        helpers.log(translations.matchEdit.goalsSaved, 'success');
                    }
                    return true;
                };
                matchRepository.removeGoal(dataObject, callback);
            }

            vm.removeAwayGoal = function (data) {
                let dataObject = {
                    id: vm.model().id(),
                    goal: {
                        id: data.id()
                    }
                };
                vm.globalModel.isBusy(true);
                let callback = function (result) {
                    vm.globalModel.spinner(false);
                    vm.globalModel.isBusy(false);
                    vm.model().awayGoalsList.remove(data);
                    vm.model().awayGoals(vm.model().awayGoalsList().length);
                    if (!result.isSuccess) {
                        helpers.log(result.message, 'error');
                        return false;
                    }
                    else {
                        helpers.log(translations.matchEdit.goalsSaved, 'success');
                    }
                    return true;
                };
                matchRepository.removeGoal(dataObject, callback);
            }

            vm.saveGoal = function () {
                vm.newGoal().time(Math.ceil(vm.timer() / 60));
                let dataObject = {
                    id: vm.model().id(),
                    goal: {
                        assistId: vm.newGoal().assistId(),
                        isOwnGoal: vm.newGoal().isOwnGoal(),
                        scorerId: vm.newGoal().scorerId(),
                        time: vm.newGoal().time()
                    }
                };
                if ((vm.showHomeTeam() && !vm.isOwngoal()) || (!vm.showHomeTeam() && vm.isOwngoal())) {
                    vm.model().homeGoalsList.push(vm.newGoal());
                    dataObject.goal.isHomeTeam = true;
                }
                else {
                    vm.model().awayGoalsList.push(vm.newGoal());
                    dataObject.goal.isHomeTeam = false;
                }

           
                vm.globalModel.isBusy(true);
                let callback = function (result) {
                    vm.globalModel.spinner(false);
                    vm.globalModel.isBusy(false);
                    vm.newGoal(mappings.fromJS(ko.toJS(vm.model().emptyGoal)));
                    vm.model().homeGoals(vm.model().homeGoalsList().length);
                    vm.model().awayGoals(vm.model().awayGoalsList().length);
                    vm.newGoalStep(1);
                    vm.showModal(false);
                    vm.isOwngoal(false);
                    if (!result.isSuccess) {
                        helpers.log(result.message, 'error');
                        return false;
                    }
                    else {
                        helpers.log(translations.matchEdit.goalsSaved, 'success');
                    }
                    return true;
                };
                matchRepository.addGoal(dataObject, callback);
            }
      
            vm.matchErrors = ko.validation.group([
                vm.model().homeTeamName,
                vm.model().awayTeamName]);

            vm.addHomeGoal = function () {
                vm.showHomeTeam(true);
                vm.isOwngoal(false);
                vm.showModal(true);
            }

            vm.timer = ko.observable(600);

            function setTimer() {
                vm.timer(vm.timer() + 1);
            }

            vm.timerEvent = setInterval(setTimer, 1000);

            vm.addAwayGoal = function () {
                vm.showHomeTeam(false);
                vm.isOwngoal(false);
                vm.showModal(true);
            }

            vm.editCreating = function () {
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
                        messageQueue.addMessage(translations.matchEdit.teamsSaved, 'success');
                        $(location).attr('href', '/login/test');
                    }
                    return true;
                };
                return matchRepository.editCreating(dataObject, callback);
            };

            vm.editPending = function () {
                let dataObject = {
                    homeGoalsList: ko.toJS(vm.model().homeGoalsList()),
                    awayGoalsList: ko.toJS(vm.model().awayGoalsList()),
                    id: vm.model().id(),
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
                        messageQueue.addMessage(translations.matchEdit.goalsSaved, 'success');
                        $(location).attr('href', '/login/test');
                    }
                    return true;
                };
                return matchRepository.editPending(dataObject, callback);
            };

            vm.changeState = function () {
                if (vm.matchErrors().length > 0) {
                    vm.matchErrors.showAllMessages();
                    return false;
                }

                let dataObject = {
                    id: vm.model().id()
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
                        messageQueue.addMessage(translations.matchEdit.teamsConfirmed, 'success');
                        $(location).attr('href', '/login/test');
                    }
                    return true;
                };
                return matchRepository.changeToPending(dataObject, callback);
            };

            vm.goLive = function () {
                let dataObject = {
                    id: vm.model().id()
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
                        messageQueue.addMessage(translations.matchEdit.matchLive, 'success');
                        $(location).attr('href', '/login/test');
                    }
                    return true;
                };
                return matchRepository.changeToOngoing(dataObject, callback);
            };

            vm.goFinished = function () {
                let dataObject = {
                    id: vm.model().id()
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
                        messageQueue.addMessage(translations.matchEdit.teamsConfirmed, 'success');
                        $(location).attr('href', '/login/test');
                    }
                    return true;
                };
                return matchRepository.changeToFinished(dataObject, callback);
            };

            return vm;
        }

        return ViewModel;
    });