define(['storageHelper', 'knockout', 'loginModel', 'messageQueue', 'baseRepository'], function (storageHelper, ko, LoginModel, MessageQueue, baseRepository) {
    var GlobalModel = function (options) {
        var self = {};
        self.loginModel = new LoginModel();
        MessageQueue.viewAllMessages();
        self.spinner = ko.observable(false);
        baseRepository.spinner = self.spinner;
        return self;
    };
    return GlobalModel;
});