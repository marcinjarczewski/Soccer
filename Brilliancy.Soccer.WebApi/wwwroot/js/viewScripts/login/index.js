define(['knockoutWithAddons', 'messageQueue', 'globalModel', 'helpers', 'loginRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (ko, messageQueue, globalModel, helpers, loginRepository, translations) {
    var ViewModel = function (options) {
        function register() {
            if (vm.registerValidationErrors().length > 0) {
                vm.registerValidationErrors.showAllMessages();
                return false;
            }
            if (vm.isBusy()) {
                return false;
            }
            vm.globalModel.spinner(true);
            vm.isBusy(true);
            var data = ko.toJS(account);
            var callback = function (result) {
                vm.globalModel.spinner(false);
                vm.isBusy(false);
                if (!result.isSuccess) {
                    helpers.log(result.message, 'error');
                    return false;
                }
                else {
                    helpers.messageBox('Rejestracja zakończona', 'Dziękujemy za rejestrację! teraz możesz się zalogować');
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
            login: ko.observable().extend({ required: { message: 'Pole nie może być puste' } }),
            password: ko.observable().extend({ required: { message: 'Pole nie może być puste' } }),
            firstName: ko.observable().extend({ required: { message: 'Pole nie może być puste' } }),
            lastName: ko.observable(),
            email: ko.observable().extend({ required: { message: 'Pole nie może być puste' }, email: { message: 'Nieprawidłowy format adresu email' } }),
            isRulesAccepted: ko.observable(false).extend({ mustCheck: true }),
            confirmPassword: ko.observable().extend({ required: { message: 'Pole nie może być puste' } })
        });
        var vm = {
            globalModel: globalModel(),
            isBusy: ko.observable(false),
            userName: ko.observable("").extend({ required: { message: 'Pole nie może być puste' } }),
            password: ko.observable("").extend({ required: { message: 'Pole nie może być puste' } }),
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