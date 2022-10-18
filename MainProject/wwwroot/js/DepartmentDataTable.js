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
            { "data": "imagePath", "name": "Image"       , "autowidth": true },
            {
                "render": function (data, type, row)
                {
                    return `<div class="d-flex justify-content-center">
                            <a href="#"
                               class="btn btn-danger mx-2"
                               onclick=DeleteCustomer("' + row.id + '"); >
                               <i class="fa-solid fa-trash"></i>
                            </a >

                            <a href="#"
                               class="btn btn-primary mx-2"
                               onclick=DeleteCustomer("' + row.id + '"); >
                                <i class="fa-solid fa-pen-to-square"></i>
                            </a >
                        </div>`
                },
                "orderable": false
            },

        ]
    });
});