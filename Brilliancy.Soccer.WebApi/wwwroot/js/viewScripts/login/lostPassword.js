define(['knockoutWithAddons', 'messageQueue', 'globalModel', 'helpers', 'loginRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, messageQueue, globalModel, helpers, loginRepository, translations) {
        var ViewModel = function (options) {
            function send() {
                if (vm.validationErrors().length > 0) {
                    vm.validationErrors.showAllMessages();
                    return false;
                }
                if (vm.globalModel.isBusy()) {
                    return false;
                }
                vm.globalModel.spinner(true);
                vm.globalModel.isBusy(true);
                var data = {
                    email: vm.email()
                };
                var callback = function (result) {
                    vm.globalModel.spinner(false);
                    vm.globalModel.isBusy(false);
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
                email: ko.observable().extend({ required: { message: translations.validation.fieldEmpty }, email: { message: translations.validation.email } }),
                translations: translations.loginLostPassword,
                send: send
            };

            vm.validationErrors = ko.validation.group([vm.email]);
            return vm;
        }
        return ViewModel;
    });