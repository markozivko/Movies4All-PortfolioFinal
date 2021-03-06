﻿define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {

        let title = ko.observable().extend({ deferred: true });
        let rating = ko.observable().extend({ deferred: true });
        let numVotes = ko.observable().extend({ deferred: true });
        let plot = ko.observable().extend({ deferred: true });
        let poster = ko.observable().extend({ deferred: true });
        let user = ko.observable().extend({ deferred: true });
        let principals = ko.observableArray().extend({ deferred: true });
        let episodes = ko.observable().extend({ deferred: true });
        let similarTitles = ko.observableArray().extend({ deferred: true });

        let goToAddNotes = (arg) => {
            postman.publish('closeModalAndgoToNotes', arg());
        }
        let goToAddRating = (arg) => {
            postman.publish('closeModalAndgoToRating', arg());
        }

        postman.subscribe('goToSimilarTitle', args => {
            ds.getTitle([args[0], args[1]], function (data) {
                title(data)
                user(args[1])
                getDetails(data.detailsUrl)

            });
        });
        postman.subscribe('goToFavoriteTitle', args => {
            ds.getTitle([args[0], args[1]], function (data) {
                title(data)
                user(args[1])
                console.log(title())
                getDetails(data.detailsUrl)

            });
        });

        postman.subscribe('goToTitle', args => {
            ds.getTitle([args[0], args[1]], function (data) {
                title(data)
                user(args[1])
                getDetails(data.detailsUrl)

            });
        });

        postman.subscribe('goToKnownForTitle', args => {
            ds.getTitle([args[0], args[1]], function (data) {
                title(data)
                user(args[1])
                console.log(title())
                getDetails(data.detailsUrl)

            });
        });
        let getDetails = (args) => {
            let url = new URL(args);
            ds.getTitleDetails([url.pathname, user()], function (data) {
                rating(data.rating);
                numVotes(data.numVotes);
                plot(data.plot);
                poster(data.poster);
                console.log(poster())
                principals(data.principals);
                let url = new URL(data.similarTitleUrl);
                ds.getSimilarTitles([url.pathname, user()], function (data) {
                    getTitle(data)
                });
                let urlEpisodes = new URL(data.episodeUrl);
                ds.getTitle([urlEpisodes.pathname, user()], function (data) {
                    // check if there are episodes or not
                    if (data.count > 0) {
                        episodes(urlEpisodes.pathname)
                    }
                    else {
                        episodes(undefined)
                    }
                });
            });
        }

        let getTitle = (args) => {
            similarTitles([]);
            args.forEach((element) => {
                let url = new URL(element.titleUrl);
                ds.getTitle([url.pathname, user()], function (data) {
                    similarTitles.push({ titleUrl: url.pathname, primaryTitle: data.primaryTitle });
                });
            });
        }
        /* **********************************
       * Section: Publication
       * ************************************/
        let goToEpisodes = () => {
            postman.publish('goToEpisodes', [episodes(), user()]);
        }
        let showPerson = (arg) => {

            let url = new URL(arg);
            postman.publish('goToPerson', [url.pathname, user()]);
            postman.publish('showPerson', [url.pathname, user()]);
        }

        let goToTitle = (arg) => {
            postman.publish('goToSimilarTitle', [arg, user()]);
        } 

        return {
            title,
            rating,
            numVotes,
            plot,
            poster,
            principals,
            similarTitles,
            showPerson,
            goToEpisodes,
            episodes,
            goToTitle,
            goToAddNotes,
            goToAddRating

        }
    }
});