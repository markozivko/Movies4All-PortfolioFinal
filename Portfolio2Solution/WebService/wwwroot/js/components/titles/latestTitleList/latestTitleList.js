define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser())
        let latestTitles = ko.observableArray([]);
        let pageSizes = ko.observableArray();
        let selectedPageSize = ko.observableArray([10]);
        let prev = ko.observable();
        let next = ko.observable();
        let selectedLatestTitle = params.selectedLatestTitle;
        let personModal = ko.observable();

        let selectLatestTitle = latestTitle => {
            selectedLatestTitle(latestTitle);
            postman.publish('goToLatestTitleDetails', latestTitle);
        }

        let getData = (url, id) => {
            ds.getLatestTitles([url,id], data => {
                pageSizes(data.pageSizes);
                prev(data.prev);
                next(data.next || undefined);
                latestTitles(data.items);
            });
        }
        let showPrev = latestTitle => {
            console.log("prec")
            console.log(prev());
            getData(prev(), currentUser());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = latestTitle => {
            //console.log(next());
            getData(next(), currentUser());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getLatestTitlesUrlWithPageSize(size), currentUser());
        });

        let selectPerson = () => {

            //postman.subscribe('personDetails', person => {

            //    personModal(person);
            //    console.log('subscribe');
            //    console.log(personModal().name);
            //});

        }
        getData(undefined,currentUser());
        return {
            latestTitles,
            selectLatestTitle,
            selectedLatestTitle,
            pageSizes,
            selectedPageSize,
            showPrev,
            enablePrev,
            showNext,
            enableNext,
            currentUser,
            selectPerson
        };
    }
});