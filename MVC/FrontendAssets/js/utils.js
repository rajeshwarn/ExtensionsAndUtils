

function ValidaContacto(tipoContacto, contacto) {
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

    switch (tipoContacto) {
        //telefone, telemóvel, fax
        case 1: case 2: case 3:
            var value = contacto;
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
                var count = contacto.match(regex).length;
                return count == 1 && contacto[0] == '+';
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
            return pattern.test(contacto);
            break;

        default:
            //morada
            return !isStringEmpty(contacto);
            break;
    }
    return true;
}


function GetLatitude(coordenadasString) {
    if (!isStringEmpty(coordenadasString)) {
        var coordenadas = coordenadasString.split(",");

        try {
            return $.trim(coordenadas[0]);
        }
        catch (ex) { }
    }
    return 38.7125952;
}

function GetLongitude(coordenadasString) {
    if (!isStringEmpty(coordenadasString)) {
        var coordenadas = coordenadasString.split(",");

        try {
            return $.trim(coordenadas[1]);
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







