define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let notes = ko.observable().extend({ deferred: true });
        let saveAsFavorite = latestTitle => {
            console.log(latestTitle());
            console.log(notes())
            let id = latestTitle().detailsUrl.split("/").pop()
            ds.createTitleBookmark([{
                TitleId: id,
                Notes: notes()
            }, id, currentUser()], function (data) {
                $('#modalForBookmark').modal('hide');
            });
        }


        return {

        };
    }
});