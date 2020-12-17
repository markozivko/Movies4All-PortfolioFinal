define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let personalities = ko.observable().extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]);
        let personalitiesList = ko.observableArray().extend({ deferred: true });
        let personUrl = ko.observableArray().extend({ deferred: true })
        let episodesUrl = ko.observableArray().extend({ deferred: true });
        let knownForTitlesUrl = ko.observableArray().extend({ deferred: true });
        let similarTitleUrl = ko.observableArray().extend({ deferred: true });

        ds.getUser('api/users/' + currentUser().currentUser(), function (data) {
            personalities(data.personalitiesUrl);
            if (personalities() !== undefined) {
                getData(personalities(), currentUser());
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

        let getData = (url, id) => {
            ds.getPersonalities([url, id], function (data) {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                if (data.items !== undefined) {
                    getPerson(data.items)
                }
            });
        }
        let deletePerson = (arg) => {
            console.log(currentUser().currentUser())
            let id = arg.split("/").pop();
            ds.deletePersonFromPersonalities(id, currentUser());
            postman.publish('goToLatest', null);
        }
        let showPrev = latestTitle => {
            getData(prev(), currentUser());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = latestTitle => {
            getData(next(), currentUser());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getPersonalitiesUrlWithPageSize(size, currentUser()), currentUser());
        });

        let seePerson = (arg) => {
            personUrl(arg);
            postman.publish('goToPerson', [arg, currentUser()]);
        }
        postman.subscribe('goToKnownForTitle', args => {
            $('#modal').modal('hide');
            knownForTitlesUrl(args)
            $('#modalTitle').modal('show')
        });
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

        $('#modalForTitle').modal('hide');
        $('#modalEpisodes').modal('hide')
        $('#modalSimilarTitle').modal('hide');
        $('#modal').modal('hide');

        return {
            personalitiesList,
            seePerson,
            currentUser,
            episodesUrl,
            showPrev,
            enablePrev,
            showNext,
            enableNext,
            selectedPageSize,
            pageSizes,
            personUrl,
            similarTitleUrl,
            knownForTitlesUrl,
            deletePerson
        }
    }
});