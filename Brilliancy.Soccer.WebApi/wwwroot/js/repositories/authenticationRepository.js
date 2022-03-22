define(['storageHelper', 'amplify'], function (storageHelper, amplify) {
    var apiUrl = '/authentication/';
    var authenticationRepository = {
        invitePlayer: function (data, callback) {
            return amplify.request({
                resourceId: "invitePlayer",
                data: data,
                success: callback,
            });
        },
        inviteAdmin: function (data, callback) {
            return amplify.request({
                resourceId: "inviteAdmin",
                data: data,
                success: callback,
            });
        },
        init: function () {
            amplify.request.define("invitePlayer", "ajax", {
                url: apiUrl + "SendInvitationPlayer",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("inviteAdmin", "ajax", {
                url: apiUrl + "SendInvitationAdmin",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
        }
    };
    authenticationRepository.init();
    return authenticationRepository;
});