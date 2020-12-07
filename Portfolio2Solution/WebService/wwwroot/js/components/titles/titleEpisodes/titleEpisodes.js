define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = params.currentUser;
        let episodesUrl = params.episodesUrl;
        let episodes = ko.observableArray().extend({ deferred: true });
        console.log(episodesUrl)
        //ds.getTitle([episodesUrl, currentUser()], function (data) {
        //    episodes(data.items)
        //    console.log(episodes())
        //});

        postman.subscribe('goToEpisodes', args => {
            ds.getTitle([args[0], args[1]], function (data) {
                episodes(data.items)
                console.log(episodes())
            });
        });
        return {
            episodes
        }
    }
});