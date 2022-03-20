define(['knockoutWithAddons', 'messageQueue', 'globalModel', 'helpers', 'loginRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, messageQueue, globalModel, helpers, loginRepository, translations) {
        var ViewModel = function (options) {
            function send() {
            var validation = ko.validation.group(vm, { deep: true });
            if (validation().length > 0) {
                validation().showAllMessages();
                return false;
            }
            if (vm.isBusy()) {
                return false;
            }
            vm.globalModel.spinner(true);
            vm.isBusy(true);
                var data = {
                    email:vm.email()
                    };
            var callback = function (result) {
                vm.globalModel.spinner(false);
                vm.isBusy(false);
                if (!result.isSuccess) {
                    helpers.log(result.message, 'error');
                    return false;
                }
                else {
                    helpers.log(result.message, 'success');
                }
                return true;
            };
            return loginRepository.lostPassword(data, callback);
        }
        var vm = {
            globalModel: globalModel(),
            isBusy: ko.observable(false),
            email: ko.observable("").extend({ required: { message: translations.validation.fieldEmpty } }),
            translations: translations.loginLostPassword,
            send: send
        };

        return vm;
    }
    return ViewModel;
});