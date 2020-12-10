define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser().currentUser()).extend({ deferred: true })

        let address = ko.observable();

        ds.getUser('api/users/' + currentUser(), function (data) {
            address(data.address);

        });

        return {
            address
        }
    }
});