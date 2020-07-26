$(document).ready(function() {
    mostrarHora();
});

function mostrarHora() {
    //Array con lista de meses del año
    var meses = new Array ("Enero","Febrero","Marzo","Abril","Mayo",
        "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");

    var fecha = new Date();
    var mins = fecha.getMinutes();
    var hrs = fecha.getHours();

    if (hrs < 10) {
        hrs = '0' + hrs;
    }

    if (mins < 10) {
        mins = '0' + mins;
    }
    //Pinta hora
    document.getElementById('reloj').innerHTML = hrs + ' : ' + mins + ' - ' +
        fecha.getDate() + " de " + meses[fecha.getMonth()] + " de " + fecha.getFullYear();

    //Actualiza reloj
    setTimeout('mostrarHora()', 1000);
}

