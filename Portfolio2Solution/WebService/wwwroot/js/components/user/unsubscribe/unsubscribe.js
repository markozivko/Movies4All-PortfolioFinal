define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let unsuscribe = () => {
            console.log(currentUser())
            ds.unsubscribeUser(currentUser());
            postman.publish('switchToAccount', ["navbar", null]);
        }
        return {
            unsuscribe
        };
    }
});