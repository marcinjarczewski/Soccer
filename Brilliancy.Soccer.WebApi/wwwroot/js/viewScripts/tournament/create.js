define(['knockoutWithAddons', 'knockoutMapping', 'messageQueue', 'globalModel', 'helpers', 'tournamentRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, messageQueue, globalModel, helpers, tournamentRepository, translations) {
        var ViewModel = function (options) {

            var mapping = {
                name: {
                    create: function (options) {
                        return ko.validatedObservable().extend({
                            required: { message: translations.validation.fieldEmpty }
                        });
                    }
                },
                address: {
                    create: function (options) {
                        return ko.validatedObservable().extend({
                            required: { message: translations.validation.fieldEmpty }
                        });
                    }
                }
            };

            var vm = {
                globalModel: globalModel(),
                isBusy: ko.observable(false),
            };
            vm.translations = translations.tournamentCreate;
            vm.model = ko.validatedObservable(mappings.fromJS(options.json, mapping));
            vm.addTournament = function () {
                var validation = ko.validation.group(vm, { deep: true });
                if (validation().length == 0) {
                    var dataObject = ko.toJS(vm.model);
                    vm.isBusy(true);
                    var callback = function (result) {
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
                    return tournamentRepository.create(dataObject, callback);
                }
                else {
                    validation.showAllMessages(true);
                }
            };
            return vm;
        }
        return ViewModel;
    });