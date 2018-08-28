var ImagePreview = (function () {
    var optionsPreview = {
        img: $(".fotoImg"),
        selfBtsCreation: true,
        withSlider: true,
        rotateLeftBt: $(".btRotateLeft"),
        rotateLeftLabel: "Rotate left",
        rotateRightBt: $(".btRotateRight"),
        rotateRightLabel: "Rotate right",
        zoomLabel: "Zoom: ",
        zoomSlider: $(".zoomSlider"),
        zoomInBt: $('.btZoomIn'),
        zoomOutBt: $('.btZoomIOut'),
        maxWidth: 748,
        width: 350
    };

    //exemplo de imagem
    //<img id="image_canv" src="/image.png" data-rotate="90">
    
    
    function initializePreview(options) {

        try
        {
            applyVariables(options);

            if (optionsPreview.selfBtsCreation)
                createBtsBar();

            //rotate image left
            if (exists(optionsPreview.rotateLeftBt))
                $(optionsPreview.rotateLeftBt).unbind("click").click(function () {
                    var deg = $(optionsPreview.img).data('rotate') || 0;

                    if (deg == 0) deg = -90;
                    else deg = deg - 90 <= -360 ? 0 : deg - 90

                    $(optionsPreview.img).data('rotate', deg);

                    var rotate = 'rotate(' + deg + 'deg)';
                    $(optionsPreview.img).css({
                        '-webkit-transform': rotate,
                        '-moz-transform': rotate,
                        '-o-transform': rotate,
                        '-ms-transform': rotate,
                        'transform': rotate
                    });

                    if (deg == 90 || deg == 270) {
                        var width = $(optionsPreview.img).width();
                        $($(optionsPreview.img).parent().find(".tdImgPreview")).css("height", width + "px");
                    }
                    else
                        $($(optionsPreview.img).parent().find(".tdImgPreview")).css("height", "auto");
                    return false;
                });


            //rotate image right
            if (exists(optionsPreview.rotateRightBt))
                $(optionsPreview.rotateRightBt).unbind("click").click(function () {
                    var deg = $(optionsPreview.img).data('rotate') || 0;

                    if (deg == 0) deg = 90;
                    else deg = deg + 90 == 360 ? 0 : deg + 90

                    $(optionsPreview.img).data('rotate', deg);

                    var rotate = 'rotate(' + deg + 'deg)';
                    $(optionsPreview.img).css({
                        '-webkit-transform': rotate,
                        '-moz-transform': rotate,
                        '-o-transform': rotate,
                        '-ms-transform': rotate,
                        'transform': rotate
                    });

                    if (deg == 90 || deg == 270) 
                    {
                        var width = $(optionsPreview.img).width();
                        $(optionsPreview.img).parent().css("height", width + "px");
                    }
                    else
                        $(optionsPreview.img).parent().css("height", "auto");

                    return false;
                });


            defineZoomBehaviour();
        }
        catch(err) { }
    }

    function applyVariables(options)
    {
        if (exists(options)) {
            if (exists(options.img))
                optionsPreview.img = options.img;

            if (exists(options.rotateLeftBt))
                optionsPreview.rotateLeftBt = options.rotateLeftBt;

            if (exists(options.rotateRightBt))
                optionsPreview.rotateRightBt = options.rotateRightBt;

            if (exists(options.zoomSlider))
                optionsPreview.zoomSlider = options.zoomSlider;

            if (exists(options.zoomInBt))
                optionsPreview.zoomInBt = options.zoomInBt;

            if (exists(options.zoomOutBt))
                optionsPreview.zoomOutBt = options.zoomOutBt;

            if (exists(options.selfBtsCreation))
                optionsPreview.selfBtsCreation = options.selfBtsCreation;

            if (!isStringEmpty(options.withSlider))
                optionsPreview.withSlider = options.withSlider;

            if (!isStringEmpty(options.maxWidth))
                optionsPreview.maxWidth = options.maxWidth;

            if (!isStringEmpty(options.width))
                optionsPreview.width = options.width; 

            if (!isStringEmpty(options.zoomLabel))
                optionsPreview.zoomLabel = options.zoomLabel;
            
            if (!isStringEmpty(options.rotateLeftLabel))
                optionsPreview.rotateLeftLabel = options.rotateLeftLabel;

            if (!isStringEmpty(options.rotateRightLabel))
                optionsPreview.rotateRightLabel = options.rotateRightLabel;
        }
    }

    function defineZoomBehaviour()
    {
        //zoom images with slider
        if (optionsPreview.withSlider) {
            $(optionsPreview.zoomSlider).slider({
                orientation: "horizontal",
                range: "min",
                max: optionsPreview.maxWidth,
                value: optionsPreview.width,
                slide: refreshZoom,
                change: refreshZoom
            });
            
            $(optionsPreview.zoomSlider).slider("value", optionsPreview.width);
        }
        else {
            //zoom in image
            $(optionsPreview.zoomInBt).unbind("click").click(function () {
                $(optionsPreview.img).width($(optionsPreview.img).width() * 1.2);
                return false;
            });


            //zoom out image
            $(optionsPreview.zoomOutBt).unbind("click").click(function () {
                $(optionsPreview.img).width($(optionsPreview.img).width() * 0.5);
                return false;
            });
        }
    }

    function refreshZoom() {
        var value = $(optionsPreview.zoomSlider).slider("value");
        $(optionsPreview.img).width(value);
    }

    function createBtsBar()
    {
        //cria tabela
        $(optionsPreview.img).parent().append("<table class='tablePreview'></table>");
        var table = $(optionsPreview.img).parent().find(".tablePreview");
        $(table).append("<tr><td class='tdImgPreview'></td></tr>");
        $(table).append("<tr><td class='tdBarPreview'></td></tr>");


        //coloca imagem na tabela
        $($(table).find(".tdImgPreview")).append($(optionsPreview.img));

        //cria barra de botões e seus botões
        $($(table).find(".tdBarPreview")).append("<div class='previewBarBts'></div>");
        
        var bar = $(table).find("div.previewBarBts");
        $(bar).append("<div class='previewZoomBts'></div><div class='previewRotateBts'></div>");
        
        if (optionsPreview.withSlider) {
            $($(bar).find(".previewZoomBts")).append("<span class='labelZoomPreview'>" + optionsPreview.zoomLabel + "</span>");
            $($(bar).find(".previewZoomBts")).append("<div class='zoomSlider'></div>");
            optionsPreview.zoomSlider = $(bar).find(".previewZoomBts .zoomSlider")
        }
        else {
            var zoomBtsArea = $($(bar).find(".previewZoomBts"));
            $(zoomBtsArea).append("<button class='btZoomIn AddButton'>Zoom in</button>");
            optionsPreview.zoomInBt = $(bar).find(".previewZoomBts .btZoomIn");

            $(zoomBtsArea).append("<button class='btZoomIOut AddButton'>Zoom out</button>");
            optionsPreview.zoomOutBt = $(bar).find(".previewZoomBts .btZoomIOut");
        }


        var rotateBtsArea = $($(bar).find(".previewRotateBts"));
        $(rotateBtsArea).append("<input type='button' class='btRotateLeft AddButton' value='" + optionsPreview.rotateLeftLabel + "'/>");
        optionsPreview.rotateLeftBt = $(bar).find(".previewRotateBts .btRotateLeft");

        $(rotateBtsArea).append("<input type='button' class='btRotateRight AddButton' value='" + optionsPreview.rotateRightLabel + "'/>");
        optionsPreview.rotateRightBt = $(bar).find(".previewRotateBts .btRotateRight");
    }


return {
    initializePreview: function (options) {
        initializePreview(options);
    }
}
})();