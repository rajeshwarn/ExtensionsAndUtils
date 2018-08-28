/*
*
* RULES: 
*     1º - Import Jquery.js
*     2º - Import JsonHelper.js
*     3º - In CodBeHind call InitializeAjaxCommunication(url)  
*     4º - If need diferentURL send paramtes else no put parameters
*
* SAMPLE To Call InitializeAjaxCommunication
*     ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "InitializeAjaxCommunication('" + Utils.ConfigAjaxConnection() + "')", true);
*
* SAMPLE To Call Service:
*     1º - Whith parameters:
*         CallAjaxServiceAsync({Token=876, id=1, desc='hello'}, succeededHandler, errorHandler)
*     2º - Whithout parameters
*         CallAjaxServiceAsync({}, succeededHandler, errorHandler)
*
*/

var BasePlatformUrl = "";

$.ajaxPrefilter(function (options, originalOptions, jqXHR) {
    options.async = true;
});

function InitializeAjaxCommunication(param) {
    try {
        if (/localhost/i.test(window.location.host) || isStringEmpty(param))
            var caminho = "http://localhost:12345/Communication/CommunicationHandler.ashx";
        else
            caminho = param;
    }
    catch (e) { caminho = "http://localhost:12345/Communication/CommunicationHandler.ashx"; }

    BasePlatformUrl = caminho;
}

function CallAjaxService(methodName, parameters, succeededHandler, errorHandler, doneHandler, url) {

    openLoadingDialog();
    
    try {

        CallGenericService(
        methodName,
        false,
        parameters,
        function (msg) {

            succeededHandler(msg);

            closeLoadingDialog();
        },
        function (msg) {
            closeLoadingDialog();

            if (errorHandler) {
                try { msg = msg.statusText; } catch (err) {
                try { msg = msgGenericError;  } catch (err) {
                    msg = 'Lamentamos mas não foi possivel efetuar a operação pretendida'; }
                }
                errorHandler(msg);
            }
            else {
                if (ShowAlertErrorComunication) {
                    ShowAlertErrorComunication();
                }
            }


        },
        function (msg) {
            closeLoadingDialog();

            if (doneHandler) {
                doneHandler();
            }
        }, url);

    } catch (e) {

        closeLoadingDialog();

        if (errorHandler) {
            errorHandler(e);
        }
        else {
            if (ShowAlertErrorComunication) {
                ShowAlertErrorComunication();
            }
        }
    }
}

function CallAjaxServiceAsync(methodName, parameters, succeededHandler, errorHandler, doneHandler, url) {

    openLoadingDialog();
    
    try {

        CallGenericService(
        methodName,
        true,
        parameters,
        function (msg) {

            succeededHandler(msg);

            closeLoadingDialog();
        },
        function (msg) {
            closeLoadingDialog();

            if (errorHandler) {
                try { msg = msg.statusText; } catch (err) {
                    try { msg = msgGenericError; } catch (err) {
                        msg = 'Lamentamos mas não foi possivel efetuar a operação pretendida';
                    }
                }
                errorHandler(msg);
            }
            else {
                if (ShowAlertErrorComunication) {
                    ShowAlertErrorComunication();
                }
            }


        },
        function (msg) {
            closeLoadingDialog();

            if (doneHandler) {
                doneHandler();
            }
        }, url);

    } catch (e) {

        closeLoadingDialog();

        if (errorHandler) {
            errorHandler(e);
        }
        else {
            if (ShowAlertErrorComunication) {
                ShowAlertErrorComunication();
            }
        }
    }
}

function CallGenericService(methodName, isAsync, parameters, succeededHandler, errorHandler, doneHandler, url) {
    $.ajax({
        type: "POST", //GET or POST or PUT or DELETE verb
        url: url || BasePlatformUrl + '/' + methodName, // Location of the service
        data: JSON.stringify(parameters), //Data sent to server
        contentType: "application/json; charset=utf-8", // content type sent to server
        dataType: "json", //Expected data format from server
        processdata: true, //True or False
        success: succeededHandler,
        async: isAsync,
        crossDomain: false,
        error: errorHandler// When Service call fails
    })
    .done(function (data) {        
        if (doneHandler)
            doneHandler();
    });
}