define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    let selectedComponent = ko.observable('profile');
    let selectedPopularTitle = ko.observable();
    let userUrl = ko.observable();

    let currentParams = ko.observable({ selectedPopularTitle });

    let menuElements = ["Profile", "Search"];

    let changeContent = element => {
        console.log(element);
        selectedComponent(element.toLowerCase());
    }
    let changeNavBar = element => {
        console.log(element);
        currentNavBar(element.toLowerCase());
    }
    let isActive = element => {
        element.toLowerCase() === selectedComponent() ? "active" : "";
    }

    //TODO: add user url
    //postman.subscribe("userLoggedIn", component => {
    //    changeContent(component);

    //}, );



    return {
        selectedComponent,
        currentParams,
        menuElements,
        changeContent,
        isActive

    };
});