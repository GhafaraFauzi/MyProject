var Table = null; //Untuk merefresh halaman
$(document).ready(function () {
    Table = $('#TB_Departemen').DataTable({
        "ajax":
        {
            url: "http://localhost:8082/api/Departemens",
            type: "GET",
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('token')
            },
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [

            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + 1; // Menampilkan kolom "No." dengan fungsi increment berdasarkan data "name" dari API 
                }
            },

            // Menambahkan kolom action dengan edit dan delete
            { "data": "nama_Departemen" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.id_Departemen + ')"><i class="fa fa-pen"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.id_Departemen + ')"><i class="fa fa-trash"></i></button >'
                }
            }
        ],
        //Default Order
        "order": [[1, "asc"]],
        "responsive": true,
        //Buat ngilangin order kolom No dan Action
        "columnDefs": [
            { "orderable": false, "targets": [0, 2] }
        ],
        //Agar nomor tidak berubah
        "drawCallback": function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            api.column(1, { page: 'current' }).data().each(function (group, i) {
                $(rows).eq(i).find('td:first').html(i + 1);
            });
        }
    });
});


//ClearScreen
function ClearScreen() {
    $('#id_Departemen').val('');
    $('#nama_Departemen').val('');
    $('#update').hide();
    $('#save').show();
}

//Cek Inputan
function checkInput() {
    var inputVal = $("nama_Departemen").val();
    var btn = $("#insertButton");
    var btnUpdate = $("#updateButton");
    var notif = $("#notif");

    if (inputVal == '') {
        btn.prop("disabled", true);
        btnUpdate.prop("disabled", true);
        notif.css("color", "red");
        notif.text("Nama tidak boleh kosong");
    } else {
        btn.prop("disabled", false);
        btnUpdate.prop("disabled", false);
        notif.text("");
    }
}

//GET BY ID
function GetById(id_Departemen) {
    //debugger;
    $.ajax({
        url: "http://localhost:8082/api/Departemens/" + id_Departemen,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //debugger;
            var obj = result.data;
            $('#id_Departemen').val(obj.id_Departemen);     //Data didapat dari API
            $('#nama_Departemen').val(obj.nama_Departemen);
            $('#myModal').modal('show');
            $('#notif').text("");
            checkInput()
            $('#save').hide();
            $('#update').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//ADD DATA
function Save() {
    var Departemen = new Object();  //penamaan objek
    Departemen.nama_Departemen = $('#nama_Departemen').val(); //value
    $.ajax({
        type: 'POST',
        url: 'http://localhost:8082/api/Departemens',
        data: JSON.stringify(Departemen), //untuk mengirim data yang di transform dalam pentuk JSON
        contentType: "application/json; charset=utf-8"
    }).then((result) => {
        debugger;
        if (result.status == result.status == 201 || result.status == 204 || result.status == 200) //allert bisa juga result.message
        {
            Swal.fire({
                icon: 'success',
                title: 'Berhasil',
                text: 'Data Berhasil Dimasukkan',    
                showconfirmButton: false,
                timer: 10500
            })
 
/*          alert("Data Berhasil Dimasukkan"); //tinggai disesuaikan*/
            Table.ajax.reload(); //Untuk merefresh halaman
        } else {
            alert("Data Tidak Berhasil Dimasukkan");
        }
    })
}

//DELETE DATA
function Delete(id_Departemen) {
    //debugger;
    Swal.fire({
        title: 'Apakah anda yakin?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya, hapus data'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "http://localhost:8082/api/Departemens/" + id_Departemen,
                type: "DELETE",
                dataType: "json",
            }).then((result) => {
                debugger;
                if (result.status == 200) {
                    Swal.fire(
                        'Data terhapus!',
                        'Data anda sudah terhapus.',
                        'success'
                    )
                    $('#TB_Departemen').DataTable().ajax.reload(); //Untuk merefresh halaman
                }
                else {
                    Swal.fire(
                        'Error!',
                        result.message,
                        'Terjadi error, data gagal dihapus'
                    )
                }
            });
        }
    })
}


//Cara 2
//function Delete(id_Departemen) {
//    event.preventDefault(); // prevent form submit
//    var form = event.target.form; // storing the form
//    //debugger;
//    $.ajax({
//        url: "http://localhost:8082/api/Departemens/" + id_Departemen,
//        type: "DELETE",
//        dataType: "json",
//    })
//        .then((result) => {
//        debugger;
//            if (result.status == 200) {
//                Swal.fire({

//                    title: 'Apakah anda yakin?',
//                    type: 'warning',
//                    showCancelButton: true,
//                    confirmButtonColor: '#3085d6',
//                    cancelButtonColor: '#d33',
//                    confirmButtonText: 'Ya, hapus data!'
//                }).then((result) => {
//                    if (result.value) {
//                        form.submit();
//                    }
//                })
//            $('#myModal').modal('hide');
//            /*alert(result.message);*/
//            $('#TB_Departemen').DataTable().ajax.reload(); //Untuk merefresh halaman
//        } else {
//            alert(result.message);
//        }
//    });
//}



//UPDATE DATA
function Update()
{
    var Departemen = new Object();
    Departemen.nama_Departemen = $('#nama_Departemen').val();
    Departemen.id_Departemen = $('#id_Departemen').val();

    $.ajax({
        url: 'http://localhost:8082/api/Departemens/Update',
        type: 'PUT',
        data: JSON.stringify(Departemen),
        contentType: "application/json; charset=utf-8",
    }).
        then((result) => {
            debugger;
            if (result.status == 200)
            {
                Swal.fire({
                    icon: 'success',
                    title: 'Berhasil',
                    text: 'Data Berhasil Diperbaharui',
                    showconfirmButton: true,
                    timer: 1500
                })
                $('#myModal').modal('hide'); //Untuk ngilangin form modal (insert data) setelah klik update
                /*alert("Data Berhasil Diperbaharui");*/
                $('#TB_Departemen').DataTable().ajax.reload(); //Untuk merefresh halaman
            }
            else
            {
                alert("Terjadi error, data gagal diperbaharui");
            }
     });
}