define(['jquery', 'storageHelper', 'messageQueue', 'amplify', 'baseRepository'], function ($, storageHelper, messageQueue, amplify, baseRepository) {
    var apiUrl = '/match/';
    var matchRepository = {
        add: function (data, callback) {
            return amplify.request({
                resourceId: "addMatch",
                data: data,
                success: callback,
            });
        },
        editCreating: function (data, callback) {
            return amplify.request({
                resourceId: "editCreatingMatch",
                data: data,
                success: callback,
            });
        },
        editPending: function (data, callback) {
            return amplify.request({
                resourceId: "editPendingMatch",
                data: data,
                success: callback,
            });
        },
        changeToPending: function (data, callback) {
            return amplify.request({
                resourceId: "changeToPending",
                data: data,
                success: callback,
            });
        },
        changeToOngoing: function (data, callback) {
            return amplify.request({
                resourceId: "changeToOngoing",
                data: data,
                success: callback,
            });
        },
        addGoal: function (data, callback) {
            return amplify.request({
                resourceId: "addGoal",
                data: data,
                success: callback,
            });
        },
        removeGoal: function (data, callback) {
            return amplify.request({
                resourceId: "removeGoal",
                data: data,
                success: callback,
            });
        },
        changeToFinished: function (data, callback) {
            return amplify.request({
                resourceId: "changeToFinished",
                data: data,
                success: callback,
            });
        },
        init: function () {
            amplify.request.define("addMatch", "ajax", {
                url: apiUrl + "add",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("editCreatingMatch", "ajax", {
                url: apiUrl + "editCreating",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("addGoal", "ajax", {
                url: apiUrl + "addGoal",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("removeGoal", "ajax", {
                url: apiUrl + "removeGoal",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("editPendingMatch", "ajax", {
                url: apiUrl + "editPending",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("changeToPending", "ajax", {
                url: apiUrl + "changeToPending",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("changeToOngoing", "ajax", {
                url: apiUrl + "changeToOngoing",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
            amplify.request.define("changeToFinished", "ajax", {
                url: apiUrl + "changeToFinished",
                dataType: "json",
                type: "POST",
                decoder: "globalDecoder",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", storageHelper.readCookie("token"));
                }
            });
        }
    };
    matchRepository.init();
    return matchRepository;
});