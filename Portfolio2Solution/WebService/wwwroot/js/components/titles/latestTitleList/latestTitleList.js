﻿define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser())
        let latestTitles = ko.observableArray([]);
        let pageSizes = ko.observableArray();
        let selectedPageSize = ko.observableArray([10]);
        let prev = ko.observable();
        let next = ko.observable();
        let selectedLatestTitle = params.selectedLatestTitle;
        let episodesUrl = ko.observableArray().extend({ deferred: true });

        let selectLatestTitle = latestTitle => {
            selectedLatestTitle(latestTitle);
            postman.publish('goToLatestTitleDetails', latestTitle);
        }

        let getData = (url, id) => {
            ds.getLatestTitles([url,id], data => {
                pageSizes(data.pageSizes);
                prev(data.prev);
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

        postman.subscribe('goToEpisodes', args => {
            $('#modalDetailsTitle').modal('hide');
            episodesUrl(args)
            $('#modalEpisodesTitle').modal('show')
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
            episodesUrl
        };
    }
});