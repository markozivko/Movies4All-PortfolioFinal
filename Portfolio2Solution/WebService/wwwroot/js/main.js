﻿require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest",
        text: "lib/require-text/text.min",
        dataservice: "services/dataService",
        postman: "services/postman"
    }
});

require(['knockout', 'text'], (ko) => {

    ko.components.register("home",
        {
            viewModel: { require: "components/titles/popularTitleList/popularTitleList" },
            template: { require: "text!components/titles/popularTitleList/popularTitleList.html" }
        });
    ko.components.register("popular-title-details",
        {
            viewModel: { require: "components/titles/popularTitleDetails/popularTitleDetails" },
            template: { require: "text!components/titles/popularTitleDetails/popularTitleDetails.html" }
        });

    ko.components.register("latest",
        {
            viewModel: { require: "components/titles/latestTitleList/latestTitleList" },
            template: { require: "text!components/titles/latestTitleList/latestTitleList.html" }
        });

    ko.components.register("latest-title-details",
        {
            viewModel: { require: "components/titles/latestTitleDetails/latestTitleDetails" },
            template: { require: "text!components/titles/latestTitleDetails/latestTitleDetails.html" }
        });

    ko.components.register("genres",
        {
            viewModel: { require: "components/titles/genres/genres" },
            template: { require: "text!components/titles/genres/genres.html" }
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

    ko.components.register("profile",
        {
            viewModel: { require: "components/user/profile/userProfile/userProfile" },
            template: { require: "text!components/user/profile/userProfile/userProfile.html" }
        });

    ko.components.register("account", {

        viewModel: { require: "components/navbar/account" },
        template: { require: "text!components/navbar/account.html" }

    });

    ko.components.register("navbar",
        {
            viewModel: { require: "components/navbar/navbar" },
            template: { require: "text!components/navbar/navbar.html" }
        });

    ko.components.register("location",
        {
            viewModel: { require: "components/user/profile/location/location" },
            template: { require: "text!components/user/profile/location/location.html" }
        });

    ko.components.register("my profile",
        {
            viewModel: { require: "components/user/profile/profile/profile" },
            template: { require: "text!components/user/profile/profile/profile.html" }
        });

    ko.components.register("title bookmarks",
        {
            viewModel: { require: "components/user/profile/titlebookmarks/titlebookmarks" },
            template: { require: "text!components/user/profile/titlebookmarks/titlebookmarks.html" }
        });

    ko.components.register("personalities",
        {
            viewModel: { require: "components/user/profile/personalities/personalities" },
            template: { require: "text!components/user/profile/personalities/personalities.html" }
        });


    ko.components.register("logout",
        {
            viewModel: { require: "components/user/profile/logout/logout" },
            template: { require: "text!components/user/profile/logout/logout.html" }
        });

    ko.components.register("search history",
        {
            viewModel: { require: "components/user/profile/searchHistory/searchHistory" },
            template: { require: "text!components/user/profile/searchHistory/searchHistory.html" }
        });
    ko.components.register("title-episodes",
        {
            viewModel: { require: "components/titles/titleEpisodes/titleEpisodes" },
            template: { require: "text!components/titles/titleEpisodes/titleEpisodes.html" }
        });
    ko.components.register("person",
        {
            viewModel: { require: "components/persons/person" },
            template: { require: "text!components/persons/person.html" }
        });
});

require(['knockout', 'viewModel'], (ko, vm) => {
    ko.applyBindings(vm);
});