$(document).ready(function () {
    $('#DeptDatatable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/User",
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
            { "data": "firstName", "name": "FirstName"   , "autowidth": true },
            { "data": "lastName", "name": "LastName"    , "autowidth": true },
            { "data": "fullName", "name": "FullName"     , "autowidth": true },
            { "data": "salay", "name": "Salay"       , "autowidth": true },
            { "data": "managerFullName", "name": "managerFullName"       , "autowidth": true },
            {
                "data": "id",
                "name": "Id",
                render: function (data, type, row) {
                    return '<a href="javascript:" onclick="OpenImage(\'/images/' + row.imagePath + '\');"><img style="width:100px;" src="/images/' + row.imagePath + '" /></a>';
                },
                "orderable": false
            },
            {
                "render": function (data, type, row)
                {
                    return `<div class="d-flex justify-content-center"> <a class="btn btn-danger mx-2" onclick="location.href='/Employee/Delete/${row.id}'"; ><i class="fa-solid fa-trash"></i> </a > <a  onclick="location.href='/Employee/Edit/${row.id}'"; class="btn btn-primary mx-2"> <i class="fa-solid fa-pen-to-square"></i> </a ><a class="btn btn-info mx-2" onclick="location.href='/Employee/Details/${row.id}'"; ><i class="fa fa-info-circle"></i></a ></div>`
                },
                "orderable": false
            }

        ]
    });
});

function OpenImage(src) {
    $('#img').attr('src', src)
    $('#ImageModal').modal('show');
}
