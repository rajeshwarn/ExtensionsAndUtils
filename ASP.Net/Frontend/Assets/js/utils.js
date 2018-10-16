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

function exists(elem) {
    try {
        var array = $(elem).length > 0 ? $(elem).FindWhereFn(function (e) {
            return e != null;
        }) : null;
        if (array == null) array = [];
        return $(elem) !== undefined && $(elem).length > 0 && array.length > 0;
    }
    catch (er) { }
    return false;
}
		
function existsObj(obj) {
	return obj !== undefined && obj != null;
}

function setOnlyNumbersBehaviour(inputElement, maxNumber, minNumber, keepEvents) {
    if ($(inputElement) !== undefined && $(inputElement).length > 0)
        $(inputElement).each(function () {
			if(keepEvents != null && !keepEvents)
				$(this).unbind("keypress keyup blur");

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
                                else
                                {
                                    $(elem).val(val);
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

function setOnlyDecimalsBehaviour(inputElement, maxNumber, minNumber) {
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
                        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 44 && charCode != 46) {
                            return false;
                        }
                        else {
                            var charcodeStr = String.fromCharCode(charCode);
                            if (isNaN(charcodeStr) && charcodeStr != "." && charcodeStr != ",") {
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

function getFormData(form, area) {
	var inputs = $(form).find("." + area + " [class*='name-'], ." + area + "[class*='name-']");
	var baseItemToSave = {};

	$(inputs).each(function () {
	
		var itemToSave = baseItemToSave;

		var fieldClassName = $.grep(this.className.split(" "), function (v, i) {
			return v.indexOf('name') === 0;
		}).join();

		var fieldName = fieldClassName.substring(fieldClassName.lastIndexOf('-') + 1);
		
		if(fieldName.indexOf('.') >= 0)
		{
			var paths = fieldName.split(".");
			var totalPaths = paths.length;
			
			$.each(paths, function(index){
				if(index < totalPaths -1)
				{
					if(!exists(itemToSave[paths[index]]))
						itemToSave[paths[index]] = {};
					itemToSave = itemToSave[paths[index]];
				}
				else
					fieldName = paths[index];
			});
		}

		if ($(this).is("select") && !isStringEmpty($($(this).find("option:selected")).val()))
			itemToSave[fieldName] = $($(this).find("option:selected")).val();
		

		if ($(this).is("table")) {
			if ($($(this).find("input[type='radio']")).length > 0)
			{
				var objCk = $(this).find("input[type='radio']:checked");
				if(($(objCk).hasClass(area) && $(objCk).hasClass("formInputArray")) || $(objCk).parent(area).hasClass("formInputArray"))
				{	
					if(!exists(itemToSave[fieldName]))
						itemToSave[fieldName] = [];
					if(!isStringEmpty($(objCk).val()))
						itemToSave[fieldName].push($(objCk).val());
				}
				else if(!isStringEmpty($(objCk).val()))
					itemToSave[fieldName] = $(objCk).val();
			}
			else if ($($(this).find("input[type='checkbox']")).length > 0)
			{
				var objCk = $(this).find("input[type='checkbox']:checked");
				if(($(objCk).hasClass(area) && $(objCk).hasClass("formInputArray")) || $(objCk).parent(area).hasClass("formInputArray"))
				{	
					if(!exists(itemToSave[fieldName]))
						itemToSave[fieldName] = [];
					if(!isStringEmpty($(objCk).val()))
						itemToSave[fieldName].push($(objCk).val());
				}
				else if(!isStringEmpty($(objCk).val()))
					itemToSave[fieldName] = $(objCk).val();
			}
		}

		if (($(this).is("input[type='text']") || $(this).is("input[type='hidden']")) && !isStringEmpty($(this).val()))
			itemToSave[fieldName] = $(this).val();

		if ($(this).is("textarea") && !isStringEmpty($(this).val()))
			itemToSave[fieldName] = $(this).val();

		if (($(this).is("input[type='radio']") || $(this).is("input[type='checkbox']")) && isChecked($(this)))
		{
			var objCk = $(this);
			if(($(objCk).hasClass(area) && $(objCk).hasClass("formInputArray")) || $(objCk).parent(area).hasClass("formInputArray"))
			{	
					if(!exists(itemToSave[fieldName]))
						itemToSave[fieldName] = [];
					if(!isStringEmpty($(objCk).val()))
						itemToSave[fieldName].push($(objCk).val());
				}
			else if(!isStringEmpty($(objCk).val()))
				itemToSave[fieldName] = $(objCk).val();
		}

		if ($(this).is("span")) {

			if ($($(this).find("input[type='radio']")).length == 1)
				itemToSave[fieldName] = isChecked($(this).find("input[type='radio']"));

			if ($($(this).find("input[type='checkbox']")).length == 1)
				itemToSave[fieldName] = isChecked($(this).find("input[type='checkbox']"));
		}
	});
	return baseItemToSave;
}

function containsInsensitive(orig, comp) {
    return orig !== undefined && orig != null
        && comp !== undefined && comp != null 
        && orig.toUpperCase().indexOf(comp.toUpperCase()) >= 0;
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

function isChecked(item) {
	return $(item).is(":checked");
	//|| $(item).attr("checked") == "checked" || $(item).attr("checked") == true
	//|| $(item).prop("checked") == "checked" || $(item).prop("checked") == true;
}

function today() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    return new Date(yyyy, mm, dd);
}

function getDate(year, month, day) {
    var date = null;

    try {
        date = new Date(year, month, day);
    }
    catch (er) { }

    return date;
}

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

function IsObjectEmpty(obj) {
		
	if(!existsObj(obj))
		return true;

	if(typeof obj !== 'object' || $.isArray(obj))
		return false;
	
	for(var key in obj) {
		if (obj.hasOwnProperty(key)) {
			return false;
		}
	}
	return true;
}

function CleanEmptyProperties(obj) {
	for (var attrname in obj) {
		if(existsObj(obj) && typeof obj[attrname] === 'object')
		{
			if(IsObjectEmpty(obj[attrname]))
				delete obj[attrname];
			else
				obj[attrname] = CleanEmptyProperties(obj[attrname])
		}
	}
	return obj;
};

function escapeRegExp(str) {
    return str.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}

function replaceAll(str, find, replace) {
    return str.replace(new RegExp(escapeRegExp(find), 'g'), replace);
}

var CopyObject = function (obj) {
    var copy;

	// Handle the 3 simple types, and null or undefined
	if (null == obj || "object" != typeof obj) return obj;

	// Handle Date
	if (obj instanceof Date) {
		copy = new Date();
		copy.setTime(obj.getTime());
		return copy;
	}

	// Handle Array
	if (obj instanceof Array) {
		copy = [];
		for (var i = 0, len = obj.length; i < len; i++) {
			copy[i] = CopyObject(obj[i]);
		}
		return copy;
	}

	// Handle Object
	if (obj instanceof Object) {
		copy = {};
		for (var attr in obj) {
			if (obj.hasOwnProperty(attr)) copy[attr] = CopyObject(obj[attr]);
		}
		return copy;
	}

	throw new Error("Unable to copy obj! Its type isn't supported.");
};

/*
var MergeObjects = function (obj1, obj2) {
    for (var attrname in obj2) {
        obj1[attrname] = obj2[attrname];
    }
    return obj1;
};
*/

var MergeObjects = function (obj1, obj2) {

	// Handle the 3 simple types, and null or undefined
	if (null == obj2 || "object" != typeof obj2) return obj2;

	// Handle Date
	if (obj2 instanceof Date) {
		obj1 = new Date();
		obj1.setTime(obj2.getTime());
		return obj1;
	}

	// Handle Array
	if (obj2 instanceof Array) {
		if(!(obj1 instanceof Array) || obj1 == null)
			obj1 = [];
		for (var i = 0, len = obj2.length; i < len; i++) {
			obj1.push(CopyObject(obj2[i]))
		}
		return obj1;
	}

	// Handle Object
	if (obj2 instanceof Object) {
		if(!(obj1 instanceof Object) || obj1 == null)
			obj1 = {};
		for (var attr in obj2) {
			if (obj2.hasOwnProperty(attr)) obj1[attr] = CopyObject(obj2[attr]);
		}
		return obj1;
	}

	throw new Error("Unable to merge objs! Its type isn't supported.");
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

var ExistsPropInObj = function (obj, searchName) {
    var exists = false;
    for (var attrname in obj) {
        if (attrname == searchName)
            exists = false;
    }
    return exists;
};

function getElemPos(el) {
    // yay readability
    for (var lx = 0, ly = 0;
        el != null;
        lx += el.offsetLeft, ly += el.offsetTop, el = el.offsetParent);
    return { x: lx, y: ly };
}

function ValidateContact(contactType, contact) {
    /*
    1	TELEFONE
    2	TELEMOVEL
    3	FAX
    4	EMAIL
    5	FACEBOOK
    6	LINKEDIN
    7	TWITTER
    8	SKYPE
    9	WEBSITE
    10	MORADA
    */

    switch (contactType) {
        //telefone, telemóvel, fax
        case 1: case 2: case 3:
            var value = contact;
            value = value.replace(/\(/g, "").replace(/\)/g, "").replace(/\-/g, "").replace(/\ /g, "").trim();

            if (value * 1 == 0 || isStringEmpty(value)) {
                return false;
            }

            //valida 4 ou 5 numeros
            var pattern = new RegExp(/^[0-9]{4,5}/);
            var valid = pattern.test(value);

            if (valid) return true;


            //valida 9 numeros
            var pattern = new RegExp(/^[0-9]{9}/);
            var valid = pattern.test(value);

            if (valid) return true;

            var numerico = value.replace(/\(/g, "").replace(/\)/g, "").replace(/\-/g, "").replace(/\+/g, "").replace(/\ /g, "").trim();

            //valida 12 numeros e +
            pattern = new RegExp(/^[0-9]{11,12}/);
            valid = pattern.test(numerico);

            if (valid) {
                var regex = new RegExp(/\+/g)
                var count = contact.match(regex).length;
                return count == 1 && contact[0] == '+';
            }

            //valida 12 numeros
            var pattern = new RegExp(/^[0-9]{11,12}/);
            var valid = pattern.test(value);

            if (valid) return true;

            return false;
            break;

        //email
        case 4:
            //var pattern = new RegExp(/^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/); //inicialmente
            var pattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i; //a validar unicode
            //var pattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return pattern.test(contact);
            break;

        default:
            //morada
            return !isStringEmpty(contact);
            break;
    }
    return true;
}

function GetLatitude(coordinatesString) {
    if (!isStringEmpty(coordinatesString)) {
        var coordinates = coordinatesString.split(",");

        try {
            return $.trim(coordinates[0]);
        }
        catch (ex) { }
    }
    return 38.7125952;
}

function GetLongitude(coordinatesString) {
    if (!isStringEmpty(coordinatesString)) {
        var coordinates = coordinatesString.split(",");

        try {
            return $.trim(coordinates[1]);
        }
        catch (ex) { }
    }
    return -9.156137;
}

function FireDefaultButton(event, target) {

    var element = event.target || event.srcElement; // srcElement is for IE
    
    if (13 == event.keyCode && !(element && "textarea" == element.tagName.toLowerCase())) {
        var container = event.currentTarget || $(element).parent();
        var defaultButton = exists($(container)) ? $(container).find("#" + target) : document.getElementById(target);

        if (defaultButton && "undefined" != typeof defaultButton.click) {
            defaultButton.click();
            event.cancelBubble = true;
            if (event.stopPropagation)
                event.stopPropagation();
            return false;
        }
    }
    return true;
}

function downloadFileByByteArray(objArray, filename) {
    var arr = objArray;
    var byteArray = new Uint8Array(arr);

    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(new Blob([byteArray], { type: 'application/octet-stream' }));
    var fileName = filename;
    link.download = fileName;
    link.click();
}

function retifyInputsIds(areaId)
{
    //<input class="nameHidden" id="FederatedData_Name" name="FederatedData.Name" type="hidden" value="">
    try {
        $("#" + areaId + " :input[name][id]").each(function () {
            if (!isStringEmpty($(this).attr("id")) && !isStringEmpty($(this).attr("name")) && $("#" + $(this).attr("id")).indexOf() > 1/* && $("#" + $(this).attr("id")).length > 1*/)
                $(this).attr("id", areaId + "_" + replaceAll($(this).attr("name"), ".", "_"));
        });
    }
    catch(er){}
}

function getIconFontList() {
    var iconsList = [];
    try {
        var files = $.map(document.styleSheets, function (s) {
            if (s.href && s.href.endsWith('styles.css'))
                return s;
            return null;
        });

        if (exists(files)) {
            var rules = exists(files[0].rule) ? files[0].rule : files[0].cssRules;
            iconsList = $.map(rules, function (r) {
                try {
                    if (r.cssText.indexOf('.icon-') >= 0 && r.cssText.indexOf(':before { content: ') > 0)
                        return r.cssText.substring(1, r.cssText.indexOf(':'));
                    return null;
                }
                catch (err) {
                    return null;
                }
            });
        }
    }
    catch (err) { }
    return iconsList;
};
