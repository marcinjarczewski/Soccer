define(['knockoutWithAddons', 'knockoutMapping', 'messageQueue', 'globalModel', 'helpers',
    'tournamentRepository', 'playerRepository', 'matchRepository', 'authenticationRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, messageQueue, globalModel, helpers, tournamentRepository, playerRepository, matchRepository, authenticationRepository, translations) {
        var ViewModel = function (options) {
            var vm = {
                globalModel: globalModel(),
                isBusy: ko.observable(false),
            };
            vm.showModal = ko.observable(false);    
            vm.translations = translations.tournamentDetails;
            vm.model = ko.validatedObservable(mappings.fromJS(options.json));              
            return vm;
        }

        return ViewModel;
    });