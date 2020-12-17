define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser());
        let search = ko.observable().extend({ deferred: true });

        let titlesList = ko.observableArray().extend({ deferred: true });
        let id = ko.observable().extend({ deferred: true })
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]).extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let episodesUrl = ko.observableArray().extend({ deferred: true });
        let personUrl = ko.observableArray().extend({ deferred: true });
        let knownForTitlesUrl = ko.observableArray().extend({ deferred: true });


        let showResult = arg => {
            console.log('hello')
            console.log(search())
            getData(undefined, search(), currentUser());
        }

        let selectTitle = title => {
            let url = new URL(title)
            postman.publish('goToTitle', [url.pathname, currentUser()]);
        }

        let getData = (url, search, user) => {
            ds.simpleSearch([url, search, user], data => {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                titlesList(data.items);
                console.log(titlesList())
            });
        }
        let showPrev = title => {
            getData(prev(), search(), currentUser());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = title => {
            getData(next(), search(), currentUser());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.simpleSearchUrlWithPageSize(size, search()), search(), currentUser());
        });

        let goToAddNotes = (el) => {
            console.log(el)
            let url = new URL(el);
            ds.getTitle([url.pathname, currentUser()], function (data) {
                $('#modal').modal('hide');
                $('#modalForPerson').modal('hide');
                $('#modalForEpisodes').modal('hide');
                postman.publish('goToNotes', data);
            });

        }
        /* **********************************
        * Section: Subscriptions
        * ************************************/
        postman.subscribe('goToEpisodes', args => {
            $('#modal').modal('hide');
            episodesUrl(args)
            $('#modalForEpisodes').modal('show')
        });


        postman.subscribe('goToPerson', args => {
            $('#modal').modal('hide');
            personUrl(args)
            $('#modalForPerson').modal('show')
        });

        postman.subscribe('goToKnownForTitle', args => {
            $('#modalForPerson').modal('hide');
            knownForTitlesUrl(args)
            $('#modal').modal('show')
        });

        $('#modal').modal('hide');
        $('#modalForPerson').modal('hide');
        $('#modalForEpisodes').modal('hide');

        postman.subscribe('closeModalAndgoToRating', args => {
            $('#modal').modal('hide');
            $('#modalForPerson').modal('hide');
            $('#modalForEpisodes').modal('hide');
        });


        return {
            currentUser,
            search,
            showResult,
            titlesList,
            pageSizes,
            selectedPageSize,
            selectTitle,
            showPrev,
            enablePrev,
            showNext,
            enableNext,
            selectedPageSize,
            pageSizes,
            episodesUrl,
            personUrl,
            goToAddNotes
        }
    }
});