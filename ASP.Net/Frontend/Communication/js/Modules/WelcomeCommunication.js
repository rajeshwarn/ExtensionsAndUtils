var MyModuleCommunication = new function () {

    this.Call_GetSomething = function (parameters, succeededHandler, errorHandler) {
        CallAjaxServiceAsync("MyModule_GetSomething", parameters, succeededHandler, errorHandler);
    }
    
}