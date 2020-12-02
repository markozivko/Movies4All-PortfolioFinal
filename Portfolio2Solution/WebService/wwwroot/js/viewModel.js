define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
  
    let currentComponent = ko.observable('about');
 
    let changeContent = () => {
        selectedComponent('account');
    }
   

    //TODO: add user url
    //postman.subscribe("userLoggedIn", component => {
    //    changeContent(component);
        
    //}, );



    return {
        currentComponent,
        changeContent
    };
});