define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitle = params.popularTitle;
        //let id = params.id || 1;
        //debugger;
        //ds.getCategory(id, function (data) { category(data) });

        postman.subscribe('changePopularTitle', popularTitle => {
            console.log(popularTitle);
        });

        return {
            popularTitle
        }
    }
});