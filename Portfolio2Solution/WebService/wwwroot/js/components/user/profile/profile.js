define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let emailparam = params.email
        let firstName = ko.observable();
        let lastName = ko.observable();
        //let birthday = ko.observable();
        let isStaff = ko.observable();
        let email = ko.observable();
        let userName = ko.observable();

        postman.subscribe('changeUser', user => {
            let url = new URL(user);
            ds.getUser(url.pathname, function (data) {
                firstName(data.firstName);
                lastName(data.lastName);
                //birthday(data.birthday.split(":")[0]);
                isStaff(data.isStaff);
                email(data.email);
                userName(data.userName);
                console.log(email())

            });
  

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