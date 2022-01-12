requirejs.config({
    baseUrl: '/lib',
    paths: {
        jquery: 'jquery/jquery',
        knockout: 'knockout/knockout-latest.min',
        knockoutValidation: 'knockout-validation/knockout.validation.min',
        toastr: 'toastr.js/toastr.min',
        pikaday: 'pikaday/pikaday.min',
        pikadayJquery: 'pikaday/pikaday.jquery',
        amplify: 'amplifyjs/amplify.min',
        moment: 'moment.js/moment.min',
        bootstrap: 'bootstrap/js/bootstrap.bundle',
        floatLabel: 'floatlabels.js/floatlabels.min',
        bootbox: 'bootbox.js/bootbox.min',     

        knockoutWithAddons: '/js/helpers/knockoutWithAddons',
        knockoutToWindow: '/js/helpers/knockoutToWindow',
        helpers: '/js/helpers/helpers',
        knockoutPaged: '/js/helpers/knockout-paged-list',
        knockoutBindings: '/js/helpers/knockout.bindings',
        knockoutExtenders: '/js/helpers/knockout.extenders',
        storageHelper: '/js/helpers/storage',
        messageQueue: '/js/helpers/messageQueue',
        loginModel: '/js/models/loginModel',
        globalModel: '/js/models/globalModel',
        baseRepository: '/js/repositories/baseRepository',
        layout: '/js/viewScripts/layout',
        loginRepository: '/js/repositories/loginRepository',

    },
    shim: {
        "knockoutValidation": {
            deps: ["knockout"]
        },
        "knockoutPaged": {
            deps: ["knockoutToWindow"]
        },
        "pikadayJquery": {
            deps: ["jquery"]
        },
        "amplify": {
            deps: ["jquery"],
            exports:'amplify'
        },
        "layout": {
            deps: ["bootstrap"],
        }
    }
});