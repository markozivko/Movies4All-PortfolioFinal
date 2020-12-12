define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitles = ko.observableArray([]).extend({ deferred: true });
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([4]).extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let selectedPopularTitle = params.selectedPopularTitle;
        let error = ko.observable();

        let selectPopularTitle = popularTitle => {
            selectedPopularTitle(popularTitle);
            postman.publish('changePopularTitle', popularTitle);
        }

        let getData = url => {
            ds.getPopularTitles(url, data => {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                popularTitles(data.items);
            });
        }
        let showPrev = popularTitle => {
            getData(prev());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = popularTitle => {
            getData(next());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getPopularTitlesUrlWithPageSize(size));
        });

        getData();

        return {
            popularTitles,
            selectPopularTitle,
            selectedPopularTitle,
            pageSizes,
            selectedPageSize,
            showPrev,
            enablePrev,
            showNext,
            enableNext
        };
    }
});