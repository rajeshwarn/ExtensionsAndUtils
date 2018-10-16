var appUtils = (function () {

    createDialog = function (title, partial, height) {
        
        $('body').append('<div class="myDialog" />').bind("dialogclose", function (event, ui) {
            $('.myDialog').remove();
            $('html').removeClass('no-scroll');
            $(this).removeClass('no-scroll');
        }).addClass('no-scroll');
        $('html').addClass('no-scroll');

        $('.myDialog').html(partial);

        $('.myDialog').dialog({
            draggable: false,
            resizable: false,
            width: 960,
            title: title,
            modal: true,
            height: height
        });

        if (height == null) {
            $('.myDialog').dialog("option", "height", avaiableWorkspaceHeight());
        }
    };
    

    createErrorDialog = function (text) {

        $(document.body).append('<div id="#dialog-error" style="font-size: 14px;" />').bind("dialogclose", function (event, ui) {
            $('#dialog-error').remove();
            $('html').removeClass('no-scroll');
            $(this).removeClass('no-scroll');
        }).addClass('no-scroll');
        $('html').addClass('no-scroll');

        if (text == null)
            text = "Lamentamos mas não foi possível concluir a operação pretendida.";
        $("#dialog-error").text(text);

        $("#dialog-error").dialog({
            draggable: false,
            resizable: false,
            height: "auto",
            width: 500,
            modal: true,
            title: "Erro",
            buttons: {
                "Ok": function () {
                    $("#dialog-error").dialog("close");
                }
            }
        });

        return $("#dialog-error");
    };


    displaySvcMsg = function () {

        var msgDiv = $(".SvcMsg");

        $(msgDiv).click(function() {
            $(this).detach();
        });

        if (!isStringEmpty($(msgDiv).justtext()))
            setTimeout(function () {
                $(msgDiv).hide('blind', {}, 500);
                $(msgDiv).detach();
            }, 5000);
    };

    

    isBusy = function (value) {
        if (value) {
            $(".loading").show();
        }
        else {
            $(".loading").hide();
        }
    };

    return {

        createDialog: function (title, partial, height) {

            createDialog(title, partial, height);
        },
            
        createErrorDialog: function (text) {
            createErrorDialog(text);
        },

        displaySvcMsg: function () {
            displaySvcMsg();
        },

        isBusy: function (value) {
            isBusy(value);
        }
    }

})();
