define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let email = ko.observable().extend({ email: true }),;
        let password = ko.observable().extend({ required: true }),;
        let user = ko.observable();


        let checkUser = (email, password) => {
            ds.login(email, password, data => {
                user(data);
            });
            alert(user.email)
        }
        checkUser(email(), password());

        return {
            user,
            checkUser
 
        }

    }
});