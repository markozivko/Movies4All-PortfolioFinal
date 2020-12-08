define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser());
        let personalities = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();
        let personalitiesList = ko.observableArray();
        let personUrl = ko.observable();

        ds.getUser('api/users/' + currentUser().currentUser(), function (data) {
            personalities(data.favoritePersonUrl);
            console.log("personaltites");
            console.log(data.favoritePersonUrl);
            console.log(personalities());

            if (personalities() !== undefined) {

                ds.getPersonalities([personalities(), currentUser()], function (data) {
                    if (data.items !== undefined) {
                        getPerson(data.items)
                    }

                });

            }
            
        });
        let getPerson = (args) => {
            personalitiesList([]);
            args.forEach((element) => {
                let url = new URL(element.favoritePersonUrl);
                ds.getTitle([url.pathname, currentUser()], function (data) {
                    personalitiesList.push({ personUrl: url.pathname, name: data.name, notes: element.notes });
                });
            });
        }

        let seePerson = (arg) => {
            console.log(arg)
            personUrl(arg);

            postman.publish('goToPerson', [arg, currentUser()]);

        }

        return {
            personalitiesList,
            seePerson,
            currentUser
        }
    }
});