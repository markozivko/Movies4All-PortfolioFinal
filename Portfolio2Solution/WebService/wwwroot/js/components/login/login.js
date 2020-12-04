define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {
        let email = ko.observable().extend({ email: true });
        let password = ko.observable().extend({ required: true });

        let checkUser = () => {
            ds.login(email(), password(), data => {
                postman.publish('changeUser', data.userUrl);
                var delayInMilliseconds = 1000; //1 second
                setTimeout(function () {
                    postman.publish('switchToAccount', "account");
                }, delayInMilliseconds);
                
            });
        }

        


        return {
            checkUser,
            email,
            password
        }
    }
});