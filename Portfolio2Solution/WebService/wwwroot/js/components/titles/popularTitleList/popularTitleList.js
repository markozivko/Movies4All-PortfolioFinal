define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitles = ko.observableArray([]);
        let pageSizes = ko.observableArray();
        let selectedPageSize = ko.observableArray([10]);
        let prev = ko.observable();
        let next = ko.observable();
        let selectedPopularTitle = params.selectedPopularTitle;

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
            console.log(prev());
            getData(prev());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = popularTitle => {
            console.log(next());
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