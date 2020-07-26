var tableHist;
$(document).ready(function () {
    //Date range picker
    $('#Periodo').daterangepicker(
        {
            'dateformat':'yyyy-MM-dd'
        });
    crearDataTable();
});

function updateDataTable() {
    if (tableHist === undefined) {
        return;
    }
    tableHist.destroy();
    crearDataTable();
}

function crearDataTable() {
    tableHist=$("#tableHistorial").DataTable({
        "bProcessing": true,
        "bServerSide": true,
        "sLanguage": {
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
        "sAjaxSource": "historial/LoadDataTable",
        "fnServerData": function (sSource, aoData, fnCallback) {
            /* Add some extra data to the sender */
            aoData.push({ "name": "Almacen", "value": $("#Almacen").val() });
            aoData.push({ "name": "Periodo", "value": $("#Periodo").val() });
            aoData.push({ "name": "InVentas", "value": $("#InVentas").is(':checked') });
            $.ajax( {
				"dataType": 'json', 
				"type": "POST", 
				"url": sSource, 
				"data": aoData, 
				"success": function (json) {
				    $('#total').html('<b>Total:</b> $' + json.Total);
                    fnCallback(json)
				}
			} );
        },
        "aoColumns": [
            { "data": "Clave" },
            { "data": "Producto" },
            { "data": "Descripcion" },
            { "data": "Fecha_Movimiento" },
            { "data": "Tipo_Movimiento" },
            { "data": "Origen" },
            { "data": "Destino" },
            { "data": "Cantidad" },
            { "data": "Cantidad_Anterior" },
            { "data": "Cantidad_Actual" }

        ]

    });
};