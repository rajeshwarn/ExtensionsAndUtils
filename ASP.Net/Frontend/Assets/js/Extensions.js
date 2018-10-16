jQuery.fn.exists = function () { return this.length > 0; };

jQuery.expr[':'].containsInsensitive = function (a, i, m) {
    return jQuery(a).text().toUpperCase()
        .indexOf(m[3].toUpperCase()) >= 0;
};

jQuery.fn.justtext = function () {

    return $(this).clone()
            .children()
            .remove()
            .end()
            .text().replace(/ /g, '').replace(/\n/g, '');

};

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
$.fn.SafeAny = function (comparerFn) {
	var elem = this.get();

	if (!exists(elem))
		return false;
	else if (exists(elem) && isStringEmpty(comparerFn))
		return true;
	else
		return elem.inArrayWhereFn(comparerFn);
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

Array.prototype.IsArrayEqual = function (array) {
	// if the other array is a falsy value, return
	if (!array)
		return false;

	// compare lengths - can save a lot of time 
	if (this.length != array.length)
		return false;

	for (var i = 0, l=this.length; i < l; i++) {
		// Check if we have nested arrays
		if (this[i] instanceof Array && array[i] instanceof Array) {
			// recurse into the nested arrays
			if (!this[i].equals(array[i]))
				return false;       
		}           
		else if (this[i] != array[i]) { 
			// Warning - two different object instances will never be equal: {x:20} != {x:20}
			return false;   
		}           
	}       
	return true;
}

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
