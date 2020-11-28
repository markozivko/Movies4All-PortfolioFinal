﻿
/* ****************************************************************************************************************
 *                                         Functionalitites for users
 * ****************************************************************************************************************/


//define([], () => {

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
 *                                         Functionalitites for titles
 * ****************************************************************************************************************/


define([], () => {

    /* **********************************
    * Function: Get popular titles
    * ************************************/

    let getPopularTitles = function (callback) {
        fetch("api/popular", {
            method : 'GET'

        })
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                callback(data.items);
            });
    }

    let getPopularTitle = (id, callback) => {
        fetch('api/popular/' + id)
            .then(response => response.json())
            .then(callback);
    }

    return {
        getPopularTitles,
        getPopularTitle
    }

});


