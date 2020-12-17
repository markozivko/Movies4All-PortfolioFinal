define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let notes = ko.observable().extend({ deferred: true });
        let title = params.selectedLatestTitle();
        let saveAsFavorite = latestTitle => {
            let id = title.detailsUrl.split("/").pop()
            console.log(id)
            ds.updateNotesForTitleBookmark([{
                TitleId: id,
                Notes: notes()
            }, id, currentUser()], function (data) {
                console.log(data)
                postman.publish('goToLatest', null);

            });
        }
        return {
            saveAsFavorite,
            notes
        };
    }
});