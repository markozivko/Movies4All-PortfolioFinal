define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let notes = ko.observable().extend({ deferred: true });
        let personality = params.selectedPerson();
        console.log("personality id;")
        console.log(personality)
        let saveAsFavorite = () => {
            let id = personality.knownForUrl.split("/").pop()
            console.log(id)
            ds.createFavoritePerson([{
                name: id,
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