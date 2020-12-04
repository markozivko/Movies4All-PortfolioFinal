define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let latestTitle = params.latestTitle;
        let details = ko.observable();
        let rating = ko.observable();
        let numVotes = ko.observable();
        let plot = ko.observable();
        let principals = ko.observableArray();

        postman.subscribe('goToLatestTitleDetails', latestTitle => {
            //let url = new URL(latestTitle.detailsUrl);
            let titleId = latestTitle.detailsUrl.split('/').pop();
            console.log(titleId);

            ds.getLatestTitleDetails('api/detailtitles/' + titleId, function (data) {

                rating(data.rating);
                numVotes(data.numVotes);
                plot(data.plot);
                principals(data.principals);

            });
        });

        return {
            rating,
            numVotes,
            plot,
            principals,
            latestTitle
        }
    }
});