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
                defaultHour: {
                    create: function (options) {
                        return ko.validatedObservable().extend({
                            hour: { message: translations.validation.hour }
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
                translations: translations.tournamentCreate,
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
                    }
                ]
            };
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
            vm.selectedDay = ko.observable(vm.days[0]);
            vm.model = ko.validatedObservable(mappings.fromJS(options.json, mapping));
            vm.addTournament = function () {
                var validation = ko.validation.group(vm, { deep: true });
                if (validation().length == 0) {
                    var dataObject = ko.toJS(vm.model);
                    dataObject.defaultDayOfTheWeek = vm.selectedDay()?.value;
                    vm.globalModel.isBusy(true);
                    var callback = function (result) {
                        vm.globalModel.spinner(false);
                        vm.globalModel.isBusy(false);
                        if (!result.isSuccess) {
                            helpers.log(result.message, 'error');
                            return false;
                        }
                        else {
                            messageQueue.addMessage(translations.tournamentCreate.created, 'success');
                            $(location).attr('href', '/tournament/edit/' + result.data);
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