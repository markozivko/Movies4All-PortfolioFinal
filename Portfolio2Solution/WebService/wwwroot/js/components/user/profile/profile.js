﻿define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser().currentUser());
        //console.log(currentUser)
        let firstName = ko.observable();
        let lastName = ko.observable();
        //let birthday = ko.observable();
        let isStaff = ko.observable();
        let email = ko.observable();
        let userName = ko.observable();


        let url = new URL(currentUser());
            ds.getUser(url.pathname, function (data) {
                firstName(data.fName);
                lastName(data.lName);
                //birthday(data.birthday.split(":")[0]);
                isStaff(data.isStaff);
                email(data.email);
                userName(data.userName);
            });


        return {
            firstName,
            lastName,
           // birthday,
            isStaff,
            email,
            userName
        }
    }
});