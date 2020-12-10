define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let selectedComponent = ko.observable('latest').extend({ deferred: true });
        let selectedLatestTitle = ko.observable().extend({ deferred: true });
        let currentUser = ko.observable(params).extend({ deferred: true });
        let role = ko.observable().extend({ deferred: true });
        let menuElements = ko.observableArray(["Latest", "Genres", "Browse", "Profile"]);

        ds.getUser('/api/users/' + currentUser().currentUser(), data => {
            role(data.role)
            console.log(role())
            if (role() === 'staff') {
                menuElements.push('Management');
            }
        });
        
        let changeContent = element => {
            console.log(element);
            selectedComponent(element.toLowerCase());
        }
        let isActive = element => {
            element.toLowerCase() === selectedComponent() ? "active" : "";
        }

        let goToHome = element => {
            selectedComponent('latest');
        }
        let currentParams = ko.observable({ selectedLatestTitle, currentUser }).extend({ deferred: true });
        return {
            selectedComponent,
            currentParams,
            menuElements,
            changeContent,
            isActive,
            goToHome

        };
    }
});