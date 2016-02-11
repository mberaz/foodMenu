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

        var redirect = window.location.search?window.location.search.replace("?returnUrl=",""):"";
        webApi.ajax("Account/Login", { type: "POST", data: ko.toJSON(obj), allowAnon: true }).done(function (result) {
            if (result.Status) {
                token = "{0}~{1}~{2}".format(result.Result.Id, result.Result.Token, result.Result.FirstName + " " + result.Result.LastName);
                Cookies.set('token', token);
                window.location = (redirect ? "/" : redirect);
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
                token = "{0}~{1}~{2}".format(result.Result.Id, result.Result.Token, result.Result.FirstName + " " + result.Result.LastName);
                $("#fileForm").submit();
            }
            else {
                bootbox.alert(result.Error);
            }

        });

    });

});
