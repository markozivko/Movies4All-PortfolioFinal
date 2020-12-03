define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let selectedComponent = ko.observable('profile');

        let menuElements = ["Profile", "Search"];

        let changeContent = element => {
            console.log(element);
            selectedComponent(element.toLowerCase());
        }
        let isActive = element => {
            element.toLowerCase() === selectedComponent() ? "active" : "";
        }
        return {
            selectedComponent,
            menuElements,
            changeContent,
            isActive

        };
    }
});