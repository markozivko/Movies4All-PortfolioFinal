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

    ko.components.register("home",
        {
            viewModel: { require: "components/titleList/popularTitleList" },
            template: { require: "text!components/titleList/popularTitleList.html" }
        });
    ko.components.register("title-details",
        {
            viewModel: { require: "components/titleDetails/popularTitleDetails" },
            template: { require: "text!components/titleDetails/popularTitleDetails.html" }
        });
    ko.components.register("login",
        {
            viewModel: { require: "components/login/login" },
            template: { require: "text!components/login/login.html" }

        });
    ko.components.register("register",
        {
            viewModel: { require: "components/register/register" },
            template: { require: "text!components/register/register.html" }
        });

    ko.components.register("about",
        {
            viewModel: { require: "components/aboutUs/aboutUs" },
            template: { require: "text!components/aboutUs/aboutUs.html" }
        });
});

require(['knockout', 'viewModel'], (ko, vm) => {
    ko.applyBindings(vm);
});