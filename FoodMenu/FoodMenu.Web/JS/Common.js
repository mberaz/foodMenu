

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


    pub.showModel = function (url, data, title, onLoad) {
        $.post(url, data, function (html) {
            $(".modal-title").html(title);
            $(".modal-body").html(html);

            $("#myModal").modal();

            if (onLoad) {
                onLoad();
            }
        });
    }

    pub.validate = function (form) {
        var inputs = form.find('input, select');
        var isValid = true;
        inputs.each(function () {
            var inp = $(this);

            var fieldValid = true;
            var errormsg = "";
            if (inp.hasClass("required") && inp.val().trim() === '')
            {
                fieldValid=false;
                errormsg = 'שדה חובה';
            }

            if (inp.hasClass("email-field") && !pub.validateEmail(inp.val().trim())) {
                fieldValid=false;
                errormsg = 'לא אמיל תקין';
            }

            pub.validateField(fieldValid, inp, errormsg);
        });

       

        return isValid;
    }

    pub.validateField = function (isValid, inp, errormsg) {
        if (!(isValid)) {
            var msg = inp.closest(".form-group").find('.error_msg');
            if (msg.length == 0) {
                inp.after($("<p class='error_msg'>" + errormsg + " </p>"));
            }

            inp.addClass("error");
            isValid = false;
        }
        else {
            inp.closest(".form-group").find('.error_msg').remove();
            inp.removeClass("error");
        }
    }

    pub.validateEmail=function(email) {
        var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }


    pub.toObject = function (form) {
        var inputs = form.find('input, select');
        var obj = {};
        inputs.each(function () {
            var inp = $(this);

            obj[inp.attr("name")] = inp.val();
        });


        return obj;
    }

    return pub;

}();