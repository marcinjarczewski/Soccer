define(['storageHelper', 'helpers'], function (storageHelper, helpers) {
    var messageQueue = {
        addMessage: function (message, level) {
            var currentMessageQueue = storageHelper.readLocalStorage("messageQueue");
            var messageObj = {
                message: message,
                level: level
            };
            var messageArray = currentMessageQueue != null ? currentMessageQueue : new Array();
            messageArray.push(messageObj);
            storageHelper.writeLocalStorage("messageQueue", messageArray);
        },
        viewAllMessages: function () {
            var currentMessageQueue = storageHelper.readLocalStorage("messageQueue");
            if (Array.isArray(currentMessageQueue)) {
                for (var i = 0; i < currentMessageQueue.length; i++) {
                    helpers.log(currentMessageQueue[i].message, currentMessageQueue[i].level)
                }
                storageHelper.writeLocalStorage("messageQueue", new Array());
            }
        },
    };

    return messageQueue;
});