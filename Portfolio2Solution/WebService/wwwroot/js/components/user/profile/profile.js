define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //let user = params.userUrl;

        let firstName = ko.observable();
        let lastName = ko.observable();
        let birthday = ko.observable();
        let isStaff = ko.observable();
        let email = ko.observable();
        let userName = ko.observable();

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

            });

        });

        return {
            firstName,
            lastName,
            birthday,
            isStaff,
            email,
            userName
        }
    }
});