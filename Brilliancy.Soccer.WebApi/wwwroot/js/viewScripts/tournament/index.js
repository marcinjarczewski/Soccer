define(['knockoutWithAddons', 'knockoutMapping', 'messageQueue', 'globalModel', 'helpers', 'tournamentRepository', "/js/plugins/i18n.js!/nls/translation.js", 'knockoutPaged'],
    function (ko, mappings, messageQueue, globalModel, helpers, tournamentRepository,translations, knockoutPaged) {
        var ViewModel = function (options) {
            var vm = {
                globalModel: globalModel(),
                translations: translations.tournamentIndex
            };
            var pagedViewModel = function (options) {
                var self = this;
                PagedList.call(self, options);

                self.localFilters = ko.observable().extend({ localStore: { key: "tournaments-filters" } });
                self.filter(self.localFilters() || {});
                //self.filter().term = ko.observable();
                //self.selectedItem = ko.observable();
                //self.validInput = ko.pureComputed(function () {
                //    return self.selectedItem() !== null;
                //});

                self.filterChanged = function (viewModel) {
                    self.localFilters(self.filter);
                    return viewModel.getList();
                };
            };
            var pagedModel = new pagedViewModel({
                url: "/tournament/getList",
                entriesPerPage: 5,
                queryOnLoad: true,
                clearLoadedDataOnError: true
            });

            vm.tournaments = ko.observable(pagedModel);
            return vm;
        }
        return ViewModel;
    });