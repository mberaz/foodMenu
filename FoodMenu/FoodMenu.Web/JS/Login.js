$(function () {

    var token = {};
    $(document).on('click', "#register", function () {
        common.showModel("/users/edit", {}, "משתמש חדש", function () {
            $("#fileForm").ajaxForm(function () {
                Cookies.set('token', token );
                window.location = "/";
            });
        });
    });

    $(document).on('click', "#login", function () {
        var obj = common.toObject($(".form-signin"));

        webApi.ajax("Account/Login", { type: "POST", data: ko.toJSON(obj), allowAnon: true }).done(function (result) {
            if (result.Status) {
                token = result.Result.Id + "~" + result.Result.Token;
                Cookies.set('token', token);
                window.location = "/";
            }
            else {
                bootbox.alert(result.Error);
            }

        });

    });


    $(document).on('click', "#save-new-user", function () {
        var form = $("#userform");
        if (!common.validate(form)) {
            return;
        }

        var passwordMatch = $("#PasswordCnfrm").val() === $("#Password").val();
        common.validateField(passwordMatch, $("#PasswordCnfrm"), "הסיסמה לא תואמת");
        common.validateField(passwordMatch, $("#Password"), "הסיסמה לא תואמת");


        common.validateField($("#logoFile").val() !== '', $("#logoFile"), "לא נבחר קובץ תמונה");

        if (!passwordMatch || $("#logoFile").val() == '') {
            return;
        }

        var obj = common.toObject(form);
        webApi.ajax("User", { type: "POST", data: ko.toJSON(obj), allowAnon: true }).done(function (result) {
            if (result.Status) {
                var fileForm = $("#fileForm");
                $("#fileForm").attr('action', fileForm.attr('data-action') + result.Result.Id);
                token = result.Result.Id + "~" + result.Result.Token;
                $("#fileForm").submit();
            }
            else {
                bootbox.alert(result.Error);
            }

        });

    });

});
