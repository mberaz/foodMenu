$(function () {

    $("#PasswordCnfrm").remove();
    var logoFileUrl=$("#userLogo").attr('data-src');
    $("#userLogo").attr('src', logoFileUrl);
    $("#userLogo").show();

    var userId = $("#id").val();

    $("#fileForm").ajaxForm(function () {
        $("#userLogo").remove();
        $("#userform").after('<img src="' + logoFileUrl +'?' + Math.random()+ '"   id="userLogo"/>');

    });

    webApi.ajax("User/" + userId, { type: "GET" }).done(function (result) {
        if (result.Status) {
            common.loadObject($("#userform"), result.Result);
        }
        else {
            bootbox.alert(result.Error);
        }

    });

    $(document).on('click', "#save-new-user", function () {

        var form = $("#userform");
        if (!common.validate(form)) {
            return;
        }

        var obj = common.toObject(form);
        webApi.ajax("User/" + userId, { type: "PUT", data: ko.toJSON(obj)}).done(function (result) {
            if (result.Status) {
                if ($("#logoFile").val() != "")
                {
                    var fileForm = $("#fileForm");
                    $("#fileForm").attr('action', fileForm.attr('data-action') + userId);
                   
                    $("#fileForm").submit();
                }
              

                toastr.info("פרטי המשתמש עודכנו");
            }
            else {
                bootbox.alert(result.Error);
            }

        });
    });


});