define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        let user = params.user;
        

        postman.subscribe('changeUser', user => {

            console.log(user.fName);

        });

        return {
            user
        }
    }
});