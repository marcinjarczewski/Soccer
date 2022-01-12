define(['jquery','knockout'], function ($,ko) {
    //needed for knockout-paged-list.js. Can't use "exports:'ko'" in knockout.js, because the library checks for init method(AMD or standard). 
    window.ko = ko;
    return ko;
});