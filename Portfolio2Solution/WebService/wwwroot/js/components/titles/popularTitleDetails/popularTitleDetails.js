define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitle = params.popularTitle;
        let details = ko.observable().extend({ deferred: true });
 
 
        postman.subscribe('changePopularTitle', popularTitle => {
            let url = new URL(popularTitle.detailsUrl);
            ds.getPopularTitleDetails(url.pathname, function (data) { details(data) });
            
        });

        return {
            details,
            popularTitle
        }
    }
});