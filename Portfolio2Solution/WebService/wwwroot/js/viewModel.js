define(['knockout', 'dataservice'], (ko, ds) => {
    let selectedComponent = ko.observable('title-list');
    let selectedPopularTitle = ko.observable();
    let currentParams = ko.observable({ selectedPopularTitle });


    return {
        selectedComponent,
        currentParams
    };
});