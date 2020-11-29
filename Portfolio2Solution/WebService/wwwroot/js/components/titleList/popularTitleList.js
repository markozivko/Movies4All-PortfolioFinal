define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let popularTitles = ko.observableArray([]);
        //let nextPage = ko.observable();
        let previousPage = ko.observable();
        let selectedPopularTitle = params.selectedPopularTitle;

        let selectPopularTitle = popularTitle => {
            selectedPopularTitle(popularTitle);
            postman.publish('changePopularTitle', popularTitle);
        }

        //let selectNextPage = nextPage => {
        //    ds.getPopularTitles('api/popular', function (items, next, prev) { nextPage(next) });
        //    postman.publish('changeNextPage', nextPage);
        //}

        //postman.subscribe('changeNextPage', nextPageUrl => {
        //    let url = new URL(nextPageUrl);
        //    console.log(popularTitle);
        //    ds.getPopularTitles(url.pathname, function (items) { popularTitles(items) });

        //});

        ds.getPopularTitles('api/popular', function (items, next, prev) {nextPage(next) });
        ds.getPopularTitles('api/popular', function (items) { popularTitles(items) });

        return {
            popularTitles,
          // nextPage,
           // selectNextPage,
            selectPopularTitle,
            selectedPopularTitle
        }
    }
});