define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {

        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let pageSizes = ko.observableArray().extend({ deferred: true });
        let selectedPageSize = ko.observableArray([10]);
        let userList = ko.observableArray().extend({ deferred: true });


        let getData = (url, id) => {
            ds.getUsers([url, id], function (data) {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                userList(data.items);

            });
        }
        let unsubscribeCustomer = (id) => {
            let customerId = id.split("/").pop()
            console.log(customerId)
            ds.unsubscribeCustomer(customerId, currentUser());
        }
        let showPrev = user => {
            getData(prev(), currentUser());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = user => {
            getData(next(), currentUser());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getAllUsersUrlWithPageSize(size), currentUser());
        });

        getData(undefined, currentUser());

        return {
            currentUser,
            prev,
            next,
            pageSizes,
            selectedPageSize,
            userList,
            showPrev,
            showNext,
            enableNext,
            enablePrev,
            unsubscribeCustomer

        }
    }
});