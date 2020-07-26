$(document).ready(function () {
    
    $('#LoginForm').submit(function () {
        // Enviamos el formulario usando AJAX
        runWaitMe();
        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            // Mostramos un mensaje con la respuesta de ASP
            success: function (data) {
                $('#login').waitMe('hide');
                window.location.replace("/consultas");
            },
            error: function (data) {
                $('#msg').html("<button type='button' class='close' onClick='ocultaAlert()'>&times;</button>" +
                     data.responseText);
                $('#msg').addClass("alert alert-dismissible alert-danger");
                $('#msg').css("display", "block");
                $('#LoginForm').waitMe('hide');
            }
        })

        return false;
    });
    
});
//Función para ocultar el alert de resultado de la alta
function ocultaAlert() {
    
    $('#msg').css("display", "none");
}
function runWaitMe() {
    $('#LoginForm').waitMe({
        effect: 'win8',
        text: '',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000',
        sizeW: '',
        sizeH: '',
        source: '/Content/img/img.svg'
    });
}