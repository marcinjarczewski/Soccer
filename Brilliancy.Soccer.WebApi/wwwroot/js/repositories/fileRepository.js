define(['storageHelper', 'amplify'], function (storageHelper, amplify) {
    var apiUrl = '/file/';
    var playerRepository = {
        uploadTournamentLogo: function (data, callback) {
            return amplify.request({
                resourceId: "tournamentLogo",
                data: data,
                success: callback,
            });
        },
        init: function () {
            amplify.request.define("tournamentLogo", "ajax", {
                url: apiUrl + "TournamentLogo",
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