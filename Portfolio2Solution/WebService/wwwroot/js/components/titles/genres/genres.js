define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser())
        let genres = ko.observableArray().extend({ deferred: true });
        let selectedGenre = ko.observable().extend({ deferred: true });
        let titlesList = ko.observableArray().extend({ deferred: true });
        let id = ko.observable().extend({ deferred: true })
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]).extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let episodesUrl = ko.observableArray().extend({ deferred: true });
        let personUrl = ko.observableArray().extend({ deferred: true });
        let knownForTitlesUrl = ko.observableArray().extend({ deferred: true });

        ds.getGenres(currentUser(), data => {
            genres(data);
        });

        let showTitleFromThisCategory = (url, name) => {
            selectedGenre(name);
            id(url.split('=').pop())
            getData(undefined, id(), currentUser());
        }

        let selectTitle = title => {
            let url = new URL(title)
            postman.publish('goToTitle', [url.pathname, currentUser()]);
        }

        let getData = (url, id, user) => {
            ds.getTitlesByCategory([url, id, user], data => {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                titlesList(data.items);
            });
        }
        let showPrev = title => {
            getData(prev(), id(), currentUser());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = title => {
            getData(next(), id(), currentUser());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getTitlesUrlWithPageSize(size, id()), id(), currentUser());
        });

        
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

        return {
            genres,
            showTitleFromThisCategory,
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
            currentUser,
            episodesUrl,
            personUrl,
            selectedGenre


            
        };
    }
});