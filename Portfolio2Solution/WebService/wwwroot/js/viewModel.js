define(['knockout', 'dataservice'], (ko, ds) => {
    let selectedComponent = ko.observable('home');
    let selectedPopularTitle = ko.observable();

    let currentParams = ko.observable({ selectedPopularTitle });

    let menuElements = ["Home", "About","Login", "Register"];

    let changeContent = element => {
        console.log(element);
        selectedComponent(element.toLowerCase());
    }

    return {
        selectedComponent,
        currentParams,
        menuElements,
        changeContent
    };
});