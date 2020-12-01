define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    let selectedComponent = ko.observable('home');
    let selectedPopularTitle = ko.observable();
    let userUrl = ko.observable();

    let currentParams = ko.observable({ selectedPopularTitle });

    let menuElements = ["Home", "About", "Profile", "Login", "Register"];

    let changeContent = element => {
        console.log(element);
        selectedComponent(element.toLowerCase());
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