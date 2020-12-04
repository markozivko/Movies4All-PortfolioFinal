define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {
        let email = ko.observable().extend({ email: true });
        let password = ko.observable().extend({ required: true });

        let checkUser = () => {
            ds.login(email(), password(), data => {
                postman.publish('changeUser', data.userUrl);
                postman.publish('switchToAccount', "account");
            });
        }

       
        return {
            checkUser,
            email,
            password
        }
    }
});