require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest",
        text: "lib/require-text/text.min",
        dataservice: "services/dataService",
        postman: "services/postman"
    }
});

require(['knockout', 'text'], (ko) => {

    ko.components.register("my", {

        viewModel: { require: "components/my/my" },
        template: { require: "text!components/my/my.html" }

    });

    ko.components.register("title-list",
        {
            viewModel: { require: "components/titleList/popularTitleList" },
            template: { require: "text!components/titleList/popularTitleList.html" }
        });

});

//require(['knockout', 'text'], (ko) => {
//    ko.components.register("my", {
//        viewModel: { require: "components/my/my" },
//        template: { require: "text!components/my/my.html" }
//    });

//    ko.components.register("title-list",
//        {
//            viewModel: { require: "components/titleList/popularTitleList" },
//            template: { require: "text!components/titleList/popularTitleList.html" }
//        });

//    ko.components.register("title-details",
//        {
//            viewModel: { require: "components/titleDetails/popularTitleDetails" },
//            template: { require: "text!components/titleDetails/popularTitleDetails.html" }
//        });
//});

//require(['knockout', 'viewModel'], (ko, svm) => {
//    ko.applyBindings(svm);
//});