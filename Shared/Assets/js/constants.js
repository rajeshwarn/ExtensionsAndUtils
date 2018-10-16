var Constants = (function () {

    var resultOperation = {
        Error: -1,
        NoDataFound: 0,
        Success: 1,
        InvalidToken: 2,
        DuplicatedData: 3,
        ValidationFailed: 4,
        Warning: 5
    }
    //resultOperation.getCode = getById.bind('undefined', resultOperation, Object.keys(resultOperation));



    return {
        ResultEnum: resultOperation
    }
})();
