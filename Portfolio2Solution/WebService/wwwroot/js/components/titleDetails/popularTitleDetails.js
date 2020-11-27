define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitle = params.title;
        //let id = params.id || 1;
        //debugger;
        //ds.getCategory(id, function (data) { category(data) });

        postman.subscribe('changePopularTitle', title => {
            console.log(title);
        });

        return {
            popularTitle
        }
    }
});