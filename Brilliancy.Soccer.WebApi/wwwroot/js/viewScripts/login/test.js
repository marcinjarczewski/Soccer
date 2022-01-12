define(['jquery', 'helpers', 'storageHelper', 'messageQueue', 'loginModel', 'knockoutPaged', 'globalModel'],
    function ($, helpers, storageHelper, messageQueue, loginModel, paged, globalModel) {
        messageQueue.addMessage('działa', 'success');
        //messageQueue.viewAllMessages();
        return { globalModel: globalModel(), test: 'aa' };
    });

//alert();