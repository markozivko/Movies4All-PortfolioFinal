define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser());
        let personalities = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();
        let personalitiesList = ko.observableArray();
        let personUrl = ko.observableArray().extend({ deferred: true })
        let episodesUrl = ko.observableArray().extend({ deferred: true });
        let knownForTitlesUrl = ko.observableArray().extend({ deferred: true });
        let similarTitleUrl = ko.observableArray().extend({ deferred: true });

        ds.getUser('api/users/' + currentUser().currentUser(), function (data) {
            personalities(data.personalitiesUrl);

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
            personUrl(arg);
            postman.publish('goToPerson', [arg, currentUser()]);
        }
        postman.subscribe('goToTitle', args => {
            $('#modal').modal('hide');
            knownForTitlesUrl(args)
            $('#modalTitle').modal('show')
        });
        postman.subscribe('goToEpisodes', args => {
            $('#modalTitle').modal('hide');
            episodesUrl(args)
            $('#modalEpisodes').modal('show')
        });

        postman.subscribe('goToSimilarTitle', args => {
            $('#modalTitle').modal('hide');
            similarTitleUrl(args)
            $('#modalSimilarTitle').modal('show')
        });

        postman.subscribe('showPerson', args => {
            $('#modalTitle').modal('hide');
            $('#modalSimilarTitle').modal('hide');
            personUrl(args[0])
            $('#modal').modal('show')
            seePerson(args[0])
        });
        return {
            personalitiesList,
            seePerson,
            currentUser,
            episodesUrl,
            personUrl,
            similarTitleUrl,
            knownForTitlesUrl
        }
    }
});