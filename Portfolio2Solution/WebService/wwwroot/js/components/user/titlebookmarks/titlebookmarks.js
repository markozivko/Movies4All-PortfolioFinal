define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let titleBookmark = ko.observable().extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]);
        let bookmarkList = ko.observableArray().extend({ deferred: true });
        let episodesUrl = ko.observableArray().extend({ deferred: true });
        let personUrl = ko.observableArray().extend({ deferred: true });
        let similarTitleUrl = ko.observableArray().extend({ deferred: true });
        let knownForTitlesUrl = ko.observableArray().extend({ deferred: true });

        ds.getUser('api/users/' + currentUser().currentUser(), function (data) {
            titleBookmark(data.titleBookMarksUrl);
            let bookmarkUrl = new URL(titleBookmark());
            getData(bookmarkUrl.pathname, currentUser());
        });

        let getData = (url, id) => {
            ds.getTitleBookmarks([url, id], function (data) {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                if (data.items !== undefined) {
                    getTitle(data.items)
                }

            });
        }
        let getTitle = (args) => {
            bookmarkList([]);
            args.forEach((element) => {
                let url = new URL(element.favoriteTitleUrl);
                ds.getTitle([url.pathname, currentUser()], function (data) {
                    bookmarkList.push({ titleUrl: url.pathname, primaryTitle: data.primaryTitle, notes: element.notes });
                });
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
            getData(ds.getTitleBookmarksUrlWithPageSize(size, currentUser()), currentUser());
        });

        let goToTitle = (arg) => {
            console.log(arg)
            postman.publish('goToFavoriteTitle', [arg, currentUser()]);
        } 

        let deleteTitle = (arg) => {
            console.log(currentUser().currentUser())
            let id = arg.split("/").pop();
            ds.deleteTitleFromBookmarks(id, currentUser());
            postman.publish('goToLatest', null);
        }

        postman.subscribe('closeModalAndgoToNotes', args => {
            $('#modalForSimilarTitle').modal('hide')
            $('#modalForPerson').modal('hide');
            $('#modalForTitle').modal('hide');
            postman.publish('goToNotes', args);
        });
        postman.subscribe('goToEpisodes', args => {
            $('#modalForTitle').modal('hide');
            episodesUrl(args)
            $('#modalForEpisodes').modal('show')
        });
        postman.subscribe('goToKnownForTitle', args => {
            $('#modalForPerson').modal('hide');
            knownForTitlesUrl(args)
            $('#modalForTitle').modal('show')
        });

        postman.subscribe('goToPerson', args => {
            $('#modalForTitle').modal('hide');
            $('#modalForSimilarTitle').modal('hide')

            personUrl(args)
            $('#modalForPerson').modal('show')
        });

        postman.subscribe('goToSimilarTitle', args => {
            $('#modalForTitle').modal('hide');
            similarTitleUrl(args)
            $('#modalForSimilarTitle').modal('show')
        });
        postman.subscribe('goToTitle', args => {
            $('#modalForPerson').modal('hide');
            $('#modalForTitle').modal('show')
        });

        $('#modalForPerson').modal('hide');
        $('#modalForTitle').modal('hide');
        $('#modalForEpisodes').modal('hide');
        $('#modalForSimilarTitle').modal('hide');

        return {
            bookmarkList,
            goToTitle,
            showPrev,
            enablePrev,
            showNext,
            enableNext,
            selectedPageSize,
            pageSizes,
            currentUser,
            episodesUrl,
            personUrl,
            similarTitleUrl,
            deleteTitle
        }
    }
});