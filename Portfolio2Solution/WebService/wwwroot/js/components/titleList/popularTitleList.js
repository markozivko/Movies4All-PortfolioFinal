define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitles = ko.observableArray([]);
        //let nextPage = ko.observable();
        let selectedPopularTitle = params.selectedPopularTitle;

        let selectPopularTitle = popularTitle => {
            selectedPopularTitle(popularTitle);
            postman.publish('changePopularTitle', popularTitle);
        }

        ds.getPopularTitles(function (data) { popularTitles(data) });

        return {
            popularTitles,
            selectPopularTitle,
            selectedPopularTitle
        }
    }
});