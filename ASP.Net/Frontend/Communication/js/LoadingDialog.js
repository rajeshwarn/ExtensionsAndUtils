var idLoadingLialog = "loading-dialog";

var LoadingDialogImagePath = "Image/Icons/LoaderFinal.gif";
try { LoadingDialogImagePath = "http://" + window.location.host + "/Image/Icons/LoaderFinal.gif"; } catch (err) { }


function openLoadingDialog() {
    var _loadingDialog = $("#" + idLoadingLialog);

    if (_loadingDialog.length < 1) {
        _loadingDialog =
            $("<div id='" + idLoadingLialog + "' class='" + idLoadingLialog + "' style='text-align: center;vertical-align: middle; height: 100%'>" +
                "<img src='" + LoadingDialogImagePath + "' style='margin: 20px;' alt='loading' />" +
            "</div>");
    }

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
        }
    })
}

function closeLoadingDialog() {
    var _loadingDialog = $("#" + idLoadingLialog);

    try{
        if (_loadingDialog.length > 0) {
            _loadingDialog.dialog("destroy");
            _loadingDialog.hide();
        }
    }
    catch(err) { }
}

function openLoadingDialog2(caminho) {
    var _loadingDialog = $("#" + idLoadingLialog);

    if (_loadingDialog.length < 1) {
        _loadingDialog =
            $("<div id='" + idLoadingLialog + "' class='" + idLoadingLialog + "' style='text-align: center;vertical-align: middle; height: 100%'>" +
                "<img src='" + caminho + "' style='margin: 20px;' alt='loading' />" +
            "</div>");
    }

    _loadingDialog.dialog({
        height: 140,
        zIndex: 999,
        modal: true,
        autoOpen: true,
        resizable: false,
        draggable: false,
        closeOnEscape: false,
        title: 'A carregar',
        open: function (event, ui) {
            $(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide(); // hide the close button
        },
        close: function (event, ui) {
            closeLoadingDialog();
        }
    })
}
