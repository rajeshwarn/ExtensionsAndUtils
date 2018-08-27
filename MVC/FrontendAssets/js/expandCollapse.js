var expandableObj = (function () {

    $(document).ready(function () {
        initialize();
    });

    function initialize() {
        $("table tbody tr .expandableBt").click(function () { expandColapseRow($(this)); });
    }
    
    setClickEvents = function() {
        $("table tbody tr .expandableBt").each(function () { 
            if ($._data($(this)[0], 'events') == null)
                $(this).click(function () { expandColapseRow($(this)); });
        });
    }

    function expandColapseRow(item) {
        var tableId = $($(item).closest("table")).attr("id");
        var rowId = $($(item).closest("tr")).attr("id");

        if ($("#" + tableId + " tbody tr#s" + rowId).is(":visible") == false) {
            $("#" + tableId + " tbody tr#s" + rowId).show();
            $("#" + tableId + " tbody #" + rowId + ".iCollapsed .expandableBt.lessBt").show();
            $("#" + tableId + " tbody #" + rowId + ".iCollapsed .expandableBt.plusBt").hide();
            $("#" + tableId + " tbody #" + rowId + ".iCollapsed").addClass("iExpansable");
            $("#" + tableId + " tbody #" + rowId + ".iCollapsed").removeClass("iCollapsed");
        } else {
            $("#" + tableId + " tbody tr#s" + rowId).hide();
            $("#" + tableId + " tbody #" + rowId + ".iExpansable .expandableBt.lessBt").hide();
            $("#" + tableId + " tbody #" + rowId + ".iExpansable .expandableBt.plusBt").show();
            $("#" + tableId + " tbody #" + rowId + ".iExpansable").addClass("iCollapsed");
            $("#" + tableId + " tbody #" + rowId + ".iExpansable").removeClass("iExpansable");
        }
    }

    return {        
        setClickEvents: function() {
            setClickEvents();
        }}
})();