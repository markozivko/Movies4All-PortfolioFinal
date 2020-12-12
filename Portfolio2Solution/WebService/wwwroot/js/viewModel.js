define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
  
    let currentComponent = ko.observable('navbar');
    let currentUser = ko.observable();
    let currentParams = ko.observable({currentUser})
 
    let changeContent = (component) => {
        currentComponent(component);
    }
   

    postman.subscribe("switchToAccount", component => {
        console.log("Comp 0");
        console.log(component[0]);
        changeContent(component[0]);
        if (component[0] == "account") {
            currentUser(component[1])
        }
    });

    postman.subscribe('logout', component => {

        currentComponent(component);

    });


    return {
        currentComponent,
        currentParams
    };
});