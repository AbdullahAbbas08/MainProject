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
            { "data": "sumSalary", "name": "SumSalary", "autowidth": true },
            {
                "render": function (data, type, row) {
                    return `<div class="d-flex justify-content-center"> <a class="btn btn-danger mx-2" onclick="location.href='/Department/Delete/${row.id}'"; ><i class="fa-solid fa-trash"></i> </a > <a  onclick="location.href='/Department/Edit/${row.id}'"; class="btn btn-primary mx-2"> <i class="fa-solid fa-pen-to-square"></i> </a ></div>`
                },
                "orderable": false
            }
        ]
    });
});
