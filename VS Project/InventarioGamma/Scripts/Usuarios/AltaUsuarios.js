
var table;
$(document).ready(function () {
    crearDataTable();



    //mostrar modal para eliminar
    $('#tableUsuarios tbody').on('click', '#btnEliminar', function () {
        var data = table.row($(this).parents('tr')).data();
        confirmarEliminar(data['Usuario']);
    });

    //mostrar modal para reset
    $('#tableUsuarios tbody').on('click', '#btnReset', function () {
        var data = table.row($(this).parents('tr')).data();
        confirmarReset(data['Usuario']);
    });


    $('#AdminUser').submit(function () {

       
        var p1 = $("#Password").val();
        var p2 = $("#ConfirmPass").val();

        if (p1 !== p2) {
            swal("Contraseñas no coinciden, verifique y vuelva a intentarlo!", data.responseText, "error");
            return false;
        }
        runWaitMe('#AdminUser');
        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            success: function (data) {
                $('#AdminUser').waitMe('hide');
                swal("Alta realizada!", data.responseText, "success");
                ocultarModal("#modalAlta");
                table.destroy();
                crearDataTable();
                document.getElementById("AdminUser").reset();
            },
            error: function (data) {
                $('#AdminUser').waitMe('hide');
                swal("Error!", data.responseText, "error");
            }
        });
        return false;
    });
});

//crear datatable
function crearDataTable() {
    $('[data-toggle="tooltip"]').tooltip();
    //Crea data table
    table = $("#tableUsuarios").DataTable({
        "processing": true,
        "oLanguage": {
            "sSearch": "Buscar:",
            "sLengthMenu": "Mostrar   _MENU_  registros por página",
            "sZeroRecords": "No se encontró información para la búsqueda",
            "sInfo": "Mostrando la página número _PAGE_ de _PAGES_",
            "sInfoEmpty": "No se encontraron registros",
            "sInfoFiltered": "(filtered from _MAX_ total records)",
            "oPaginate": {
                "sFirst": "Primera página",
                "sLast": "Última página",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        },
        "sScrollX": true,
        "ajax": {
            "url": "Admin/ListaUsuarios",
            "type": "post",
            "dataSrc": "",
        },
        "columns": [
            { data: "Usuario" },
            { data: "Almacen" },
            {
                "data": null,
                "defaultContent": "<button data-toggle='tooltip' data-placement='left' title='Restablecer contraseña' id='btnReset' class='btn btn-primary btn-xs glyphicon glyphicon-pencil'></button>"
                    + "<button data-toggle='tooltip' data-placement='left' title='Eliminar' id='btnEliminar' class='btn btn-danger btn-xs glyphicon glyphicon-remove'></button>"
            }

        ]

    });

}


function ocultaAlert() {
    $('#mensaje').css("display", "none");
}

function runWaitMe() {
    $('#contenedor').waitMe({
        effect: 'roundBounce',
        text: 'Procesando registro, por favor espere...',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000',
        sizeW: '',
        sizeH: '',
        source: '/Content/img/img.svg'
    });
}


//confirmar eliminacion
function confirmarEliminar(usuario) {
    if (usuario == "admin") {
        swal("Error!", "El usuario administrador no se puede eliminar", "error");
        return;
    }
    swal({
        title: "¿Está seguro?",
        text: "Se eliminará el Usuario seleccionado!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Eliminar!",
        closeOnConfirm: false
    },
        function () {
            eliminar(usuario);
        }
    );
}

//Funcion para eliminar
function eliminar(usuario) {
    var dataJson = { Usuario: usuario };
    runWaitMe("#tableUsuarios");
    $.ajax({
        type: 'POST',
        url: 'Admin/Eliminar',
        data: JSON.stringify(dataJson),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#tableUsuarios').waitMe('hide');
            swal("Eliminado!", data.mensaje, "success");
            table.destroy();
            crearDataTable();
        },
        error: function (data) {
            $("#tableUsuarios").waitMe('hide');
            swal("Error!", data.mensaje, "error");
        }
    });
}



//confirmar eliminacion
function confirmarReset(usuario) {
    swal({
        title: "¿Está seguro?",
        text: "Se reestablecerá la cpntraseña del usuario!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Reestablecer!",
        closeOnConfirm: false
    },
        function () {
            resetPass(usuario);
        }
    );
}

//Funcion para eliminar
function resetPass(usuario) {
    var dataJson = { Usuario: usuario };
    //runWaitMe("#tableUsuarios");
    $.ajax({
        type: 'POST',
        url: 'Admin/Reset',
        data: JSON.stringify(dataJson),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
           // $('#tableUsuarios').waitMe('hide');
            swal("Contraseña reestablecida, ahora es el nombre de usuario, cambiarla una vez que se inicie sesión!", data.mensaje, "success");
            table.destroy();
            crearDataTable();
        },
        error: function (data) {
            //$("#tableUsuarios").waitMe('hide');
            swal("Error!", data.mensaje, "error");
        }
    });
}

function AgregarUsuario(){
    $("#modalAlta").modal();
}