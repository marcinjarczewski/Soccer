define(['jquery', 'storageHelper', 'messageQueue', 'amplify', 'baseRepository'], function ($, storageHelper, messageQueue, amplify, baseRepository) {
    var apiUrl = '/tournament/';
    var tournamentRepository = {
        create: function (data, callback) {
            return amplify.request({
                resourceId: "createTournament",
                data: data,
                success: callback,
            });
        },
        edit: function (data, callback) {
            data.__ko_mapping__ = null;
            return amplify.request({
                resourceId: "editTournament",
                data: data,
                success: callback,
            });
        },
        init: function () {
            amplify.request.define("createTournament", "ajax", {
                url: apiUrl + "createTournament",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("editTournament", "ajax", {
                url: apiUrl + "editTournament",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
        }
    };
    tournamentRepository.init();
    return tournamentRepository;
});