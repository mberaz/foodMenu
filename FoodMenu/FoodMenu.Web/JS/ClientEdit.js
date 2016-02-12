$(function () {

    var userId = $("#id").val();


    webApi.ajax("Client/" + userId, { type: "GET" }).done(function (result) {
        if (result.Status) {
            common.loadObject($("#editform"), result.Result);
            //$("[name='Sex'][value='" + result.Result.Sex + "']").check();
        }
        else {
            bootbox.alert(result.Error);
        }

    });

    $(document).on('click', "#save-client", function () {

        var form = $("#editform");
        if (!common.validate(form)) {
            return;
        }

        var obj = common.toObject(form);
        webApi.ajax("Client/" + userId, { type: "PUT", data: ko.toJSON(obj) }).done(function (result) {
            if (result.Status) {
               
                toastr.info("פרטי הלקוח עודכנו");
            }
            else {
                bootbox.alert(result.Error);
            }

        });
    });


});