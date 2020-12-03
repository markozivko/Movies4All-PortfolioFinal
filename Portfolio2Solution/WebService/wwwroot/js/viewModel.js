﻿define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
  
    let currentComponent = ko.observable('navbar');
 
    let changeContent = (component) => {
        currentComponent(component);
    }
   

    //TODO: add user url
    postman.subscribe("swithToAccount", component => {
        console.log(component);
        changeContent(component);
    });



    return {
        currentComponent
    };
});