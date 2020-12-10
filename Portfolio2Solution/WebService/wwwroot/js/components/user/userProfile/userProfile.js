define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let selectedComponent = ko.observable('my profile').extend({ deferred: true });
        let selectedLatestTitle = ko.observable().extend({ deferred: true });
        let latestTitle = ko.observable().extend({ deferred: true });
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });


        let menuElements = ["My Profile", "Location", "Title Bookmarks", "Personalities", "Search History", "My Ratings", "Logout"];

        let changeContent = element => {
            console.log(element);
            selectedComponent(element.toLowerCase());
        }
        let isActive = element => {
            element.toLowerCase() === selectedComponent() ? "active" : "";
        }
        let currentParams = ko.observable({ selectedLatestTitle, latestTitle, currentUser });
        return {
            selectedComponent,
            currentParams,
            menuElements,
            changeContent,
            isActive,
            latestTitle
        };
    }
});