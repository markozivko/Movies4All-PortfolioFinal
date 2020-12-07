define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {

        let episodes = ko.observableArray().extend({ deferred: true });
      

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