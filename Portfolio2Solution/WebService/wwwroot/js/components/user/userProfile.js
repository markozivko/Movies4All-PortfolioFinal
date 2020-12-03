define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //let user = params.userUrl;

        let firstName = ko.observable();
        let lastName = ko.observable();
        let birthday = ko.observable();
        let isStaff = ko.observable();
        let email = ko.observable();
        let userName = ko.observable();
        let streetNumber = ko.observable();
        let streetName = ko.observable();
        let zipCode = ko.observable();
        let city = ko.observable();
        let country = ko.observable();

        postman.subscribe('changeUser', user => {
            let url = new URL(user);
            alert(url);
            ds.getUser(url.pathname, function (data) {

                firstName(data.firstName);
                lastName(data.lastName);
                birthday(data.birthday);
                isStaff(data.isStaff);
                email(data.email);
                userName(data.userName);
                streetNumber(data.streetNumber);
                streetName(data.streetName);
                zipCode(data.zipCode);
                city(data.city);
                country(data.country);

            });


        });

        return {
            firstName,
            lastName,
            birthday,
            isStaff,
            email,
            userName,
            streetNumber,
            streetName,
            zipCode,
            city,
            country
        }
    }
});