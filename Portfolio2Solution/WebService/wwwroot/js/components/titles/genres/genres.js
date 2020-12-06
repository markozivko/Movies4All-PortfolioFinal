define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser())
        let genres = ko.observableArray();

        ds.getGenres(currentUser(), data => {

            genres(data);

        });

        return {
            genres
        };
    }
});