define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitles = ko.observableArray([]);
        let selectedPopularTitle = params.selectedpopularTitle;

        let selectPopularTitle = title => {
            selectedPopularTitle(title);
            postman.publish('changePopularTitle', title);
        }

        ds.getPopularTitles(function (data) { popularTitles(data) });

        return {
            popularTitles,
            selectPopularTitle,
            selectedPopularTitle
        }
    }
});