define(['knockoutWithAddons', 'globalModel', 'knockoutMapping', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, globalModel, mappings, translations) {
        var ViewModel = function (options) {
            var vm = {
                globalModel: globalModel(),
                translations: translations.invitePlayer
            };

            vm.model = ko.observable(mappings.fromJS(options.json));
            vm.badgeClass = ko.pureComputed(function () {
                return vm.model().isKeyValid() ? "bg-success" : "bg-danger";
            });
            return vm;
        }
        return ViewModel;
    });