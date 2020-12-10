define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {
        let selectedComponent = ko.observable('home').extend({ deferred: true });
        let selectedPopularTitle = ko.observable().extend({ deferred: true });

        let currentParams = ko.observable({ selectedPopularTitle }).extend({ deferred: true });

        let menuElements = ["Home", "About", "Login", "Register"];

        let changeContent = element => {
            selectedComponent(element.toLowerCase());
        }

        let isActive = element => {
            element.toLowerCase() === selectedComponent() ? "active" : "";
        }

        return {
            selectedComponent,
            currentParams,
            menuElements,
            changeContent,
            isActive
        };
    }
});