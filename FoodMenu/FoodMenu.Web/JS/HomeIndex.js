var oTable = {};
$(function () {
    //webApi.ajax("User", { type: "GET", data: {}, allowAnon: true }).done(function (result) {


    //});

    oTable = $('#usertable').dataTable({
        "iDisplayLength": 20,
        "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": common.baseURl + "/User"
        , "fnServerData": function (sSource, aoData, fnCallback) {
            var searchParams = "";// "?typeUser=" + type + "&CompletedShow=" + showAll;
            $.ajax({
                "dataType": 'json',
                "type": "GET",
                "url": sSource + searchParams,
                "data": aoData,
                "success": fnCallback
            });
        },
        "fnDrawCallback": function (oSettings) {
            

        }

    });
})