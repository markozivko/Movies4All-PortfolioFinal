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
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('No popular titles');
                }
            })
            .then(function (data) {
                callback(data);
            })
            .catch((error) => {
                console.log(error);
            });
    }
    let getPopularTitlesUrlWithPageSize = size => popularTitleApiUrl + "?pageSize=" + size;


    /* **********************************
    * Function: Get popular title details
    * ************************************/
    let getPopularTitleDetails = (uri, callback) => {
        fetch(uri)
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Details for movie not found');
                }
            })
            .then(function (data) {
                callback(data);
            })
            .catch((error) => {
                console.log(error);
            });
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else if (response.status === '404') {
                    throw new Error('Come back in few minutes');
                }
                else {
                    throw new Error('something went wrong');
                }
            })
            .then(function (data) {
                callback(data);
            })
            .catch((error) => {
                console.log(error)
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else if (response.status === '404') {
                    throw new Error('Title not found');
                }
                else {
                    throw new Error('something went wrong');
                }
            })
            .then(callback)
            .catch((error) => {
                console.log(error)
            });
    }
    /* **********************************
    * Function: Get title / similar title, episodes
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
                .then(response => {

                    if (response.ok) {
                        return response.json();
                    } else if (response.status === '404') {
                        throw new Error('Title not found');
                    }
                    else {
                        throw new Error('something went wrong');
                    }
                })
                .then(callback)
                .catch((error) => {
                    console.log(error)
                });
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('something went wrong');
                }
            })
            .then(callback)
            .catch((error) => {
                console.log(error)
            });
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                }
                else if (response.status === '404') {
                    throw new Error('Title not found');
                }
                else {
                    throw new Error('something went wrong');
                }
            })
            .then(callback)
            .catch((error) => {
                console.log(error)
            });
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                }
                else if (response.status === '404') {
                    throw new Error('Title not found');
                }
                else {
                    throw new Error('something went wrong');
                }
            })
            .then(callback)
            .catch((error) => {
                console.log(error)
            });
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                }
                else if (response.status === '404') {
                    throw new Error('Title not found');
                }
                else {
                    throw new Error('something went wrong');
                }
            })
            .then(function (data) {
                callback(data);
            })
            .catch((error) => {
                console.log(error)
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('User not found');
                }
            })
            .then(function (data) {
                callback(data);
            })
            .catch((error) => {
                console.log(error)
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else if (response.status === '404') {
                    throw new Error('User not found');
                } else {
                    throw new Error('Something went wrong.');
                }
            })
            .then(callback)
            .catch((error) => {
                console.log(error)
            });
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                }
                else if (response.status === '404') {
                    throw new Error('Title not found');
                }
                else {
                    throw new Error('something went wrong');
                }
            })
            .then(callback)
            .catch((error) => {
                console.log(error)
            });
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
            .then(response => {

                if (response.ok) {
                    return response.json();
                }
                else if (response.status === '404') {
                    throw new Error('Title not found');
                }
                else {
                    throw new Error('something went wrong');
                }
            })
            .then(callback)
            .catch((error) => {
                console.log(error)
            });
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
                .then(response => {

                    if (response.ok) {
                        return response.json();
                    } else {
                        throw new Error('something went wrong');
                    }
                })
                .then(callback)
                .catch((error) => {
                    console.log(error)
                });
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
                .then(response => {

                    if (response.ok) {
                        return response.json();
                    } else {
                        throw new Error('something went wrong');
                    }
                })
                .then(callback)
                .catch((error) => {
                    console.log(error)
                });
        }

    }
    let getRatingsUrlWithPageSize = (size, id) => ratingsApiUrl + id + "?pageSize=" + size;

    /* **********************************
    * Function: Get users
    * ************************************/

    const usersApiUrl = 'api/users';
    let getUsers = function ([uri, user], callback) {
        if (uri === undefined) {
            uri = usersApiUrl;
        }
        fetch("api/users", {
            headers: {
                'Authorization': parseInt(user.currentUser())
            }

        })
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('something went wrong');
                }
            })
            .then(function (data) {
                callback(data);
            })
            .catch((error) => {
                console.log(error)
            });
    }

    let getAllUsersUrlWithPageSize = (size) => usersApiUrl + "?pageSize=" + size;


    /* **********************************
    * Function: Create new user
    * ************************************/

    let createUser = function (user, callback) {
        fetch("api", {
            method: "POST", body: JSON.stringify(user), headers: {
                'Content-Type': 'application/json'
            }
        })

            .then(response => {

                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Email is already in use');
                }
            })
            .then(data => callback(data))
            .catch((error) => {
                console.log(error)
            });
    }

    /* **********************************
* Function: Create new title bookmark
* ************************************/
    let createTitleBookmark = function ([bookmark, id, user], callback) {
        fetch("api/titles/" + id, {
            method: "POST", body: JSON.stringify(bookmark), headers: {
                'Content-Type': 'application/json',
                'Authorization': parseInt(user.currentUser())
            }
        })

            .then(response => {

                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Unauthorised');
                }
            })
            .then(data => callback(data))
            .catch((error) => {
                console.log(error)
            });
    }

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
            .then(response => {

                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('something went wrong');
                }
            })
            .then(callback)
            .catch((error) => {
                console.log(error)
            });
    }


    return {
        getGenres,
        getTitleBookmarks,
        getTitle,
        getTitleDetails,
        getSimilarTitles,
        getPopularTitles,
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
        getRatings,
        createUser,
        getUsers,
        getAllUsersUrlWithPageSize,
        createTitleBookmark
    }

});


