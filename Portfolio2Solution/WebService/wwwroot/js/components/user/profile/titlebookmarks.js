define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser().currentUser());
        let titleBookmark = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();
        let bookmarkList = ko.observableArray();

        let url = new URL(currentUser());
        ds.getUser(url.pathname, function (data) {
            titleBookmark(data.titleBookMarksUrl);

            //TODO: check how to get the title and notes to display
            let newUrl = new URL(titleBookmark());
            ds.getTitleBookmarks(newUrl.pathname, function (data) {
                data.items.forEach((element) => {
                    let urlNew = new URL(element.favoriteTitleUrl);
                    if (bookmarkList().length > 0) {
                        bookmarkList.removeAll();
                    }

                    ds.getTitle(urlNew.pathname, function (data) {

                        console.log(data.primaryTitle);

                    });

                });
                bookmarkList(data.items);
            });
        });

        return {
            bookmarkList
        }
    }
});