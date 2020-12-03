define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {
        let selectedComponent = ko.observable('home');

        postman.publish('swithToAccount', "navbar");
        postman.publish('logout', 'home');

        return {
            selectedComponent
        };
    }
});