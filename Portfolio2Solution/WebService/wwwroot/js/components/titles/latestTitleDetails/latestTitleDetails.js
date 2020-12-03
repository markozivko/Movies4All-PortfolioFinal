define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let latestTitle = params.latestTitle;
        let details = ko.observable();


        postman.subscribe('changeLatestTitle', latestTitle => {
            let url = new URL(latestTitle.detailsUrl);
            ds.getLatestTitleDetails(url.pathname, function (data) { details(data) });

        });

        return {
            details,
            latestTitle
        }
    }
});