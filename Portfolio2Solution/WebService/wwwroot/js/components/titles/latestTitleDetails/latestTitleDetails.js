define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let latestTitle = params.latestTitle;
        let user = ko.observable();
        let rating = ko.observable();
        let numVotes = ko.observable();
        let plot = ko.observable();
        let poster = ko.observable();
        let principals = ko.observableArray();
        //TODO finish episodes
        //let episodes = ko.observable();
        let similarTitles = ko.observableArray();
        let person = ko.observable();


        postman.subscribe('userData', currentUser => {
            user(currentUser)
        });

        postman.subscribe('goToLatestTitleDetails', latestTitle => {
            let titleId = latestTitle.detailsUrl.split('/').pop();

            ds.getTitleDetails(['api/detailtitles/' + titleId, user()], function (data) {

                rating(data.rating);
                numVotes(data.numVotes);
                plot(data.plot);
                poster(data.poster);
                principals(data.principals);
                //episodes(data.episodes)
                let url = new URL(data.similarTitleUrl);
                ds.getSimilarTitles([url.pathname, user()], function (data) {
                    getTitle(data)
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
        

        let showPerson = (arg) => {

            let url = new URL(arg);
            ds.getPerson([url.pathname, user()], function (data) {

                person(data.name);
                //postman.publish('personDetails', person());

            });
        } 

        return {
            rating,
            numVotes,
            plot,
            poster,
            principals,
            latestTitle,
            similarTitles,
            showPerson
        }
    }
});