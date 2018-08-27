var idLoadingLialog = "loading-section";


function openLoadingDialog() {
    var _loadingDialog = $("#" + idLoadingLialog);

    if (_loadingDialog.length < 1) {
        _loadingDialog =
            $("<div id='" + idLoadingLialog + "' class='text-center " + idLoadingLialog + " hidden'>" +
                "<h3><i class='glyphicon glyphicon-hourglass'></i></h3 >" +
                "</div >");
    }
    
    $(".ui-dialog").style("z-index", "399997", "important");

    _loadingDialog.dialog({
        height: 140,
        width: 100,
        zIndex: 999999,
        modal: true,
        autoOpen: true,
        resizable: false,
        draggable: false,
        closeOnEscape: false,
        open: function (event, ui) {
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar').hide(); // hide the close button           

        },
        close: function (event, ui) {
            closeLoadingDialog();
            $(".ui-dialog").css("z-index", "");
        }
    })
}

function closeLoadingDialog() {
    var _loadingDialog = $("#" + idLoadingLialog);
    try {
        _loadingDialog.dialog("close");
        _loadingDialog.dialog("destroy");
        _loadingDialog.hide();
    }
    catch (er) { }
}

function openLoadingDialog2(caminho) {
    var _loadingDialog = $("#" + idLoadingLialog);

    if (_loadingDialog.length < 1) {
        _loadingDialog =
            $("<div id='" + idLoadingLialog + "' class='text-center " + idLoadingLialog + " hidden'>" +
                "<h3><i class='glyphicon glyphicon-hourglass'></i></h3 >" +
                "</div >");
    }

    $(".ui-dialog").style("z-index", "399997", "important");

    _loadingDialog.dialog({
        height: 140,
        zIndex: 999,
        modal: true,
        autoOpen: true,
        resizable: false,
        draggable: false,
        closeOnEscape: false,
        title: ResxJs.DIALOG_LOADING_MSG,
        open: function (event, ui) {
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide(); // hide the close button
        },
        close: function (event, ui) {
            closeLoadingDialog();
            $(".ui-dialog").css("z-index", "");
        }
    })
}
