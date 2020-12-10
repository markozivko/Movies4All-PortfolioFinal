define([], () => {


    /* ****************************************************************************************************************
    *                                         Functionalitites for titles
    * ****************************************************************************************************************/

    /* **********************************
    * Function: Get popular titles
    * ************************************/
    const popularTitleApiUrl = 'api/popular';
    let getPopularTitles = function (uri, callback) {
        if (uri === undefined) {
            uri = popularTitleApiUrl;

        }

        fetch(uri, {
            method: 'GET'

        })
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                callback(data);
            });
    }
    let getPopularTitlesUrlWithPageSize = size => popularTitleApiUrl + "?pageSize=" + size;


    /* **********************************
    * Function: Get popular title by id
    * ************************************/
    let getPopularTitle = (id, callback) => {
        fetch('api/popular/' + id)
            .then(response => response.json())
            .then(callback);
    }

    let getPopularTitleDetails = (uri, callback) => {
        fetch(uri)
            .then(response => response.json())
            .then(callback);
    }

    /* **********************************
    * Function: Get latest titles
    * ************************************/
    const latestTitleApiUrl = 'api/titles/latest';
    let getLatestTitles = function ([uri, user], callback) {
        if (uri === undefined) {
            uri = latestTitleApiUrl;
        }
        fetch(uri, {
            headers: {

                'Authorization': parseInt(user.currentUser())
            },
            method: 'GET'

        })
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                callback(data);
            });
    }
    let getLatestTitlesUrlWithPageSize = size => latestTitleApiUrl + "?pageSize=" + size;

    /* **********************************
    * Function: Get latest title by id
    * ************************************/

    let getLatestTitle = ([id, user], callback) => {
        fetch('api/titles/latest/' + id, {
            headers: {
                'Authorization': parseInt(user.currentUser())
            },
            method: 'GET'

        })
            .then(response => response.json())
            .then(callback);
    }
    /* **********************************
    * Function: Get title by id
    * ************************************/
    let getTitle = ([uri, user], callback) => {
        if (uri === undefined) {
            console.log('undefined')
        }
        else {
            fetch(uri, {
                headers: {
                    'Authorization': parseInt(user.currentUser())
                },
                method: 'GET'

            })
                .then(response => response.json())
                .then(callback);
        }

    }
    /* **********************************
    * Function: Get all genres
    * ************************************/
    let getGenres = (user, callback) => {

        fetch("api/genres", {
            headers: {
                'Authorization': parseInt(user.currentUser())
            },
            method: 'GET'

        })
            .then(response => response.json())
            .then(callback);
    }
    /* **********************************
    * Function: Get titleDetails
    * ************************************/
    let getTitleDetails = ([uri, user], callback) => {
        fetch(uri, {
            headers: {
                'Authorization': parseInt(user.currentUser())
            }
        })
            .then(response => response.json())
            .then(callback);
    }
    /* **********************************
    * Function: Get similarTitles
    * ************************************/
    let getSimilarTitles = ([uri, user], callback) => {
        fetch(uri, {
            headers: {
                'Authorization': parseInt(user.currentUser())
            },
            method: 'GET'

        })
            .then(response => response.json())
            .then(callback);
    }
    /* **********************************
 * Function: Get titles by category
 * ************************************/
    const titleApiUrl = 'api/titles/category/';
    let getTitlesByCategory = function ([uri, id, user], callback) {
        if (uri === undefined) {
            uri = titleApiUrl + id;
        }
        fetch(uri, {
            headers: {

                'Authorization': parseInt(user.currentUser())
            },
            method: 'GET'

        })
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                callback(data);
            });
    }
    let getTitlesUrlWithPageSize = (size, id) => titleApiUrl + id + "?pageSize=" + size;

    /* ****************************************************************************************************************
     *                                         Functionalitites for users
     * ****************************************************************************************************************/
    /* **********************************
    * Function: Login
    * ************************************/

    let login = (email, password, callback) => {

        fetch('api/login?email=' + email + '&password=' + password)
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                callback(data);
            });
    }

    /* **********************************
    * Function: Get specific user
    * ************************************/
    let getUser = (uri, callback) => {
        console.log(uri)
        fetch(uri, {
            headers: {
                'Authorization': uri.split('/').pop()
            }
        })
            .then(response => response.json())
            .then(callback);
    }
    /* **********************************
    * Function: Get titleBookmarks
    * ************************************/
    const titleBookmarksApiUrl = 'api/titlebookmarks/';
    let getTitleBookmarks = ([uri, user], callback) => {
        if (uri === undefined) {
            uri = titleBookmarksApiUrl + user.currentUser();
        }
        fetch(uri, {
            headers: {
                'Authorization': parseInt(user.currentUser())
            }
        })
            .then(response => response.json())
            .then(callback);
    }
    let getTitleBookmarksUrlWithPageSize = (size, id) => titleBookmarksApiUrl + id + "?pageSize=" + size;
    /* **********************************
    * Function: Get personalities
    * ************************************/
    const personalitiesApiUrl = 'api/personalities/';
    let getPersonalities = ([uri, user], callback) => {
        if (uri === undefined) {
            uri = personalitiesApiUrl + user.currentUser();
        }
        fetch(uri, {
            headers: {
                'Authorization': parseInt(user.currentUser())
            }
        })
            .then(response => response.json())
            .then(callback);
    }
    let getPersonalitiesUrlWithPageSize = (size, id) => personalitiesApiUrl + id + "?pageSize=" + size;

    /* **********************************
     * Function: Get user search history
     * **********************************/
    const searchHistoryApiUrl = 'api/searchHistory/';
    let getSearchHistory = ([uri, user], callback) => {
        if (uri === undefined) {
            console.log('undefined')
        }
        else {
            fetch(uri, {
                headers: {
                    'Authorization': parseInt(user.currentUser())
                }
            })
                .then(response => response.json())
                .then(callback);
        }

    }
    let getSearchHistoryUrlWithPageSize = (size, id) => searchHistoryApiUrl + id + "?pageSize=" + size;

    /* **********************************
 * Function: Get user search history
 * **********************************/
    const ratingsApiUrl = 'api/ratings/';
    let getRatings = ([uri, user], callback) => {
        if (uri === undefined) {
            console.log('undefined')
        }
        else {
            fetch(uri, {
                headers: {
                    'Authorization': parseInt(user.currentUser())
                }
            })
                .then(response => response.json())
                .then(callback);
        }

    }
    let getRatingsUrlWithPageSize = (size, id) => ratingsApiUrl + id + "?pageSize=" + size;

    /* **********************************
    * Function: Get users
    * ************************************/

    //let getUsers = function (callback) {
    //    fetch("api/users", {
    //        headers: {
    //            'Authorization': 3,
    //        }

    //    })
    //        .then(function (response) {
    //            return response.json();
    //        })
    //        .then(function (data) {
    //            callback(data);
    //        });
    //}

    //getUsers(function (data) {
    //    console.log(data);
    //});

    /* **********************************
    * Function: Get specific user
    * ************************************/
    //let getUser = (id, callback) => {
    //    fetch("api/users/" + id)
    //        .then(response => response.json())
    //        .then(callback);
    //}

    //return {
    //    getUsers,
    //    getUser
    //}

    //});



    /* **********************************
    * Function: Create new user
    * ************************************/
    //let createUser = function (user, callback) {
    //    fetch("api", {
    //        method: "POST", body: JSON.stringify(user), headers: {
    //            'Content-Type': 'application/json'

    //        }
    //    })
    //        .then(response => response.json())
    //        .then(data => callback(data));
    //}

    //createUser({
    //    FirstName: "A-gun-evil-twin",
    //    LastName: "Aabidah2",
    //    BirthDay: "01/08/2009",
    //    IsStaff: false,
    //    Email: "gun@super.com",
    //    Password: "gun123",
    //    UserName: "shootGub2009",
    //    StreetNumber: "21",
    //    StreetName: "Joyful",
    //    ZipCode: "35214",
    //    City: "Mumbai",
    //    Country: "India"
    //}, function (data) {
    //        console.log(data);
    //});

    /* **********************************
    * Function: Update the user
    * ************************************/

    //let updateUser = function (user, callback) {
    //    fetch("api/users/49", {
    //        method: "PUT",
    //        body: JSON.stringify(user),
    //        headers: {
    //                'Content-Type': 'application/json',
    //                'Authorization': 49
    //        }
    //    })
    //        .then(response => response.json())
    //        .then(data => callback(data));
    //}

    //updateUser({
    //    FirstName: "A-gun-new-name",
    //    LastName: "Aabidah2",
    //    BirthDay: "01/08/2009",
    //    IsStaff: false,
    //    Email: "gun@super.com",
    //    Password: "gun123",
    //    UserName: "shootGub-newName",
    //    StreetNumber: "21",
    //    StreetName: "Joyful",
    //    ZipCode: "35214",
    //    City: "Mumbai",
    //    Country: "India"
    //}, function (data) {
    //        console.log(data);
    //});

    /* **********************************
    * Function: Delete user
    * ************************************/
    //let unsubscribeUser = url => fetch(url, {
    //    method: "DELETE", headers: {
    //        'Authorization': 50
    //    }
    //});

    //unsubscribeUser("api/users/50")

    /* ****************************************************************************************************************
    *                                         Functionalitites for person
    * ****************************************************************************************************************/
    /* **********************************
    * Function: get actor or 
    * production team member
    * ************************************/

    let getPerson = ([uri, user], callback) => {
        fetch(uri, {
            headers: {
                'Authorization': parseInt(user.currentUser())
            },
            method: 'GET'

        })
            .then(response => response.json())
            .then(callback);
    }


    return {
        getGenres,
        getTitleBookmarks,
        getTitle,
        getTitleDetails,
        getSimilarTitles,
        getPopularTitles,
        getPopularTitle,
        getPopularTitlesUrlWithPageSize,
        getPopularTitleDetails,
        getLatestTitles,
        getLatestTitle,
        getLatestTitlesUrlWithPageSize,
        login,
        getUser,
        getPerson,
        getPersonalities,
        getTitlesByCategory,
        getTitlesUrlWithPageSize,
        getTitleBookmarksUrlWithPageSize,
        getPersonalitiesUrlWithPageSize,
        getSearchHistoryUrlWithPageSize,
        getSearchHistory,
        getRatingsUrlWithPageSize,
        getRatings
    }

});


