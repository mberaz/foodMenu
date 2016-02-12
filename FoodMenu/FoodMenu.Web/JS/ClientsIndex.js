var oTable = {};
$(function () {

    oTable = $('#usertable').dataTable({
        "iDisplayLength": 20,
        "bProcessing": true,
        "bServerSide": true,
        "language": common.hebrewDataTable,
        "aoColumnDefs": [
            {
                "aTargets": [9],
                "bSortable": false,
                "bSearchable": false,
                "mRender": function (data, type, full) {
                    return '<a class="btn btn-info" href="/Clients/EditClient/' + data + '" >ערוך </a>';
                }
            }
        ],
        "sAjaxSource": common.baseURl + "Client/table"
        , "fnServerData": function (sSource, aoData, fnCallback) {
            var searchParams = "";// "?typeUser=" + type + "&CompletedShow=" + showAll;
            var headers= webApi.getHeaders();
            $.ajax({
                "dataType": 'json',
                "type": "GET",
                "url": sSource + searchParams,
                "data": aoData,
                "headers":headers,
                "success": fnCallback
            });
        },
        "fnDrawCallback": function (oSettings) {
            

        }

    });
})