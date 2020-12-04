define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
  
    let currentComponent = ko.observable('navbar');
 
    let changeContent = (component) => {
        currentComponent(component);
    }
   

    postman.subscribe("switchToAccount", component => {
        changeContent(component);
    });

    postman.subscribe('logout', component => {

        currentComponent(component);

    });


    return {
        currentComponent
    };
});