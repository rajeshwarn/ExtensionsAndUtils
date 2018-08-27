$.validator.setDefaults({
    ignore: []
});

$(function () {

    $.validator.methods.date = function (value, element) {
        return this.optional(element) || Globalize.parseDate(value);
    };

    $.validator.methods.range = function (value, element, param) {
        var globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    };

    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    };



    //    $.validator.addMethod("DateGreaterThan", function (value, element, params) {
    //        if ($(params[0]).val() != '') {
    //            if (!/Invalid|NaN/.test(new Date(value))) {
    //                return new Date(value) > new Date($(params[0]).val());
    //            }
    //            return isNaN(value) && isNaN($(params[0]).val()) || (Number(value) > Number($(params[0]).val()));
    //        };
    //        return true;
    //    }, 'Must be greater than {1}.');
    //    

    //    $.validator.addMethod("endate_greater_startdate", function (value, element) {
    //        return enddate > startdate
    //    }, "* Enddate should be greater than Startdate");

    //    jQuery.validator.addMethod("lettersonly", function (value, element) {
    //        return this.optional(element) || /^[a-z ]+$/i.test(value);
    //    });


    $.validator.addMethod('TimeGreaterThan', function (value, element, param) {
        if (value != "" && $(param).val() != "") {
            var maxValue = value, maxTimeUnit, minValue = $(param).val(), minTimeUnit;
            var minItem = ($($(element).closest(".isTimeGroup")).find(param));

            minTimeUnit = $($($(minItem).closest(".isTimeGroup")).find(".isMinTimeUnit")).val();
            maxTimeUnit = $($($(element).closest(".isTimeGroup")).find(".isMaxTimeUnit")).val();

            if (minTimeUnit === undefined || maxTimeUnit === undefined || isStringEmpty(minTimeUnit) || isStringEmpty(maxTimeUnit))
                return true;

            var maxConverted = maxValue * convertTimeUnit(minTimeUnit, maxTimeUnit);

            return (maxConverted > minValue); //($.isNumeric(maxValue) && $.isNumeric(minValue) && maxValue > minValue);
        } else
            return true;
    }, 'O valor máximo deve ser maior que o valor mínimo');
    

    function convertTimeUnit(timeMin, timeMax) {
            switch (timeMin)
            {
                case "Year": //anos e o máximo tambem
                case "1":
                    return 1;
                case "Month": //meses
                case "2":
                    switch (timeMax)
                    {
                        case "Year":
                        case "1":
                            return 12;
                        default: return 1;
                    }
                case "Week": //meses
                case "3":
                    switch (timeMax) {
                        case "Year":
                        case "1":
                            return 52;
                        case "Month":
                        case "2":
                            return 4;
                        default: return 1;
                    }
                case "Day": //dias - 3
                case "4":
                    switch (timeMax)
                    {
                        case "Year":
                        case "1":
                            return 365;
                        case "Month":
                        case "2":
                            return 30;
                        case "Week":
                        case "3": 
                            return 7;
                        default: return 1;
                    }
            }
        
            return 0;
    }

    //    jQuery.validator.addMethod('lesserThan', function (value, element, param) {
    //        return (value < $(param).val());
    //    }, 'Must be less than end');
});