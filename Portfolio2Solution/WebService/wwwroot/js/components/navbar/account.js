define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let selectedComponent = ko.observable('latest');
        let selectedLatestTitle = ko.observable();
        let currentUser = ko.observable(params)
    
        let menuElements = ["Latest", "Genres", "Browse", "Profile"];

        let changeContent = element => {
            console.log(element);
            selectedComponent(element.toLowerCase());
        }
        let isActive = element => {
            element.toLowerCase() === selectedComponent() ? "active" : "";
        }
        let currentParams = ko.observable({ selectedLatestTitle, currentUser });
        return {
            selectedComponent,
            currentParams,
            menuElements,
            changeContent,
            isActive

        };
    }
});