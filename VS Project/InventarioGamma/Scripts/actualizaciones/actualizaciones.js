$(document).ready(function () {
    $('.selectpicker').selectpicker();
    // Interceptamos el evento submit
    $('#formMod').submit(function () {
        // Enviamos el formulario usando AJAX
        runWaitMe("#formMod");
        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            // Mostramos un mensaje con la respuesta de PHP
            success: function (data) {
                swal("Actualizado!", data.responseText, "success");
                $('#formMod').waitMe('hide');
                ocultarModal('#modalMod');
                $("#modalMod").html("");
                table.destroy();
                crearDataTable();
            },
            error: function (data) {
                swal("Error!", data.responseText, "error");
                $('#formMod').waitMe('hide');
            }
        })

        return false;
    });
});

//confirmar eliminacion
function confirmarEliminar() {
    swal({
        title: "¿Está seguro?",
        text: "Se eliminará el producto seleccionado!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Eliminar!",
        closeOnConfirm: false
    },
    function(){
    eliminar();
        }
    );
}

//Funcion para eliminar
function eliminar() {
    var idProducto = $("#inId").val();
    var dataJson = { IdProducto: idProducto };
    runWaitMe("#formMod");
    $.ajax({
        type: 'POST',
        url: '/Actualizaciones/Eliminar',
        data: JSON.stringify(dataJson),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#formMod').waitMe('hide');
            swal("Eliminado!", data.mensaje, "success");
            ocultarModal('#modalMod');
            $("#modalMod").html("");
            table.destroy();
            crearDataTable();
        },
        error: function (data) {
            $("#formMod").waitMe('hide');
            swal("Error!", data.mensaje, "error");
        }
    });
}

//Función para ocultar el alert de resultado de la alta
function ocultaAlert() {
    $('#mensaje').css("display", "none");
}

//Función para mostrar div de carga al dar de alta
function runWaitMe(div) {
    $(div).waitMe({
        effect: 'roundBounce',
        text: 'Procesando registro, por favor espere...',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000',
        sizeW: '',
        sizeH: '',
        source: '/Content/img/img.svg'
    });
}