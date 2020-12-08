define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
       /* **********************************
        * Section: Variables
        * ************************************/
        let latestTitle = params.latestTitle;
        let user = params.currentUser;
        let rating = ko.observable();
        let numVotes = ko.observable();
        let plot = ko.observable();
        let poster = ko.observable();
        let principals = ko.observableArray();
        let episodes = ko.observable();
        let similarTitles = ko.observableArray();

       /* **********************************
        * Section: Data Handling
        * ************************************/
        postman.subscribe('goToLatestTitleDetails', latestTitle => {
            let titleId = latestTitle.detailsUrl.split('/').pop();

            ds.getTitleDetails(['api/detailtitles/' + titleId, user()], function (data) {
                rating(data.rating);
                numVotes(data.numVotes);
                plot(data.plot);
                poster(data.poster);
                principals(data.principals);
                let url = new URL(data.similarTitleUrl);
                ds.getSimilarTitles([url.pathname, user()], function (data) {
                    getTitle(data)
                });
                
                let urlEpisodes = new URL(data.episodeUrl);
                ds.getTitle([urlEpisodes.pathname, user()], function (data) {
                    // check if there are episodes or not
                    if (data.count > 0) {
                        episodes(urlEpisodes.pathname)
                    }
                    else {
                        episodes(undefined)
                    }
                });
            });

        });

        let getTitle = (args) => {
            similarTitles([]);
            args.forEach((element) => {
                let url = new URL(element.titleUrl);
                ds.getTitle([url.pathname, user()], function (data) {
                    similarTitles.push({ titleUrl: url.pathname, primaryTitle: data.primaryTitle });
                });
            });
        }
       /* **********************************
        * Section: Publication
        * ************************************/
        let goToEpisodes = () => {
            postman.publish('goToEpisodes', [episodes(), user()]);
        }
        let showPerson = (arg) => {
            let url = new URL(arg);
            postman.publish('goToPerson', [url.pathname, user()]);
        } 
        return {
            rating,
            numVotes,
            plot,
            poster,
            principals,
            latestTitle,
            similarTitles,
            showPerson,
            goToEpisodes,
            episodes
        }
    }
});