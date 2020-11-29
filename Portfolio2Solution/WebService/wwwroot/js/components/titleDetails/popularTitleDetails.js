define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitle = params.popularTitle;
        let details = ko.observable();
       // let id = popularTitle.detailsUrl.split("/").pop() || 1;
        //debugger;
 
        postman.subscribe('changePopularTitle', popularTitle => {
            let url = new URL(popularTitle.detailsUrl);
            //alert(url.pathname);
            console.log(popularTitle);
            ds.getPopularTitleDetails(url.pathname, function (data) { details(data) });
            
        });

        return {
            details
        }
    }
});