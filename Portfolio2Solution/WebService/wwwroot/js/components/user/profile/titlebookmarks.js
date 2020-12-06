define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser());
        let titleBookmark = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();
        let bookmarkList = ko.observableArray();

        ds.getUser('api/users/' + currentUser().currentUser(), function (data) {
            titleBookmark(data.titleBookMarksUrl);

            //TODO: check how to get the title and notes to display
            let bookmarkUrl = new URL(titleBookmark());
            ds.getTitleBookmarks([bookmarkUrl.pathname, currentUser()], function (data) {
                if (data.items !== undefined) {
                    data.items.forEach((element) => {
                        let titleUrl = new URL(element.favoriteTitleUrl);
                        if (bookmarkList().length > 0) {
                            bookmarkList.removeAll();
                        }

                        ds.getTitle([titleUrl.pathname, currentUser()], function (data) {

                            console.log(data.primaryTitle);

                        });

                    });
                    bookmarkList(data.items);
                }
                
            });
        });

        return {
            bookmarkList
        }
    }
});