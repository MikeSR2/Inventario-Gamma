$(document).ready(function () {
    // Interceptamos el evento submit
    $('#formulario').submit(function () {
        // Enviamos el formulario usando AJAX
        runWaitMe();
        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            // Mostramos un mensaje con la respuesta de PHP
            success: function (data) {
                $('#mensaje').html("<button type='button' class='close' onClick='ocultaAlert()' >&times;</button>" +
                    'Alta Exitosa');
                $('#mensaje').addClass("alert alert-dismissible alert-success");
                $('#mensaje').css("display", "block");
                document.getElementById("formulario").reset();
                $('#contenedor').waitMe('hide');
            },
            error: function (data) {
                $('#mensaje').html("<button type='button' class='close' onClick='ocultaAlert()' >&times;</button>" +
                     data.responseText);
                $('#mensaje').addClass("alert alert-dismissible alert-danger");
                $('#mensaje').css("display", "block");
                $('#contenedor').waitMe('hide');
            }
        })
        
        return false;
    });
});

//Función para ocultar el alert de resultado de la alta
function ocultaAlert() {
    $('#mensaje').css("display", "none");
}

//Función para mostrar div de carga al dar de alta
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