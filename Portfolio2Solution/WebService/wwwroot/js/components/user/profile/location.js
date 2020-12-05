define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser().currentUser())

        let streetNumber = ko.observable();
        let streetName = ko.observable();
        let zipCode = ko.observable();
        let city = ko.observable();
        let country = ko.observable();

  
            let url = new URL(currentUser());
            ds.getUser(url.pathname, function (data) {
                streetNumber(data.streetNumber);
                streetName(data.streetName);
                zipCode(data.zipCode);
                city(data.city);
                country(data.country);

            });



        return {
            streetNumber,
            streetName,
            zipCode,
            city,
            country
        }
    }
});