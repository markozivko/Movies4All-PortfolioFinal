define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let searchHistory = ko.observable().extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]);
        let searchList = ko.observableArray().extend({ deferred: true });

        ds.getUser('api/users/' + currentUser().currentUser(), function (data) {
            searchHistory(data.searchHistoryUrl);
            let searchHistoryUrl = new URL(searchHistory());
            getData(searchHistoryUrl.pathname, currentUser());
        });

        let getData = (url, id) => {
            ds.getSearchHistory([url, id], function (data) {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                if (data.items !== undefined) {
                    getHistory(data.items)
                }

            });
        }
        let getHistory = (args) => {
            searchList([]);
            args.forEach((element) => {
                searchList.push({ word: element.word, date: element.date.split('T').shift() + ' ' + element.date.split('T').pop()});
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
            getData(ds.getSearchHistoryUrlWithPageSize(size, currentUser()), currentUser());
        });


        return {
            searchList,
            showPrev,
            enablePrev,
            showNext,
            enableNext,
            selectedPageSize,
            pageSizes,
            currentUser

        }
    }
});