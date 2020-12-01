define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let email = params.email;
        let password = params.password;
        let user = ko.observable();


        //let selectUser = user => {
        //    selectedPopularTitle(popularTitle);
        //    postman.publish('changeUser', user);
        //}
        let getUser = (email, password) => {
            ds.login(email, password, data => {
                user(data);
            });
        }
        getUser(email, password);

        return {
            user
            //,selectUser
        }

    }
});