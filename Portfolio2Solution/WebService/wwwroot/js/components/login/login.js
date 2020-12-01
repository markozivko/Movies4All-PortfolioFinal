﻿define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let email = ko.observable().extend({ email: true });
        let password = ko.observable().extend({ required: true });
        let user = ko.observable();

        let checkUser = () => {
            ds.login(email(), password(), data => {
                user(data.userUrl);
            });
        }

        if (user() !== undefined) {
            goToUserProfile();
        }

        let goToUserProfile = () => {

            postman.publish('userLoggedIn', "profile", user);

        }
       
        return {
            user,
            checkUser,
            email,
            password,
            goToUserProfile
        }
    }
});