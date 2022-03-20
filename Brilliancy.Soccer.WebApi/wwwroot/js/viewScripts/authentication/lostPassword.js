define(['knockoutWithAddons', 'knockoutMapping',  'messageQueue', 'globalModel', 'helpers', 'loginRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, mappings, messageQueue, globalModel, helpers, loginRepository, translations) {
        var ViewModel = function (options) {
            function send() {
                var validation = ko.validation.group(vm, { deep: true });
                if (validation().length > 0) {
                    validation.showAllMessages();
                    return false;
                }
                if (vm.globalModel.isBusy()) {
                    return false;
                }
                vm.globalModel.spinner(true);
                vm.globalModel.isBusy(true);
                var data = {
                    password: vm.password(),
                    repeatPassword: vm.repeatPassword(),
                    authId: vm.authId()
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
                return loginRepository.changePassword(data, callback);
            }
            var vm = {
                globalModel: globalModel(),
                password: ko.observable("").extend({ required: { message: translations.validation.fieldEmpty } }),
                translations: translations.authenticationLostPassword,
                send: send
            };

            vm.repeatPassword = ko.observable().extend({ equalPasswords: vm.password });
            vm.authId = ko.observable(mappings.fromJS(options.json).authId);
            return vm;
        }
        return ViewModel;
    });