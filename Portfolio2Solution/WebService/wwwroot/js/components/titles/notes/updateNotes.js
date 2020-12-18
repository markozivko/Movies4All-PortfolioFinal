define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let notes = ko.observable().extend({ deferred: true });
        let title = params.selectedLatestTitle();
        console.log(title)
        let saveAsFavorite = latestTitle => {
            let id = title.split("/").pop()
            console.log(id)
            ds.updateNotesForTitleBookmark([{
                TitleId: id,
                Notes: notes()
            }, currentUser()], function (data) {
                alert("updated")

            });
        }
        return {
            saveAsFavorite,
            notes
        };
    }
});