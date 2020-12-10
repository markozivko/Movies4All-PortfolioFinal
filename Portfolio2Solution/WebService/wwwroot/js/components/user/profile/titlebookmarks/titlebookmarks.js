define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let titleBookmark = ko.observable().extend({ deferred: true });
        let prev = ko.observable().extend({ deferred: true });
        let next = ko.observable().extend({ deferred: true });
        let bookmarkList = ko.observableArray().extend({ deferred: true });
        let episodesUrl = ko.observableArray().extend({ deferred: true });
        let personUrl = ko.observableArray().extend({ deferred: true });
        let similarTitleUrl = ko.observableArray().extend({ deferred: true });

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
            postman.publish('goToFavoriteTitle', [arg, currentUser()]);
        } 

        postman.subscribe('goToEpisodes', args => {
            $('#modalForTitle').modal('hide');
            episodesUrl(args)
            $('#modalForEpisodes').modal('show')
        });


        postman.subscribe('goToPerson', args => {
            $('#modalForTitle').modal('hide');
            $('#modalForSimilarTitle').modal('hide')

            personUrl(args)
            $('#modalForPerson').modal('show')
        });

        postman.subscribe('goToSimilarTitle', args => {
            $('#modalForTitle').modal('hide');
            similarTitleUrl(args)
            $('#modalForSimilarTitle').modal('show')
        });
        postman.subscribe('goToTitle', args => {
            $('#modalForPerson').modal('hide');
            $('#modalForTitle').modal('show')
        });
        return {
            bookmarkList,
            goToTitle,
            currentUser,
            episodesUrl,
            personUrl,
            similarTitleUrl
        }
    }
});