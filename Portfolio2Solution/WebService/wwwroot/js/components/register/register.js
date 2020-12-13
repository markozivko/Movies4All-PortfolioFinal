define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function () {

        let firstName = ko.observable("").extend({ required: true });
        let lastName = ko.observable("").extend({ deferred: true });
        let birthDay = ko.observable().extend({ deferred: true });
        let isStaff = ko.observable(false).extend({ deferred: true });
        let email = ko.observable("").extend({ deferred: true });
        let password = ko.observable("").extend({ deferred: true });
        let repeatPassword = ko.observable("").extend({ deferred: true });
        let userName = ko.observable("").extend({ deferred: true });
        let streetNumber = ko.observable("").extend({ deferred: true });
        let streetName = ko.observable("").extend({ deferred: true });
        let zipCode = ko.observable("").extend({ deferred: true });
        let city = ko.observable("").extend({ deferred: true });
        let country = ko.observable("").extend({ deferred: true });
        let user = ko.observable().extend({ deferred: true });


        let createUser = () => {

            let validatePass = validatePassword();
            let validatePassRepeat = validateRepeatPassword();

            if (validatePass && validatePassRepeat) {
                ds.createUser({

                    FirstName: firstName(),
                    LastName: lastName(),
                    BirthDay: birthDay(),
                    IsStaff: isStaff(),
                    Email: email(),
                    Password: password(),
                    UserName: userName(),
                    StreetNumber: streetNumber(),
                    StreetName: streetName(),
                    ZipCode: zipCode(),
                    City: city(),
                    Country: country()

                }, function (data) {
                    console.log(data);
                    user(data);
                    $('#modalForRegister').modal('show');
                });
            } else {
                alert('Passwords should be the same');
            }
            
        }
        let goToAccount = () => {
           postman.publish('newUser', null);
        }

        let validatePassword = () => {

            if (password().length < 8) {
                return false;
            }

            return true;

        }


        let validateRepeatPassword = () => {

            console.log("validation")
            console.log(password());
            console.log(repeatPassword());

            if (password() === repeatPassword()) {
                return true;
            }



            return false;

        }

        return {
            firstName,
            lastName,
            birthDay,
            isStaff,
            email,
            password,
            repeatPassword,
            userName,
            streetNumber,
            streetName,
            zipCode,
            city,
            country,
            createUser,
            user,
            goToAccount
        }
    }
});