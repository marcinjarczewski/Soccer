define(['knockoutWithAddons', 'knockoutMapping', 'messageQueue', 'helpers', 'globalModel', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, messageQueue, helpers, globalModel, translations) {
        var ViewModel = function (options) {
            var vm = {
                globalModel: globalModel(),
                isBusy: ko.observable(false),
                isTournamentAdmin: options.isTournamentAdmin,
                helpers: helpers
            };
            vm.showModal = ko.observable(false);    
            vm.translations = translations.tournamentDetails;
            vm.model = ko.validatedObservable(mappings.fromJS(options.json));
            for (var i = 0; i < vm.model().players().length; i++) {
                vm.model().players()[i].randClass = "fa-brands fa-accessible-icon";
                let sum = 0;
                sum = (vm.model().players()[i].firstName() ?? '').length * 3;
                sum += (vm.model().players()[i].lastName() ?? '').length * 7;
                sum += (vm.model().players()[i].nickName() ?? '').length * 11;
                if (sum % 5 == 0) {
                    vm.model().players()[i].randClass = "fa-solid fa-person-military-rifle";
                }
                if (sum % 5 == 1) {
                    vm.model().players()[i].randClass = "fa-solid fa-user-injured";
                }
                if (sum % 5 == 2) {
                    vm.model().players()[i].randClass = "fa-solid fa-person-walking-with-cane";
                }
                if (sum % 5 == 3) {
                    vm.model().players()[i].randClass = "fa-solid fa-person-running";
                }
            }
            return vm;
        }

        return ViewModel;
    });