﻿define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {

        let knownFor = ko.observableArray().extend({ deferred: true });
        let person = ko.observable()
        let user = ko.observable()
        console.log('Hello')
        postman.subscribe('goToPerson', args => {
            ds.getPerson([args[0], args[1]], function (data) {
                user(args[1])
                person(data)
                console.log(person())
                let url = new URL(data.knownForUrl);
                ds.getTitle([url.pathname, user()], function (data) {
                    getTitle(data.knowForList)
                });
            });
        });

        let getTitle = (args) => {
            knownFor([]);
            args.forEach((element) => {
                let url = new URL(element.titleUrl);
                ds.getTitle([url.pathname, user()], function (data) {
                    knownFor.push({ titleUrl: url.pathname, primaryTitle: data.primaryTitle });
                });
            });
        }
        return {
            person,
            knownFor
        }
    }
});