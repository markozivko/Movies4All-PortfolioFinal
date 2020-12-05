define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser().currentUser())

        let address = ko.observable();


        let url = new URL(currentUser());
        ds.getUser(url.pathname, function (data) {
            address(data.address);

        });

        return {
            address
        }
    }
});