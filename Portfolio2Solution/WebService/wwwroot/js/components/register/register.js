define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {
        let email = ko.observable().extend({ email: true });
        let password = ko.observable().extend({ required: true });
        let FirstName = "A-gun-evil-twin";
        let LastName = "Aabidah2";
        let BirthDay = "01/08/2009";
        let IsStaff =  false;
        let Email =  "gun@super.com";
        let Password =  "gun123";
        let UserName =  "shootGub2009";
        let StreetNumber =  "21";
        let StreetName =  "Joyful";
        let ZipCode =  "35214";
        let City =  "Mumbai";
        let Country =  "India";

        ds.createUser({}, function (data) {
            console.log(data);
    });


        let checkUser = () => {
            ds.login(email(), password(), data => {
                postman.publish('switchToAccount', ["account", data.userUrl.split("/").pop()]);
            });
        }

        return {
            checkUser,
            email,
            password
        }
    }
});