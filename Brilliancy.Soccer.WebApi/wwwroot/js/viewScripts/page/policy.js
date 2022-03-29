define(['knockoutWithAddons', 'messageQueue', 'globalModel', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, messageQueue, globalModel, translations) {
        var ViewModel = function () {         
            var vm = {
                globalModel: globalModel(),
                translations: translations.pagePolicy
            };
            return vm;
        }
        return ViewModel;
    });