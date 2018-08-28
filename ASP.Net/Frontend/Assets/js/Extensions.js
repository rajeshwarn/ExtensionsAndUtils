

/*function isStringEmpty(text) {
        
    var result = text.replace(/^\s+|\s+$/gm, '');

    if (result == "")
        return true;
    return false;
}*/

function isStringEmpty(value) {
    var valueWithoutSpaces = $.trim(value);
    return valueWithoutSpaces == "" || valueWithoutSpaces == null || valueWithoutSpaces === undefined;
}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

jQuery.fn.exists = function () { return this.length > 0; };

function exists(elem) {
    var array = $(elem).FindWhereFn(function (e) {
        return e != null;
    });
    if (array == null) array = [];
    return $(elem) !== undefined && $(elem).length > 0 && array.length > 0;
}

jQuery.fn.justtext = function () {

    return $(this).clone()
            .children()
            .remove()
            .end()
            .text().replace(/ /g, '').replace(/\n/g, '');

};


$.fn.showOption = function() { //tem em contar outros browser para além do chrome (no chrome o simple .hide ou .show funcionam mas em outros browsers não)
    this.each(function() {
        if (this.tagName == "OPTION") {
            var opt = this;
            if ($(this).parent().get(0).tagName == "SPAN") {
                var span = $(this).parent().get(0);
                $(span).replaceWith(opt);
                $(span).remove();
            }
            opt.disabled = false;
            $(opt).show();
        }
    });
    return this;
};

$.fn.hideOption = function() { //tem em contar outros browser para além do chrome (no chrome o simple .hide ou .show funcionam mas em outros browsers não)
    this.each(function() {
        if (this.tagName == "OPTION") {
            var opt = this;
            if ($(this).parent().get(0).tagName == "SPAN") {
                var span = $(this).parent().get(0);
                $(span).hide();
            } else {
                $(opt).wrap("span").hide();
            }
            opt.disabled = true;
        }
    });
    return this;
};

$.fn.getOption = function(optionText) { //tem em contar outros browser para além do chrome (no chrome o simple .hide ou .show funcionam mas em outros browsers não)
    var option;
    this.each(function() {
        if ($(this).text() == optionText) {
            option = $(this);
            return false;
        }
    });

    return option;
};

$.fn.getSelectedVal = function () { //tem em contar outros browser para além do chrome (no chrome o simple .hide ou .show funcionam mas em outros browsers não)

    var allSelected = this.find("option[selected='selected']");
    $(allSelected).append(this.find("option:selected"));

    return allSelected;
};


jQuery.fn.selectedText = function () {

if($(this).is("select"))
    return $(this).find(":selected").text();
else 
    return $(this).find(" select :selected").text();
};

function postRequest() {
    AddAntiForgeryToken = function(data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    };
    return {
        dataToSend: function(data) {
            return AddAntiForgeryToken(data);
        }
    };
}


function setOnlyNumbersBehaviour(inputElement, maxNumber, minNumber) {
    if ($(inputElement) !== undefined && $(inputElement).length > 0)
        $(inputElement).each(function () {
            $(this).on("keypress keyup blur", function (e) {
                var elem = $(this);
                try {
                    //para permitir apenas numeros
                    var val = $(elem).val();
                    var code = e.charCode || e.keyCode;
                    var keyCode = (e.keyCode && e.keyCode > 0) || !e.which ? e.keyCode : e.which;

                    if (e.charCode && keyCode && e.charCode == keyCode) {
                        var charCode = (e.which) ? e.which : code;
                        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                            return false;
                        }
                        else {
                            var charcodeStr = String.fromCharCode(charCode);
                            if (isNaN(charcodeStr)) {
                                return false;
                            }
                            else {
                                val = (val + charcodeStr) * 1;

                                if (maxNumber != null && val > maxNumber) {
                                    $(elem).val(maxNumber);
                                    return false;
                                }
                                else if (minNumber != null && val < minNumber) {
                                    $(elem).val(minNumber);
                                    return false;
                                }
                            }
                        }
                    }
                }
                catch (er) {
                    return false;
                }
            });
        });
}

/*
function setOnlyNumbersBehaviour(inputElement, maxNumber, minNumber) {
    if ($(inputElement) !== undefined && $(inputElement).length > 0)
        $(inputElement).each(function () {
            $(this).on("keypress keyup blur", function (e) {
                try {
                    //para permitir apenas numeros
                    var val = $(this).val();
                    var code = e.charCode || e.keyCode;
                    var charCode = (e.which) ? e.which : code;
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else {
                        val = (val + String.fromCharCode(charCode)) * 1;

                        if (maxNumber != null && val > maxNumber) {
                            $(this).val(maxNumber);
                            return false;
                        }
                        else if (minNumber != null && val < minNumber) {
                            $(this).val(minNumber);
                            return false;
                        }
                    }
                }
                catch (er) {
                    return false;
                }
            });
        });
}
*/


function setOnlyLettersBehaviour(inputElement) {
    if ($(inputElement) !== undefined && $(inputElement).length > 0)
        $(inputElement).each(function () {
            $(this).on("keypress keyup blur", function (e) {
                var code = e.charCode || e.keyCode;
                var charCode = (e.which) ? e.which : code;
                var val = String.fromCharCode(charCode);
                if (!/^[a-zA-Z]+$/.test(val))
                    return false;
            });
        });
}

function setPhoneNumberBehaviour(inputElement) {
    if ($(inputElement) !== undefined && $(inputElement).length > 0)
        $(inputElement).each(function () {
            $(this).unbind("keypress keyup blur").on("keypress keyup blur", function (e) {
                //para permitir apenas numeros, "+", "-", "." e " "
                var code = e.charCode || e.keyCode;
                var charCode = (e.which) ? e.which : code;
                if ((charCode < 48 || charCode > 57) && charCode != 43 && charCode != 45 && charCode != 46 && charCode != 32)
                    return false;
            });
        });
}

function setCharactersLimit(inputElement, charNumber, errorMsg) {
    if ($(inputElement) !== undefined && $(inputElement).length > 0)
        $(inputElement).unbind('keyup change input paste').bind('keyup change input paste', function (e) {
            var $this = $(this);
            var val = $this.val();
            var valLength = val.length;

            if (valLength > charNumber) {
                $this.val($this.val().substring(0, charNumber));

                if (errorMsg !== undefined && errorMsg != null && $.trim(errorMsg).length > 0)
                    alert(errorMsg);
            }
        });
}

function setNumberLimit(inputElement, maxNumber, minNumber, errorMsg) {
    if ($(inputElement) !== undefined && $(inputElement).length > 0)
        $(inputElement).unbind('keyup change input paste').bind('keyup change input paste', function (e) {
            var $this = $(this);
            var val = $this.val();

            if (val > maxNumber) {
                $this.val(maxNumber);

                if (errorMsg !== undefined && errorMsg != null && $.trim(errorMsg).length > 0)
                    alert(errorMsg);
            }
            else if (val < minNumber) {
                $this.val(minNumber);

                if (errorMsg !== undefined && errorMsg != null && $.trim(errorMsg).length > 0)
                    alert(errorMsg);
            }
        });
}

jQuery.expr[':'].containsInsensitive = function (a, i, m) {
    return jQuery(a).text().toUpperCase()
        .indexOf(m[3].toUpperCase()) >= 0;
};

function parseBool(elem) {
    if (elem === undefined || elem == null)
        return false;

    var valueToCompare = $.trim(elem);
    valueToCompare = valueToCompare.toLowerCase();

    return valueToCompare == "true";
}

function parseToInt(elem) {
    try {
        if (isStringEmpty(elem))
            return 0;

        return elem * 1;
    }
    catch (err) {
        return 0;
    }
}


$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

$.fn.isChecked = function () {
    var checked = this.checked || this.is(":checked") ||
        parseBool(this.prop("checked")) || this.prop("checked") == "checked" ||
        parseBool(this.attr("checked")) || this.attr("checked") == "checked";
    return checked;
};

$.fn.isDisabled = function () {
    var disabled = this.checked || this.is(":disabled") ||
        parseBool(this.prop("disabled")) || this.prop("disabled") == "disabled" ||
        parseBool(this.attr("disabled")) || this.attr("disabled") == "disabled";
    return disabled;
};

function getDateFromString(date) {
    try {
        try {
            var dsplit = date.split("-");
            return new Date(dsplit[2], dsplit[1] - 1, dsplit[0]);
        }
        catch (e) {
            return new Date(date);
        }
    }
    catch (e) {
        return new Date($.now());
    }
}


function getStringFromDate(date, separator) {
    if (date == null)
        return "";

    try {
        if (!exists(separator))
            separator = '-';

        var dd = date.getDate();
        var mm = date.getMonth() + 1; //January is 0!

        var yyyy = date.getFullYear();
        if (dd < 10)
            dd = '0' + dd;

        if (mm < 10)
            mm = '0' + mm;

        return dd + separator + mm + separator + yyyy;
    }
    catch (ex) { }

    return "";
}


function StringFormat() {
    // The string containing the format items (e.g. "{0}")
    // will and always has to be the first argument.
    var theString = arguments[0];

    // start with the second argument (i = 1)
    for (var i = 1; i < arguments.length; i++) {
        // "gm" = RegEx options for Global search (more than one instance)
        // and for Multiline search
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }

    return theString;
}


function escapeRegExp(str) {
    return str.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}

function replaceAll(str, find, replace) {
    return str.replace(new RegExp(escapeRegExp(find), 'g'), replace);
}

$.fn.shift = function () {
    var bottom = this.get(0);
    this.splice(0, 1);
    return bottom;
};

$.fn.pop = function () {
    var top = this.get(-1);
    this.splice(this.length - 1, 1);
    return top;
};


Array.prototype.inArray = function (comparer) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == comparer) return true;
    }
    return false;
};

Array.prototype.inArrayWhereFn = function (comparerFn) {
    for (var i = 0; i < this.length; i++) {
        if (comparerFn(this[i])) return true;
    }
    return false;

    //exemplo de uso:
    /*
    var array = [{ name: "tom", text: "tasty" }];
    var element = { name: "tom", text: "tasty" };

    if (!this.inArray(function(e) { 
            return e.name === element.name && e.text === element.text; 
        })) 
    {
        this.push(element);
    }
    */
};

Array.prototype.SafeAny = function (comparerFn) {
    if (!exists(this))
        return false;
    else if (exists(this) && isStringEmpty(comparerFn))
        return true;
    else
        return this.inArrayWhereFn(comparerFn);
};

Array.prototype.FindWhereFn = function (comparerFn) {
    var aux = [];

    for (var i = 0; i < this.length; i++) {
        if (comparerFn(this[i])) aux.push(this[i]);
    }
    return aux;

    //exemplo de uso:
    /*
    var array = [{ name: "tom", text: "tasty" }];
    var bookInfo = array.FindWhereFn(function (e) {
                return e.Id == idSelected * 1;
            });
    */
};

$.fn.FindWhereFn = function (comparerFn) {

    try {
        var aux = []; var thisObj = this.get();

        for (var i = 0; i < thisObj.length; i++) {
            if (comparerFn(thisObj[i])) aux.push(thisObj[i]);
        }
        return aux;
    }
    catch (err) {
        return null;
    }

    //exemplo de uso:
    /*
    var array = [{ name: "tom", text: "tasty" }];
    var bookInfo = array.FindWhereFn(function (e) {
                return e.Id == idSelected * 1;
            });
    */
};

Array.prototype.FirstOrDefaultFn = function (comparerFn) {
    var aux = null;

    for (var i = 0; i < this.length; i++) {
        if (aux == null && comparerFn(this[i])) aux = this[i];
    }
    return aux == null ? null : aux;

    //exemplo de uso:
    /*
    var array = [{ name: "tom", text: "tasty" }];
    var bookInfo = array.FirstOrDefaultFn(function (e) {
                return e.Id == idSelected * 1;
            });
    */
};

$.fn.FirstOrDefaultFn = function (comparerFn) {
    var aux = null; var thisObj = this.get();
    
    for (var i = 0; i < thisObj.length; i++) {
        if (aux == null && comparerFn(thisObj[i])) aux = thisObj[i];
    }
    return aux == null ? null : aux;

    //exemplo de uso:
    /*
    var array = [{ name: "tom", text: "tasty" }];
    var bookInfo = array.FirstOrDefaultFn(function (e) {
                return e.Id == idSelected * 1;
            });
    */
};

Array.prototype.RemoveFromFn = function (comparerFn) {
    var aux = [];

    for (var i = 0; i < this.length; i++) {
        if (!comparerFn(this[i])) aux.push(this[i]);
    }
    return aux;

    //exemplo de uso:
    /*
    var array = [{ name: "tom", text: "tasty" }];
    var bookInfo = array.RemoveFromFn(function (e) {
                return e.Id == idSelected * 1;
            });
    */
};

Array.prototype.ExcludeFromFn = function (array) { //exclui um array de outro
    var aux = [];
    var arrayFrom = this;

    if (exists(arrayFrom) && exists(array))
        if (arrayFrom.length > array.length)
            $.each(arrayFrom, function () {
                var item = this;
                if (!array.SafeAny(function (e) { return e === item; }))
                    aux.push(item);
            });
        else {
            $.each(array, function () {
                var item = this;
                if (!arrayFrom.SafeAny(function (e) { return e === item; }))
                    aux.push(item);
            });
        }

    return aux;

    //exemplo de uso:
    /*
    var array = [{ name: "tom", text: "tasty" }];
    var array2 = [{ name: "tom", text: "tasty" }];
    var bookInfo = array.ExcludeFromFn(array2);
    */
};

Array.prototype.Count = function (comparerFn) {

    if (!exists(this) || !this.SafeAny(comparerFn))
        return 0;
    else if (exists(this) && isStringEmpty(comparerFn))
        return this.length;
    else
        return this.FindWhereFn(comparerFn).length;
};

Array.prototype.Sum = function (comparerFn, sumFn) {

    try {
        if (!exists(this) || !this.SafeAny(comparerFn))
            return 0;
        else if (exists(this) && (isStringEmpty(comparerFn) || isStringEmpty(sumFn)))
            return 0;
        else {
            var array = this.FindWhereFn(comparerFn);

            var total = 0;
            for (var i = 0; i < array.length; i++) {
                total += sumFn(array[i]);
            }

            return total;
        }
    }
    catch (er) { return 0; }

    /*usage:
    var soma = ocorrenciasRealtime.Sum(function (e) { return e.SubFiltro == ocorrencia.SubFiltro && e.Filtro == filtro; }, function (e) { return e.Total; })
    */
};

Array.prototype.Take = function (numberMax, numberMin) {

    try {
        if (numberMin === undefined || numberMin == null || numberMin < 0)
            numberMin = 0;

        if (!exists(this))
            return null;
        else
            return this.slice(numberMin, numberMax);
    }
    catch (err) {
        return this;
    }
};



function sort_by(field, reverse, primer) {

    var key = primer ?
        function (x) { var val = primer(x[field]); return $.isNumeric(val) ? val * 1 : val } :
        function (x) { var val = x[field]; return $.isNumeric(val) ? val * 1 : val };

    reverse = !reverse ? 1 : -1;

    return function (a, b) {
        return a = key(a), b = key(b), reverse * ((a > b) - (b > a));
    }
};

Array.prototype.OrderBy = function (field) {

    try {
        return this.sort(sort_by(field, false));
    }
    catch (err) {
        return this;
    }
};

Array.prototype.OrderByDescending = function (field) {

    try {
        return this.sort(sort_by(field, true));
    }
    catch (err) {
        return this;
    }
};

Array.prototype.Max = function (field) {

    try {
        var orderedArray = this.OrderByDescending(field);
        var val = (orderedArray[0])[field];
        return $.isNumeric(val) ? val * 1 : val;
    }
    catch (err) {
        return null;
    }
};

$.fn.Max = function (field) {

    try {
        var orderedArray = this.get().OrderByDescending(field);
        var val = (orderedArray[0])[field];
        return $.isNumeric(val) ? val * 1 : val;
    }
    catch (err) {
        return null;
    }
};

Array.prototype.WhereMax = function (field) {

    try {
        var orderedArray = this.OrderByDescending(field);
        return orderedArray[0];
    }
    catch (err) {
        return null;
    }
};

$.fn.WhereMax = function (field) {

    try {
        var orderedArray = this.get().OrderByDescending(field);
        return orderedArray[0];
    }
    catch (err) {
        return null;
    }
};

Array.prototype.Min = function (field) {

    try {
        var orderedArray = this.OrderBy(field);
        var val = (orderedArray[0])[field];
        return $.isNumeric(val) ? val * 1 : val;
    }
    catch (err) {
        return null;
    }
};

$.fn.Min = function (field) {

    try {
        var orderedArray = this.get().OrderBy(field);
        var val = (orderedArray[0])[field];
        return $.isNumeric(val) ? val * 1 : val;
    }
    catch (err) {
        return null;
    }
};

Array.prototype.WhereMin = function (field) {

    try {
        var orderedArray = this.OrderBy(field);
        return orderedArray[0];
    }
    catch (err) {
        return null;
    }
};

$.fn.WhereMin = function (field) {

    try {
        var orderedArray = this.get().OrderBy(field);
        return orderedArray[0];
    }
    catch (err) {
        return null;
    }
};


var ExistsPropInObj = function (obj, searchName) {
    var exists = false;
    for (var attrname in obj) {
        if (attrname == searchName)
            exists = false;
    }
    return exists;
};

var MergeObjects = function (obj1, obj2) {
    for (var attrname in obj2) {
        obj1[attrname] = obj2[attrname];
    }
    return obj1;
};

function getPos(el) {
    // yay readability
    for (var lx = 0, ly = 0;
        el != null;
        lx += el.offsetLeft, ly += el.offsetTop, el = el.offsetParent);
    return { x: lx, y: ly };
}


