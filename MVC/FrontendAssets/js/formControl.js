var formControl = (function () {
    var initialData, wantSubmit = false;

    $(document).ready(function () {

        //displayErrorTableFields();

        if (exists($(":input")))
            initialData = $(":input").serializeObject();

        //if (exists($(".pageContent")))
        //    $(".pageContent").submit(function () {
        //        wantSubmit = true;
        //        //$(".loading").fadeOut("slow");
        //    });

        if (exists($(".submitBt")))
            $(".submitBt").unbind("click").click(function () {
                wantSubmit = true;
                //$(".loading").fadeOut("slow");
            });

        //$(window).unbind('beforeunload').bind('beforeunload', function () {
        if (exists($(".cancelBt")))
            $(".cancelBt").unbind("click").click(function () {
                if (!wantSubmit && exists($(":input")) && hasUnsaved()) {
                    var alertMsg = "Confirma que pretende sair sem guardar as alterações efetuadas?";
                    if (isStringEmpty(sairSemGuardarMsg)) alertMsg = sairSemGuardarMsg;
                    ShowConfirmDialog(alertMsg, 2);
                    return false;
                }
                return true;
            });
    });


    function hasUnsaved() {
        return JSON.stringify(initialData) !== JSON.stringify($(":input").serializeObject());
    }


    displayErrorTableFields = function () {
        $("table").not(".table-custom").each(function () {
            $($($(this).children("tbody")).children("tr")).children("td").each(function () {

                if (exists($($(this).find("span.field-validation-error")))) {
                    var input = $("[name='" + $($(this).find("span.field-validation-error")).data("valmsg-for") + "']");
                    if (exists($(input))) {
                        if (!$(input).hasClass("input-validation-error"))
                            $(input).addClass("input-validation-error");
                        $(input).attr("title", $($(this).find("span.field-validation-error")).text());
                        $($(this).find("span.field-validation-error")).hide();
                    }
                }
            });
        });
    }

    cleanErrorMsgInsideTable = function () {
        var input;

        if (exists($("table input")))
            $("table input").unbind("oninput").on("input", function () {
                if (exists($(this).closest("td"))) {
                    if (!$(this).valid()) {
                        input = $("[name='" + $($(this).closest("td").find("span.field-validation-error")).data("valmsg-for") + "']");
                        if (!$(input).hasClass("input-validation-error"))
                            $(input).addClass("input-validation-error");
                        $(input).attr("title", $($(this).closest("td").find("span.field-validation-error")).text());
                        $($(this).closest("td").find("span.field-validation-error")).hide();
                    }
                    else {
                        input = $("[name='" + $($(this).closest("td").find("span.field-validation-valid")).data("valmsg-for") + "']");
                        $(input).removeClass("input-validation-error");
                        $(input).attr("title", null);
                        $($(this).closest("td").find("span.field-validation-error")).removeClass("field-validation-error").addClass("field-validation-valid");
                        $($(this).closest("td").find("span.field-validation-valid")).empty().show();
                    }
                }
            });

        if (exists($("table select")))
            $("table select").unbind("blur").blur(function () {
                if (exists($(this).closest("td"))) {
                    var input = $("[name='" + $($(this).closest("td").find("span.field-validation-error")).data("valmsg-for") + "']");
                    if (exists(!$(this).valid() && $(input))) {
                        if (!$(input).hasClass("input-validation-error"))
                            $(input).addClass("input-validation-error");
                        $(input).attr("title", $($(this).closest("td").find("span.field-validation-error")).text());
                        $($(this).closest("td").find("span.field-validation-error")).hide();
                    }
                    else {
                        input = $("[name='" + $($(this).closest("td").find("span.field-validation-valid")).data("valmsg-for") + "']");
                        $(input).removeClass("input-validation-error");
                        $(input).attr("title", null);
                        $($(this).closest("td").find("span.field-validation-error")).removeClass("field-validation-error").addClass("field-validation-valid");
                        $($(this).closest("td").find("span.field-validation-valid")).empty().show();
                    }
                }
            });
    }

    isChecked = function (item) {
        return $(item).is(":checked");
        //|| $(item).attr("checked") == "checked" || $(item).attr("checked") == true
        //|| $(item).prop("checked") == "checked" || $(item).prop("checked") == true;
    }

    getFormDataByClass = function (form) {
        var inputs = $(form).find("[class^='name-']");
        var itemToSave = {};

        $(inputs).each(function () {

            var fieldClassName = $.grep(this.className.split(" "), function (v, i) {
                return v.indexOf('name') === 0;
            }).join();

            var fieldName = fieldClassName.substring(fieldClassName.lastIndexOf('-') + 1);

            if ($(this).is("select"))
                itemToSave[fieldName] = $($(this).find("option:selected")).val();

            if ($(this).is("table")) {
                if ($($(this).find("input[type='radio']")).length > 0)
                    //itemToSave[fieldName] = $($(this).find("input[type='radio']:checked")).val();
                {

                    var checksGroup = $($(this).find("input[type='radio']:checked")).parent();
                    itemToSave[fieldName] = [];

                    $.each(checksGroup, function (index) {
                        var checkInputs = $(this).find("[name]:not([name='__RequestVerificationToken'])");
                        var newInput = {};

                        $.each(checkInputs, function (index) {
                            var checkFieldName = $(this).attr("name");
                            newInput[checkFieldName] = $(this).val();
                        });

                        itemToSave[fieldName].push(newInput);
                    });
                }
                else if ($($(this).find("input[type='checkbox']")).length > 0)
                    //itemToSave[fieldName] = $($(this).find("input[type='checkbox']:checked")).val();
                {

                    var checksGroup = $($(this).find("input[type='checkbox']:checked")).parent();
                    itemToSave[fieldName] = [];

                    $.each(checksGroup, function (index) {
                        var checkInputs = $(this).find("[name]:not([name='__RequestVerificationToken'])");
                        var newInput = {};

                        $.each(checkInputs, function (index) {
                            var checkFieldName = $(this).attr("name");
                            newInput[checkFieldName] = $(this).val();
                        });

                        itemToSave[fieldName].push(newInput);
                    });
                }
            }

            if ($(this).is("input[type='text']") || $(this).is("input[type='hidden']"))
                itemToSave[fieldName] = $(this).val();

            if ($(this).is("textarea"))
                itemToSave[fieldName] = $(this).val();

            if (($(this).is("input[type='radio']") || $(this).is("input[type='checkbox']")) && isChecked($(this)))
                itemToSave[fieldName] = $(this).val();

            if ($(this).is("span")) {

                if ($($(this).find("input[type='radio']")).length == 1)
                    itemToSave[fieldName] = isChecked($(this).find("input[type='radio']"));

                if ($($(this).find("input[type='checkbox']")).length == 1)
                    itemToSave[fieldName] = isChecked($(this).find("input[type='checkbox']"));
            }
        });

        return itemToSave;
    }

    getFormData = function (form, withToken) {
        if (withToken === undefined || withToken == null)
            withToken = false;

        var inputs = withToken ? $(form).find("[name]") : $(form).find("[name]:not([name='__RequestVerificationToken'])");
        var itemToSave = {};

        $(inputs).each(function () {
            var thisInput = this;

            var fieldName = $(this).attr("name");

            if ($(this).is("select"))
                itemToSave[fieldName] = $($(this).find("option:selected")).val();
            
            if ($(this).is("table")) {
                if ($($(this).find("input[type='radio']:checked")).length > 0) {
                    var checksGroup = $($(this).find("input[type='radio']:checked")).parent();
                    itemToSave[fieldName] = [];

                    $.each(checksGroup, function (index) {
                        var checkInputs = $(this).find("[name]:not([name='__RequestVerificationToken'])");
                        var newInput = {};

                        $.each(checkInputs, function (index) {
                            var checkFieldName = $(this).attr("name");
                            newInput[checkFieldName] = $(this).val();
                        });

                        itemToSave[fieldName].push(newInput);
                    });
                }
                else if ($($(this).find("input[type='checkbox']:checked")).length > 0) {
                    //itemToSave[fieldName] = $($(this).find("input[type='checkbox']:checked")).val();
                    var checksGroup = $($(this).find("input[type='checkbox']:checked")).parent();
                    itemToSave[fieldName] = [];

                    $.each(checksGroup, function (index) {
                        var checkInputs = $(this).find("[name]:not([name='__RequestVerificationToken'])");
                        var newInput = {};

                        $.each(checkInputs, function (index) {
                            var checkFieldName = $(this).attr("name");
                            newInput[checkFieldName] = $(this).val();
                        });

                        itemToSave[fieldName].push(newInput);
                    });
                }
            }

            if ($(this).is("input[type='text']") || $(this).is("input[type='hidden']") || $(this).is("input[type='password']")) {
                if ($(this).is("input[type='hidden']") && $(inputs).not("[type='hidden']").is("[name='" + fieldName + "']"))
                { }
                else
                    itemToSave[fieldName] = $(this).val();
            }

            if ($(this).is("textarea"))
                itemToSave[fieldName] = $(this).val();

            if (($(this).is("input[type='radio']") || $(this).is("input[type='checkbox']")) && isChecked($(this))) {
                itemToSave[fieldName] = $(this).val();
            }

            if ($(this).is("span")) {

                if ($($(this).find("input[type='radio']")).length == 1)
                    itemToSave[fieldName] = isChecked($(this).find("input[type='radio']"));

                if ($($(this).find("input[type='checkbox']")).length == 1)
                    itemToSave[fieldName] = isChecked($(this).find("input[type='checkbox']"));
            }
        });
        
        return itemToSave;
    }

    return {
        displayErrorTableFields: function () {
            displayErrorTableFields();
        },
        cleanErrorMsgInsideTable: function () {
            cleanErrorMsgInsideTable();
        },
        setInitialData: function () {
            if (exists($(":input")))
                initialData = $(":input").serializeObject();
        },
        getFormData: function (form, withToken) {
            return getFormData(form, withToken);
        },
        getFormDataByClass: function (form) {
            return getFormDataByClass(form);
        },
        isChecked: function (item) {
            return isChecked(item);
        }
    }
})();