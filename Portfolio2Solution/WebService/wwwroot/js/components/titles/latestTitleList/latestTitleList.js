define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
       /* **********************************
        * Section: Declarations
        * ************************************/
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true })
        let latestTitles = ko.observableArray([]).extend({ deferred: true });
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]);
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let selectedLatestTitle = params.selectedLatestTitle;
        let episodesUrl = ko.observableArray().extend({ deferred: true });
        let personUrl = ko.observableArray().extend({ deferred: true });
        let similarTitleUrl = ko.observableArray().extend({ deferred: true });
        let knownForTitlesUrl = ko.observableArray().extend({ deferred: true });


       /* **********************************
        * Section: Data Handling
        * ************************************/
        let selectLatestTitle = latestTitle => {
            selectedLatestTitle(latestTitle);
            postman.publish('goToLatestTitleDetails', latestTitle);
        }

        let getData = (url, id) => {
            ds.getLatestTitles([url,id], data => {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                latestTitles(data.items);
            });
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
            getData(ds.getLatestTitlesUrlWithPageSize(size), currentUser());
        });

        getData(undefined, currentUser());

        /* **********************************
        * Section: Subscriptions
        * ************************************/
        postman.subscribe('goToEpisodes', args => {
            $('#modalDetailsTitle').modal('hide');
            episodesUrl(args)
            $('#modalForEpisodes').modal('show')
        });


        postman.subscribe('goToPerson', args => {
            $('#modalDetailsTitle').modal('hide');
            $('#modalForTitle').modal('hide');
            personUrl(args)
            $('#modalForPerson').modal('show')
        });

        postman.subscribe('goToSimilarTitle', args => {
            $('#modalDetailsTitle').modal('hide');
            similarTitleUrl(args)
            $('#modalForTitle').modal('show')
        });

        postman.subscribe('goToTitle', args => {
            $('#modalForPerson').modal('hide');
            $('#modalForTitle').modal('show')
        });
        postman.subscribe('goToKnownForTitle', args => {
            $('#modalForPerson').modal('hide');
            knownForTitlesUrl(args)
            $('#modalForTitle').modal('show')
        });


        return {
            latestTitles,
            selectLatestTitle,
            selectedLatestTitle,
            pageSizes,
            selectedPageSize,
            showPrev,
            enablePrev,
            showNext,
            enableNext,
            currentUser,
            episodesUrl,
            personUrl,
            similarTitleUrl
        };
    }
});