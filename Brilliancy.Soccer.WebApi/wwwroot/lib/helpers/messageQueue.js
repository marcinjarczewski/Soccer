(function () {
    MessageQueue = {
        addMessage: function (message, level) {
            var currentMessageQueue = StorageHelper.readLocalStorage("messageQueue");
            var messageObj = {
                message: message,
                level: level
            };
            var messageArray = currentMessageQueue != null ? currentMessageQueue : new Array();
            messageArray.push(messageObj);
            StorageHelper.writeLocalStorage("messageQueue", messageArray);
        },
        viewAllMessages: function () {
            var currentMessageQueue = StorageHelper.readLocalStorage("messageQueue");
            if (Array.isArray(currentMessageQueue)) {
                for (var i = 0; i < currentMessageQueue.length; i++) {
                    helpers.log(currentMessageQueue[i].message, currentMessageQueue[i].level)
                }
                StorageHelper.writeLocalStorage("messageQueue", new Array());
            }
        },
    };
})();