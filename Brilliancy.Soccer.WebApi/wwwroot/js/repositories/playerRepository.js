define(['jquery', 'storageHelper', 'messageQueue', 'amplify', 'baseRepository'], function ($, storageHelper, messageQueue, amplify, baseRepository) {
    var apiUrl = '/player/';
    var playerRepository = {
        edit: function (data, callback) {
            return amplify.request({
                resourceId: "editPlayers",
                data: data,
                success: callback,
            });
        },
        init: function () {
            amplify.request.define("editPlayers", "ajax", {
                url: apiUrl + "Edit",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
        }
    };
    playerRepository.init();
    return playerRepository;
});