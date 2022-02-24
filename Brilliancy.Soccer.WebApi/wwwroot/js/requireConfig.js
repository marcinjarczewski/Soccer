var locale = localStorage['language'] || 'pl';
requirejs.config({
    baseUrl: '/lib',
    paths: {
        jquery: 'jquery/jquery',
        knockout: 'knockout/knockout-latest.min',
        knockoutValidation: 'knockout-validation/knockout.validation.min',
        knockoutMapping: 'knockout.mapping/knockout.mapping',
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
        layout: '/js/viewScripts/layout',
        baseRepository: '/js/repositories/baseRepository',
        loginRepository: '/js/repositories/loginRepository',
        playerRepository: '/js/repositories/playerRepository',
        tournamentRepository: '/js/repositories/tournamentRepository',
        matchRepository: '/js/repositories/matchRepository'
    },
    shim: {
        "knockoutValidation": {
            deps: ["knockout"]
        },
        "knockoutMapping": {
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
            deps: ["bootstrap"]
        }
    },
    config: {
        "/js/plugins/i18n.js": {
            locale: locale
        }
    }
});
