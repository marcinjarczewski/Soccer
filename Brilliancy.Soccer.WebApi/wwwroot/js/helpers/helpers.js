define(['jquery', 'knockout', 'moment', 'knockoutValidation', 'toastr'], function ($, ko, moment, koValidation, toastr) {
    var Helpers = function () {
    };

    Helpers.prototype.date = function viewDate(date, format) {
        if (format === null
            || format === undefined) {
            format = 'YYYY-MM-DD';
        }

        if (date != null) {
            return moment(date).format(format);
        }
        return '';
    };


    Helpers.prototype.showError = function validOpts(data) {
        if (!data) {
            return helpers.log('nieznany błąd', 'error');
        }
        if (data == null || data.responseJSON == null || data.responseJSON.exceptionMessage == null) {
            return helpers.log(ko.toJSON(data), 'error');
        }
        helpers.log(ko.toJSON(data.responseJSON.exceptionMessage), 'error');
    };


    Helpers.prototype.valid = function validOpts(self, show) {
        var result = ko.validation.group(self, { deep: true });
        if (show == true) {
            if (typeof (self.sendModel) !== 'undefined' && !self.sendModel.isValid()) {
                result.showAllMessages(true);
                return false;
            }
            else if (typeof (self.model) !== 'undefined' && !self.model.isValid()) {
                result.showAllMessages(true);
                return false;
            }
        }
        else {
            result.showAllMessages(false);
        }
        return true;
    };

    Helpers.prototype.log = function validOpts(message, type) {
        if (type === 'error') {
            toastr.error(message);
        } else if (type === 'info') {
            toastr.info(message);
        } else if (type === 'success') {
            toastr.success(message);
        } else {
            toastr.info(message);
        }
    };

    return new Helpers();
});