define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {
        let email = ko.observable().extend({ email: true });
        let password = ko.observable().extend({ required: true });


        let checkUser = () => {
            ds.login(email(), password(), data => {
                postman.publish('switchToAccount',["account",data.userUrl.split("/").pop()]);
            });
        }

        return {
            checkUser,
            email,
            password
        }
    }
});