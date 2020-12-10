define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let rating = ko.observable().extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]);
        let ratingsList = ko.observableArray().extend({ deferred: true });

        ds.getUser('api/users/' + currentUser().currentUser(), function (data) {
            rating(data.userRatingsUrl);
            let ratingUrl = new URL(rating());
            getData(ratingUrl.pathname, currentUser());
        });

        let getData = (url, id) => {
            ds.getRatings([url, id], function (data) {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                if (data.items !== undefined) {
                    getRating(data.items)
                }

            });
        }
        let getRating = (args) => {
            ratingsList([]);
            args.forEach((element) => {
                ratingsList.push({ title: element.title, date: element.date, rating: element.rating, description: element.ratingDescription });
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
            getData(ds.getRatingsUrlWithPageSize(size, currentUser()), currentUser());
        });


        return {
            ratingsList,
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