define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser());
        let titleBookmark = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();
        let bookmarkList = ko.observableArray();

        ds.getUser('api/users/' + currentUser().currentUser(), function (data) {
            titleBookmark(data.titleBookMarksUrl);
            let bookmarkUrl = new URL(titleBookmark());
            ds.getTitleBookmarks([bookmarkUrl.pathname, currentUser()], function (data) {
                if (data.items !== undefined) {
                    getTitle(data.items)
                }
                
            });
        });
        let getTitle = (args) => {
            bookmarkList([]);
            args.forEach((element) => {
                let url = new URL(element.favoriteTitleUrl);
                ds.getTitle([url.pathname, currentUser()], function (data) {
                    bookmarkList.push({ titleUrl: url.pathname, primaryTitle: data.primaryTitle, notes: element.notes });
                });
            });
        }

        let goToTitle = (arg) => {
            console.log(arg)
            $('#modalForTitle').modal('show')
            //postman.publish('goToTitle', [arg, currentUser()]);
        } 
        return {
            bookmarkList,
            goToTitle
        }
    }
});