var imgCropSave = (function () {

    var cropitObj; var widthValue = 0, heightValue = 0;

    setPlugin = function (img, w, h) {
        cropitObj = $('.image-cropper');
        widthValue = w ? w : 700;
        heightValue = h ? h : 258;

        $(cropitObj).cropit({
            imageBackground: true,
            imageBackgroundBorderWidth: 30, // Width of background border
            //imageBackgroundBorderSize: 150,
            smallImage: 'allow',
            allowCrossOrigin: true,
            width: widthValue,
            height: heightValue,
            imageState: {
                src: img, // renders an image by default
            },
            initialZoom: 'middle',
            minZoom: 'fit',
            onZoomEnabled: enableEvent.bind(cropitObj),
            onZoomDisabled: disableEvent.bind(cropitObj),
            onImageError: showErrorMsg.bind(cropitObj.find(".cropit-image-preview"))
        });
        $(cropitObj).cropit('previewSize', { width: widthValue, height: heightValue });

        if (exists($(cropitObj).find(".croper-select-image-btn")))
            $($(cropitObj).find(".croper-select-image-btn")).unbind("click").click(function () { selectEvent(); });

        if (exists($(cropitObj).find(".croper-download-btn")))
            $($(cropitObj).find(".croper-download-btn")).unbind("click").click(function () { saveImg(); });

        if (!exists($(cropitObj).find(".slider-wrapper input[type='range']")) && exists($(cropitObj).find(".slider-wrapper div.zoomSlider"))) {
            try {
                var zoomSlider = $(cropitObj).find(".slider-wrapper div.zoomSlider");
                $(zoomSlider).slider({
                    orientation: "horizontal",
                    range: "min",
                    max: widthValue * 2,
                    value: widthValue,
                    slide: refreshZoom,
                    change: refreshZoom
                });

                $(zoomSlider).slider("value", widthValue);

                if (exists($(".slider-wrapper .zoomInputBox"))) {
                    $(".slider-wrapper .zoomInputBox").val(widthValue);

                    $(".slider-wrapper .zoomInputBox").unbind("change").change(function (e) {
                        $(zoomSlider).slider("value", $(this).val());
                        refreshZoom();
                    });

                    var maxNumber = widthValue * 2; var minNumber = 0;
                    $(".slider-wrapper .zoomInputBox").unbind("keypress keyup").on("keypress keyup", function (e) {
                        var val = $(this).val();
                        var code = e.charCode || e.keyCode;
                        var charCode = (e.which) ? e.which : code;
                        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                            return false;
                        }
                        else if (val > maxNumber)
                            $(this).val(maxNumber);
                        else if (val < minNumber)
                            $(this).val(minNumber);

                        setTimeout(function () {
                            $(".slider-wrapper .zoomInputBox").trigger("change");
                        }, 50);

                    });
                }
            } catch (error) { }
        }
    };

    function refreshZoom() {
        var value = $($(cropitObj).find(".slider-wrapper div.zoomSlider")).slider("value");
        if (exists(value)) {
            var ratioValue = (value / widthValue);
            $(cropitObj).cropit('zoom', ratioValue);

            if (exists($(".slider-wrapper .zoomInputBox")))
                $(".slider-wrapper .zoomInputBox").val(value);
        }
    }

    selectEvent = function () {
        return cropitObj.find("input.cropit-image-input").trigger("click");
    };

    saveImg = function (openNewWindows, extensao) {
        var extensaoFinal = "image/" + extensao;
        extensaoFinal = extensaoFinal != 'image/png' && extensaoFinal != 'image/jpeg' && extensaoFinal != 'image/gif' ? 'image/jpeg' : extensaoFinal;
        var saveImg = cropitObj.cropit("export", {
            type: extensaoFinal,
            quality: 1,
            originalSize: true
        });
        if (openNewWindows)
            window.open(saveImg);
        else
            return saveImg;
    }
    function enableEvent() {
        return cropitObj.find(".slider-wrapper").removeClass("disabled");
    }
    function disableEvent() {
        return cropitObj.find(".slider-wrapper").addClass("disabled");
    }
    function showErrorMsg(a) {
        return 1 === a.code ? (cropitObj.find(".error-msg").text("Please use an image that's at least " + cropitObj.outerWidth() + "px in width and " + cropitObj.outerHeight() + "px in height."), cropitObj.addClass("has-error"), window.setTimeout(function (a) {
            return function () {
                return a.removeClass("has-error")
            }
        }(cropitObj), 3e3)) : void 0;
    }

    return {
        initialize: function (img, w, h) {
            setPlugin(img, w, h);
        },
        getFinalImage: function (extensao) {
            return saveImg(false, extensao);
        },
        selectImage: function () {
            return selectEvent();
        },
        setImage: function (imageSrc) {
            cropitObj.cropit('imageSrc', imageSrc);
        }
    }
})();
