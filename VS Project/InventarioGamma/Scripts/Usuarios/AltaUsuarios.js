function ocultaAlert() {
    $('#mensaje').css("display", "none");
}

$(document).ready(function () {
    $('#AdminUser').submit(function () {
        var p1 = $("#Password").val();
        var p2 = $("#ConfirmPass").val();

        if (p1 != p2) {
            $('#mensaje').html("<button type='button' class='close' data-dismiss='alert'>&times;</button>" +
                  "Las contraseñas deben de coincidir verifique que sean iguales");
            $('#mensaje').addClass("alert alert-dismissible alert-danger");
            $('#mensaje').css("display", "block");
            return false;
        }
        runWaitMe();
        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            success: function (data) {
                $('#mensaje').html("<button type='button' class='close' onClick='ocultaAlert()' >&times;</button>" +
                    data);
                $('#mensaje').addClass("alert alert-dismissible alert-success");
                $('#mensaje').css("display", "block");
                document.getElementById("AdminUser").reset();
                $('#contenedor').waitMe('hide');
            },
            error: function (data) {
                $('#mensaje').html("<button type='button' class='close' data-dismiss='alert'>&times;</button>" +
                    data);
                $('#mensaje').addClass("alert alert-dismissible alert-danger");
                $('#mensaje').css("display", "block");
                $('#contenedor').waitMe('hide');
            }
        })
        return false;
    });
});

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