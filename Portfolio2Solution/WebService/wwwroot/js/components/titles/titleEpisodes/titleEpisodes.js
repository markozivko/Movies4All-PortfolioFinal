﻿define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {

        let episodes = ko.observableArray().extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]);
        let currentUser = ko.observable().extend({ deferred: true });

        let getData = (url, id) => {
            ds.getTitle([url, id], function (data) {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                episodes(data.items)
                console.log(episodes())
            });
        }

        postman.subscribe('goToEpisodes', args => {
            currentUser(args[1]);
            getData(args[0], args[1]);
        });


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


        return {
            episodes,
            showPrev,
            enablePrev,
            showNext,
            enableNext,
            selectedPageSize,
            pageSizes
        }
    }
});