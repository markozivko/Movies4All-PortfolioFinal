define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {

        postman.publish('switchToAccount', ["navbar", null]);

        return {

        };
    }
});