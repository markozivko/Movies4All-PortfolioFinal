define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let selectedComponent = ko.observable('my profile');
        let selectedLatestTitle = ko.observable();

        let currentParams = ko.observable({ selectedLatestTitle });

        let menuElements = ["My Profile", "Location", "Title Bookmarks", "Personalities", "Logout"];

        let changeContent = element => {
            console.log(element);
            selectedComponent(element.toLowerCase());
        }
        let isActive = element => {
            element.toLowerCase() === selectedComponent() ? "active" : "";
        }

        postman.subscribe('logout', component => {

            selectedComponent(component);

        });

        return {
            selectedComponent,
            currentParams,
            menuElements,
            changeContent,
            isActive
        };
    }
});