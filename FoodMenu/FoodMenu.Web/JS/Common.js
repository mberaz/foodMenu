

var common = {};

common = function () {
    var pub = {};
    var self = {};


    pub.initLoadingAnimation = function (run) {
        $(document).ajaxStart(function (setting) {
            self.showLoadingAnimation($('body'))
        }).ajaxStop(function (setting) {
            self.hideLoadingAnimation($('body'));
        }).ajaxSend(function (evt, request, settings) {

        });
    };

    self.showLoadingAnimation = function (bodyElement) {
        $(".dataTables_processing").css("visibility", "hidden");
        $.blockUI({
            message: '<img src="/content/images/loading.gif" />',
            css: { width: '4%', border: '0px solid transparent', cursor: 'wait', backgroundColor: 'transparent', top: '15%' },
            overlayCSS: { backgroundColor: '#FFFFFF', opacity: 0.0, cursor: 'wait' }
        });
    };

    self.hideLoadingAnimation = function (bodyElement) {
        $.unblockUI();
        $(".dataTables_processing").css("visibility", "bisible");
    };


    pub.showModel = function (url, data,title, onLoad ) {
        $.post(url, data, function (html) {
            $(".modal-title").html(title);
            $(".modal-body").html(html);

            $("#myModal").modal();

            if (onLoad) {
                onLoad();
            }
        });
    }
    return pub;

}();