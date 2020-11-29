define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitle = params.popularTitle;
        let details = ko.observable('');
        let id = popularTitle.detailsUrl.split("/").pop() || 1;
        debugger;
        ds.getPopularTitleDetails(id, function (data) { details(data) });
        
        //postman.subscribe('changePopularTitle', popularTitle => {
        //    console.log(popularTitle);
        //});

        return {
            details
        }
    }
});