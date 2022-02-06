define(['storageHelper', 'knockout', 'loginModel', 'messageQueue', 'baseRepository', "/js/plugins/i18n.js!/nls/translation.js"],
    function (storageHelper, ko, LoginModel, MessageQueue, baseRepository, translations) {
        var GlobalModel = function (options) {
        var self = {};
        // bind login model
        self.loginModel = new LoginModel();
        //show messages from queue
        MessageQueue.viewAllMessages();
        //init spinner
        self.spinner = ko.observable(false);
        baseRepository.spinner = self.spinner;
        //set language
        self.changeLanguage = function (name) {
            storageHelper.saveLanguage(name);
            window.location.reload(true);
            };
        self.translations = translations?.layout;
        return self;
    };
    return GlobalModel;
});