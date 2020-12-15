define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let selectedComponent = ko.observable('latest').extend({ deferred: true });
        let selectedLatestTitle = ko.observable().extend({ deferred: true });
        let selectedPerson = ko.observable().extend({ deffered: true });
        let currentUser = ko.observable(params).extend({ deferred: true });
        let role = ko.observable().extend({ deferred: true });
        let menuElements = ko.observableArray(["Latest", "Genres", "Browse", "Profile"]);
        let currentParams = ko.observable({ selectedLatestTitle, selectedPerson, currentUser }).extend({ deferred: true });
        ds.getUser('/api/users/' + currentUser().currentUser(), data => {
            role(data.role)
            console.log(role())
            if (role() === 'staff') {
                menuElements.push('Management');
            }
        });
        
        let changeContent = element => {
            console.log(element);
            selectedComponent(element.toLowerCase());
        }
        let isActive = element => {
            element.toLowerCase() === selectedComponent() ? "active" : "";
        }

        let goToHome = element => {
            selectedComponent('latest');
        }

        postman.subscribe('goToNotes', args => {
            selectedComponent('notes');
            selectedLatestTitle(args)
        });

        postman.subscribe('goToRating', args => {
            selectedComponent('ratings');
            selectedLatestTitle(args)
        });

        postman.subscribe('goToPNotes', args => {
            selectedComponent('pNotes');
            selectedPerson(args)
        });

        postman.subscribe('goToLatest', args => {
            selectedComponent('latest');
        });

        
       
        return {
            selectedComponent,
            currentParams,
            menuElements,
            changeContent,
            isActive,
            goToHome

        };
    }
});