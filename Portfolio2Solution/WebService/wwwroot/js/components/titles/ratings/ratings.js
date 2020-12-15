define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let currentUser = ko.observable(params.currentUser()).extend({ deferred: true });
        let selectedRating = ko.observable().extend({ deferred: true });
        let title = params.selectedLatestTitle();
        let rateTitle = el => {
            let id = title.detailsUrl.split("/").pop()
            console.log(id)
            console.log(selectedRating()[0])
            ds.rateTitle([{
                TitleId: id,
                Rating: parseInt(selectedRating()[0])
            }, id, currentUser()], function (data) {
                    console.log(data)
                    postman.publish('goToLatest', null);
            });
        }
        return {
            rateTitle,
            selectedRating
        };
    }
});