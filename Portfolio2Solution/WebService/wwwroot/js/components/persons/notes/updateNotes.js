define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let notes = ko.observable().extend({ deferred: true });
        let personality = params.selectedPerson();
        console.log(personality)
        let saveAsFavorite = latestTitle => {
            let id = personality.split("/").pop()
            console.log(id)
            ds.updateNotesForPersonalities([{
                name: id,
                Notes: notes()
            }, currentUser()], function (data) {
                    postman.publish('goToLatest', null);

            });
        }
        return {
            saveAsFavorite,
            notes
        };
    }
});