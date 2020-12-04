define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let latestTitle = params.latestTitle;
        let details = ko.observable();
        let rating = ko.observable();
        let numVotes = ko.observable();
        let plot = ko.observable();
        let poster = ko.observable();
        let principals = ko.observableArray();
        //TODO finish episodes
        //let episodes = ko.observable();
        let temp = ko.observableArray();
        let title = ko.observable();
        let similarTitles = ko.observableArray();

        postman.subscribe('goToLatestTitleDetails', latestTitle => {
            //let url = new URL(latestTitle.detailsUrl);
            let titleId = latestTitle.detailsUrl.split('/').pop();
            console.log(titleId);

            ds.getTitleDetails('api/detailtitles/' + titleId, function (data) {

                rating(data.rating);
                numVotes(data.numVotes);
                plot(data.plot);
                poster(data.poster);
                principals(data.principals);
                //episodes(data.episodes)
                let url = new URL(data.similarTitleUrl);
                ds.getSimilarTitles(url.pathname, function (data) {
                    temp(data);
                    temp().forEach((element) => {
                        console.log(element)
                        let urlNew = new URL(element.titleUrl);
                        similarTitles.removeAll();

                        ds.getTitle(urlNew.pathname, function (data) {
                            similarTitles.push(data); 
                        });
                    });
                });
            });
        });

        return {
            rating,
            numVotes,
            plot,
            poster,
            principals,
            latestTitle,
            similarTitles
        }
    }
});