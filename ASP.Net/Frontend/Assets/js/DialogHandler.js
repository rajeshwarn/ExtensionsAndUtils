function ShowAlertDialog(msg, result, time, operationClose, closeAll) {
    if (isStringEmpty(msg))
        msg = ResxJs.ALERT_GENERIC_ERROR;

    var iconImg = "", iconArea = "";
    if (result) {
        if (result * 1 == 1) iconImg = "glyphicon-ok green";
        else if (result * 1 == -1) iconImg = "glyphicon-remove red";
        else iconImg = "glyphicon-warning-sign yellow";
        iconArea = "<div class='icon glyphicon " + iconImg + "'></div>";
    }

    var _loadingDialog = $("#AlertMasterDialog");

    if (_loadingDialog.length < 1)
        _loadingDialog = $("<div id='AlertMasterDialog' style='text-align: center;'></div>");

    _loadingDialog.html("<div class='AlertMasterDialogContainer'>" +
        iconArea +
        "<div>" + msg + "</span>" +
        "</div>");

    var heigthH = 180;
    var resize = false;
    if (msg != null) {
        heigthH += (msg.match(/<br/g) || []).length * 20;
        resize = true;
    }

    if (closeAll)
        $(".ui-dialog-content").dialog("close");

    _loadingDialog.dialog({
        width: 550,
        heigth: heigthH,
        minHeight: heigthH,
        zIndex: 100000,
        modal: true,
        autoOpen: true,
        resizable: resize,
        draggable: false,
        closeOnEscape: false,
        open: function (event, ui) {
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();// hide the close button
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar.ui-widget-header').detach();
        },
        buttons:
        [{
            text: ResxJs.GLOBAL_OK,
            click: function () {
                $(this).dialog("close");

                try {
                    if (typeof operationClose !== "undefined" && operationClose != null)
                        operationClose();
                }
                catch (er) { }
            }
        }]
    });

    var timeout = 3000;
    try {
        if (time !== undefined && time != null && time != '')
            timeout = time * 1;

        if (timeout == 0)
            timeout = 3000;
    }
    catch (err) { }

    if (result && result * 1 == 1) //sucesso
    {
        if (timeout < 0) {
            if (_loadingDialog && _loadingDialog.dialog("isOpen"))
                _loadingDialog.dialog("close");
        }
        else {
            setTimeout(function () {
                if (_loadingDialog && _loadingDialog.dialog("isOpen"))
                    _loadingDialog.dialog("close");
            }, timeout);
        }
    }
}

function ShowConfirmDialog(msg, result, operation, operationCancel, closeAll) {

    var iconImg = "", iconArea = "";
    if (result) {
        if (result * 1 == 1) iconImg = "glyphicon-ok green";
        else if (result * 1 == -1) iconImg = "glyphicon-remove red";
        else iconImg = "glyphicon-warning-sign yellow";
        iconArea = "<div class='icon glyphicon " + iconImg + "'></div>";
    }

    if (closeAll === undefined || closeAll == null)
        closeAll = true;

    var _loadingDialog = $("#AlertMasterDialog");

    if (_loadingDialog.length < 1)
        _loadingDialog = $("<div id='AlertMasterDialog' style='text-align: center;'></div>");

    _loadingDialog.html("<div class='AlertMasterDialogContainer'>" +
        iconArea +
        "<div>" + msg + "</span>" +
        "</div>");

    var yesBtText = ResxJs.GLOBAL_OK, cancelBtText = ResxJs.GLOBAL_CANCEL;

    if (closeAll)
        $(".ui-dialog-content").dialog("close");

    _loadingDialog.dialog({
        width: 550,
        heigth: 550,
        zIndex: 100000,
        modal: true,
        autoOpen: true,
        resizable: false,
        draggable: false,
        closeOnEscape: false,
        open: function (event, ui) {
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();// hide the close button
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar.ui-widget-header').detach();
        },
        buttons:
        [
            {
                text: yesBtText,
                click: function () {
                    $(this).dialog("close");

                    try {
                        if (typeof operation !== "undefined" && operation != null)
                            operation();
                        else
                            $(".cancelHiddenBt").trigger("click");
                    }
                    catch (er) { }
                },
            },
            {
                text: cancelBtText,
                click: function () {
                    $(this).dialog("close");
                    
                    try {
                        if (typeof operationCancel !== "undefined" && operationCancel != null)
                            operationCancel();
                    }
                    catch (er) { }

                    return false;
                }
            }
        ]
    });
}

function ErrorCommunication(msg, operationClose) {

    if (isStringEmpty(msg))
        msg = ResxJs.ALERT_GENERIC_ERROR;
    try { msg = msgGenericError; } catch (err) { }

    var iconImg = "glyphicon-warning-sign red";
    var iconArea = "<div class='icon glyphicon " + iconImg + "'></div>";

    var _loadingDialog = $("#AlertMasterDialog");

    if (_loadingDialog.length < 1)
        _loadingDialog = $("<div id='AlertMasterDialog' style='text-align: center;'></div>");

    _loadingDialog.html("<div class='AlertMasterDialogContainer'>" +
        iconArea +
        "<div>" + msg + "</span>" +
        "</div>");

    $(".ui-dialog-content").dialog("close");

    _loadingDialog.dialog({
        width: 550,
        heigth: 550,
        zIndex: 100000,
        modal: true,
        autoOpen: true,
        resizable: false,
        draggable: false,
        closeOnEscape: false,
        open: function (event, ui) {
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();// hide the close button
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar.ui-widget-header').detach();
        },
        buttons:
        [{
            text: ResxJs.GLOBAL_OK,
            click: function () {
                $(this).dialog("close");

                try {
                    if (typeof operationClose !== "undefined" && operationClose != null)
                        operationClose();
                }
                catch (er) { }
            }
        }]
    });

}

function ShowGeneralDialog(dialogId, htmlContent, isAutoOpen, dialogButtons)
{
    if (isStringEmpty(dialogId))
        dialogId = "generalDialog";

    if (isStringEmpty(htmlContent))
        htmlContent = "";

    if (!dialogButtons || dialogButtons == null)
        dialogButtons = [];

    if (isAutoOpen === undefined || isAutoOpen == null)
        isAutoOpen = true;


    var _loadingDialog = $("#" + dialogId);

    if (_loadingDialog.length < 1)
        _loadingDialog = $("<div id='" + dialogId + "' class='text-center' tabindex='-1' role='dialog' data-keyboard='false' data-backdrop='static'></div>");
    
    _loadingDialog.html("<div class='dialog-content'>" + htmlContent + "</div>");

    
    _loadingDialog.dialog({
        width: 700,
        heigth: 400,
        minHeight: 200,
        zIndex: 100000,
        modal: true,
        autoOpen: isAutoOpen,
        resizable: true,
        draggable: true,
        closeOnEscape: false,
        open: function (event, ui) {
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();// hide the close button
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar.ui-widget-header').detach();
        },
        buttons: dialogButtons
    });
}