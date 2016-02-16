$(function () {

    $("#editform [name='Weight'], #editform [name='FatPercentage']").change(function () {
        $("#meetingForm [name='Weight']").val($("#editform [name='Weight']").val());
        $("#meetingForm [name='FatPercentage']").val($("#editform [name='FatPercentage']").val());
    });


    $(document).on('click', "#save-client", function () { });

});