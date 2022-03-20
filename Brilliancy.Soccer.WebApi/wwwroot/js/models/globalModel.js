define(['storageHelper', 'knockout', 'loginModel', 'messageQueue', 'baseRepository', 'loginRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (storageHelper, ko, LoginModel, MessageQueue, baseRepository, loginRepository, translations) {
        var GlobalModel = function (options) {
        var self = {};
        // bind login model
        self.loginModel = new LoginModel();
        //show messages from queue
        MessageQueue.viewAllMessages();
        //init spinner
        self.spinner = ko.observable(false);
        self.isBusy = ko.observable(false);
        baseRepository.spinner = self.spinner;
        baseRepository.isBusy = self.isBusy;
        //set language
        self.changeLanguage = function (name) {
            storageHelper.saveLanguage(name);
            var callback = function () {
                window.location.reload(true);
            };
            loginRepository.changeLanguage({ name: name }, callback);
        };
        self.translations = translations?.layout;
        return self;
    };
    return GlobalModel;
});