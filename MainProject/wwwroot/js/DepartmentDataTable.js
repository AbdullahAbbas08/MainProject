$(document).ready(function () {
    $('#DeptDatatable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/Department",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id"          , "name": "Id"          , "autowidth": true },
            { "data": "name", "name": "Name"   , "autowidth": true },
            { "data": "employeeCount", "name": "EmployeeCount"    , "autowidth": true },
            { "data": "totalSalaies", "name": "TotalSalaies"     , "autowidth": true },
        ]
    });
});
