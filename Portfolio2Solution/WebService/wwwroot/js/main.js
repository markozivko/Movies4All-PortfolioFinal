require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest",
        text: "lib/require-text/text.min",
        dataservice: "dataService"
    }
});

require(['knockout', 'userViewModel'], function (ko, svm) {
    console.log(svm.name)
    ko.applyBindings(svm);
});