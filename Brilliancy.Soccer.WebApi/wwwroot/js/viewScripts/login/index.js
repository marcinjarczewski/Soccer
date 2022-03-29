define(['knockoutWithAddons', 'messageQueue', 'globalModel', 'helpers', 'loginRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, messageQueue, globalModel, helpers, loginRepository, translations) {
    var ViewModel = function (options) {
        function register() {
            if (vm.registerValidationErrors().length > 0) {
                vm.registerValidationErrors.showAllMessages();
                return false;
            }
            if (vm.globalModel.isBusy()) {
                return false;
            }
            vm.globalModel.spinner(true);
            vm.globalModel.isBusy(true);
            var data = ko.toJS(account);
            var callback = function (result) {
                vm.globalModel.spinner(false);
                vm.globalModel.isBusy(false);
                if (!result.isSuccess) {
                    helpers.log(result.message, 'error');
                    return false;
                }
                else {
                    messageQueue.addMessage(translations.loginIndex.registerCompletedDescription, 'success');
                    window.location.reload();
                }
                return true;
            };
            return loginRepository.register(data, callback);
        }
        function login() {
            if (vm.validationErrors().length > 0) {
                vm.validationErrors.showAllMessages();
                return;
            }
            return $.when(loginRepository.login(vm.userName(), vm.password()));
        }

        var account = ko.validatedObservable({
            login: ko.observable().extend({ required: { message:translations.validation.fieldEmpty } }),
            password: ko.observable().extend({ required: { message:translations.validation.fieldEmpty } }),
            firstName: ko.observable().extend({ required: { message:translations.validation.fieldEmpty } }),
            lastName: ko.observable(),
            email: ko.observable().extend({ required: { message: translations.validation.fieldEmpty }, email: { message: translations.validation.email } }),
            isRulesAccepted: ko.observable(false).extend({ mustCheck: true }),
            confirmPassword: ko.observable().extend({ required: { message:translations.validation.fieldEmpty } })
        });
        var vm = {
            globalModel: globalModel(),
            userName: ko.observable("").extend({ required: { message:translations.validation.fieldEmpty } }),
            password: ko.observable("").extend({ required: { message:translations.validation.fieldEmpty } }),
            rememberMe: ko.observable(false),
            account: account,
            register: register,
            login: login,
            translations: translations.loginIndex
        };
        vm.validationErrors = ko.validation.group([vm.userName, vm.password]);
        vm.registerValidationErrors = ko.validation.group([
            vm.account().login,
            vm.account().password,
            vm.account().confirmPassword,
            vm.account().firstName,
            vm.account().email,
            vm.account().isRulesAccepted,
        ]);
        return vm;
    }
    return ViewModel;
});