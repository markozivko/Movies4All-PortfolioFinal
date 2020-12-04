define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let selectedComponent = ko.observable('my profile');
        let selectedLatestTitle = ko.observable();
        let latestTitle = ko.observable();

        let currentParams = ko.observable({ selectedLatestTitle, latestTitle});

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

        postman.subscribe('changeLatestTitle', title => {

            selectedComponent('latest-title-details');
            latestTitle(title);
            postman.publish('goToLatestTitleDetails', title);

        });

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