define(['knockoutWithAddons', 'messageQueue', 'globalModel', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, messageQueue, globalModel, translations) {
        var ViewModel = function (options) {         
            var vm = {
                globalModel: globalModel(),
                translations: translations.pageHome,
                isLoggedIn: options.isLoggedIn
            };
            return vm;
        }
        return ViewModel;
    });