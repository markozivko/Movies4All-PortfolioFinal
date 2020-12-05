define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser())
        let latestTitles = ko.observableArray([]);
        let pageSizes = ko.observableArray();
        let selectedPageSize = ko.observableArray([10]);
        let prev = ko.observable();
        let next = ko.observable();
        let selectedLatestTitle = params.selectedLatestTitle;
       // let userId = currentUser().split("/").pop();
        let selectLatestTitle = latestTitle => {
            selectedLatestTitle(latestTitle);
            postman.publish('goToLatestTitleDetails', latestTitle);
        }

        let getData = url => {
            ds.getLatestTitles(url, data => {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                latestTitles(data.items);
            });
        }
        let showPrev = latestTitle => {
            console.log(prev());
            getData(prev());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = latestTitle => {
            console.log(next());
            getData(next());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getLatestTitlesUrlWithPageSize(size));
        });

        getData();

        return {
            latestTitles,
            selectLatestTitle,
            selectedLatestTitle,
            pageSizes,
            selectedPageSize,
            showPrev,
            enablePrev,
            showNext,
            enableNext
        };
    }
});