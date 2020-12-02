define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {
        let selectedComponent = ko.observable('home');
        let selectedPopularTitle = ko.observable();

        let currentParams = ko.observable({ selectedPopularTitle });

        let menuElements = ["Home", "About", "Login", "Register", "Profile"];

        let changeContent = element => {
            console.log(element);
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