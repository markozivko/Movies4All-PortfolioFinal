define(['knockout', 'dataservice'], (ko, ds) => {
    let selectedComponent = ko.observable('title-list');
    let selectedPopularTitle = ko.observable();
    let currentParams = ko.observable({ selectedPopularTitle });

    let changeContent = () => {
        if (selectedComponent() === "title-list") {
            currentParams({ popularTitle: selectedPopularTitle });
            selectedComponent('title-details');
        } else {
            currentParams({ selectedPopularTitle });
            selectedComponent('title-list');
        }
    }
    return {
        selectedComponent,
        currentParams,
        changeContent
    };
});