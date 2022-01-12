define(['amplify', 'helpers', 'knockout'], function (amplify, helpers, ko) {   
    amplify.request.decoders.globalDecoder =
        function (data, status, xhr, success, error) {
            debugger;
            model.spinner(false);
            if (data == null) {
                helpers.log('Unexpected error', 'error');
                error('Unexpected error', "fatal");
                return;
            }
            if (data.isSuccess) {
                success(data);
            } else if (data.isSuccess == false) {
                if (data.message) {
                    helpers.log(data.message, 'error');
                }
                error(data, "error");
            } else {
                helpers.log('Unexpected error', 'error');
                error('Unexpected error', "fatal");
            }
        };
    var model = { spinner: ko.observable(false) };
    return model;
});