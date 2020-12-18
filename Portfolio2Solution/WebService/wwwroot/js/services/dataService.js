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
* Function: Rate title
* ************************************/
    let rateTitle = function ([rating, id, user], callback) {
        fetch("api/titles/Ratings/" + id, {
            method: "POST", body: JSON.stringify(rating), headers: {
                'Content-Type': 'application/json',
                'Authorization': parseInt(user.currentUser())
            }
        })

            .then(response => {

                if (response.ok) {
                    return response.json();
                } else if (response.status == '400') {
                    throw new Error('Title already rated');
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
                } else if (response.status == '400') {
                    throw new Error('Bookmark already exist for this movie');
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
    * Function: createFavoritePerson
    * ************************************/
    let createFavoritePerson = function ([bookmark, id, user], callback) {
        fetch("api/persons/" + id, {
            method: "POST", body: JSON.stringify(bookmark), headers: {
                'Content-Type': 'application/json',
                'Authorization': parseInt(user.currentUser())
            }
        })

            .then(response => {

                if (response.ok) {
                    return response.json();
                } else if (response.status == '400') {
                    throw new Error('Person already exist in your favorite list');
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
    * Function: Update notes title bookmarks
    * ************************************/

    let updateNotesForTitleBookmark = function ([bookmark,user], callback) {
        fetch("api/titlebookmarks/" + user.currentUser(), {
            method: "PUT",body: JSON.stringify(bookmark), headers: {
                'content-type': 'application/json',
                'authorization': parseInt(user.currentUser())
            }
        })
            .then(response => {

                if (response.ok) {
                    return "ok";
                }  if (response.status == '404') {
                    throw new Error('This title is not in your bookmarks');
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
 * Function: Update notes title bookmarks
 * ************************************/

    let updateNotesForPersonalities = function ([person, user], callback) {
        fetch("api/personalities/" + user.currentUser(), {
            method: "PUT", body: JSON.stringify(person), headers: {
                'content-type': 'application/json',
                'authorization': parseInt(user.currentUser())
            }
        })
            .then(response => {

                if (response.ok) {
                    return "ok";
                } if (response.status == '404') {
                    throw new Error('This personality was not bookmarked');
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
    * Function: Delete user
    * ************************************/
    let unsubscribeUser = (user) => fetch("api/users/" + user.currentUser(), {
        method: "DELETE", headers: {
            'Authorization': parseInt(user.currentUser())
        }
    });
   /* **********************************
    * Function: Delete customer
    * ************************************/
    let unsubscribeCustomer = (customerId, user) => fetch("api/users/" + customerId, {
        method: "DELETE", headers: {
            'Authorization': parseInt(user.currentUser())
        }
    });
    /* **********************************
    * Function: Delete title bookmarks
    * ************************************/
    let deleteTitleFromBookmarks = (id, user) => fetch("api/titlebookmarks/" + user.currentUser() + "/" + id, {
        method: "DELETE", headers: {
            'Authorization': parseInt(user.currentUser())
        }
    });
    /* **********************************
   * Function: Delete person
   * ************************************/
    let deletePersonFromPersonalities = (id, user) => fetch("api/personalities/" + user.currentUser() + "/" + id, {
        method: "DELETE", headers: {
            'Authorization': parseInt(user.currentUser())
        }
    });
    /* **********************************
    * Function: Delete rating
    * ************************************/
    let deleteRating = (id, user) => fetch("api/ratings/" + user.currentUser() + "/" + id, {
        method: "DELETE", headers: {
            'Authorization': parseInt(user.currentUser())
        }
    });

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
/* ****************************************************************************************************************
*                                         Functionalitites for searching
* ****************************************************************************************************************/
    /* **********************************
     * Function: Simple Search
     * ************************************/
    const simpleSearchApiUrl = 'api/search/simple';
    let simpleSearch = function ([uri, search, user], callback) {
        if (uri === undefined) {
            uri = simpleSearchApiUrl + "?search=" + search;
        }
        fetch(uri , {
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
    let simpleSearchUrlWithPageSize = (size, search) => simpleSearchApiUrl + "?search=" + search+ "?pageSize=" + size;
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
        createTitleBookmark,
        createFavoritePerson,
        rateTitle,
        unsubscribeUser,
        deleteTitleFromBookmarks,
        unsubscribeCustomer,
        deletePersonFromPersonalities,
        deleteRating,
        simpleSearchUrlWithPageSize,
        simpleSearch,
        updateNotesForTitleBookmark,
        updateNotesForPersonalities
    }

});


