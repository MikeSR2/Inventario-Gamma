var table;
$(document).ready(function () {
    crearDataTable();
   
    //mostrar modal para modificar
    $('#tableConsulta tbody').on('click', '#btnMod', function () {
        var data = table.row($(this).parents('tr')).data();
        var idRow = $(this).parents('tr').attr('id');
        $("#modalMod").load("/Consultas/modificar", function () {
            $("#inId").val(idRow);
            $("#inClave").val(data['Clave']);
            $("#inNombre").val(data['Nombre']);
            $("#inMarca").val(data['Marca']);
            $("#inDescripcion").val(data['Descripcion']);
            $("#inPresentacion").val(data['Presentacion']);
            $("#inPrecio").val(data['Precio_Costo']);
            $("#inImporte").val(data['Importe_Inventario']);
            $("#inUbicacion").val(data['Ubicacion']);
            $("#inAlmacen").val(data['Almacen']).change();
        });
        $('.selectpicker').selectpicker();
        $("#modalMod").modal();
    });
    //mostrar modal para alta de inventario
    $('#tableConsulta tbody').on('click', '#btnAltaIn', function () {
        var data = table.row($(this).parents('tr')).data();
        var idRow = $(this).parents('tr').attr('id');
        $("#inIdA").val(idRow);
        $("#inClaveA").val(data['Clave']);
        $("#inNombreA").val(data['Nombre']);
        $("#modalAlta").modal();
    });
    //mostrar modal para transferencia
    $('#tableConsulta tbody').on('click', '#btnTransferencia', function () {
        var data = table.row($(this).parents('tr')).data();
        var idRow = $(this).parents('tr').attr('id');
        $("#inIdT").val(idRow);
        $("#inClaveT").val(data['Clave']);
        $("#inNombreT").val(data['Nombre']);
        $("#modalTransfer").modal();
    });
    //mostrar modal para agregar al carrito
    $('#tableConsulta tbody').on('click', '#btnVenta', function () {
        var data = table.row($(this).parents('tr')).data();
        var idRow = $(this).parents('tr').attr('id');
        $("#inIdV").val(idRow);
        $("#inClaveV").val(data['Clave']);
        $("#inNombreV").val(data['Nombre']);
        $("#modalAVenta").modal();
    });

    // Interceptamos el evento submit
    $('#formAlta').submit(function () {
        // Enviamos el formulario usando AJAX
        runWaitMe('#formAlta');
        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            // Mostramos un mensaje con la respuesta de PHP
            success: function (data) {
                $('#formAlta').waitMe('hide');
                swal("Actualizado!", data.responseText, "success");
                ocultarModal("#modalAlta");
                table.destroy();
                crearDataTable();
                document.getElementById("formAlta").reset();

            },
            error: function (data) {
                $('#formAlta').waitMe('hide');
                swal("Error!", data.responseText, "error");
            }
        })

        return false;
    });

    // Interceptamos el evento submit
    $('#formTransfer').submit(function () {
        // Enviamos el formulario usando AJAX
        runWaitMe('#formTransfer');
        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            // Mostramos un mensaje con la respuesta 
            success: function (data) {
                $('#formTransfer').waitMe('hide');
                swal("Actualizado!", data.responseText, "success");
                ocultarModal("#modalTransfer");
                table.destroy();
                crearDataTable();
                document.getElementById("formTransfer").reset();
            },
            error: function (data) {
                $('#formTransfer').waitMe('hide');
                swal("Error!", data.responseText, "error");
            }
        })

        return false;
    });

    // Interceptamos el evento submit para venta
    $('#formAVenta').submit(function () {
        // Enviamos el formulario usando AJAX
        runWaitMe('#formAVenta');
        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            // Mostramos un mensaje con la respuesta 
            success: function (data) {
                $('#formAVenta').waitMe('hide');
                swal("Venta realizda!", data.responseText, "success");
                ocultarModal("#modalAVenta");
                table.destroy();
                crearDataTable();
                document.getElementById("formAVenta").reset();
            },
            error: function (data) {
                $('#formAVenta').waitMe('hide');
                swal("Error!", data.responseText, "error");
            }
        })

        return false;
    });


    //table filtering
    $('#inputAlmacen').change(function () {
        table.draw();
    });
});

//crear datatable
function crearDataTable() {
    $('[data-toggle="tooltip"]').tooltip();
    //Crea data table
    table = $("#tableConsulta").DataTable({
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
            "url": "Consultas/LoadDataTable",
            "type": "post",
            "dataSrc": "",
        },
        "columns": [
            { data: "Clave" },
            { data: "Nombre" },
            { data: "Marca" },
            { data: "Descripcion" },
            { data: "Presentacion" },
            { data: "Precio_Costo", render: $.fn.dataTable.render.number(',', '.', 0, '$') },
            { data: "Importe_Inventario", render: $.fn.dataTable.render.number(',', '.', 0, '$') },
            { data: "Almacen" },
            { data: "Ubicacion" },
            { data: "Existencia" },
            {
                "data": null,
                "defaultContent": "<button data-toggle='tooltip' data-placement='left' title='Modificar registro' id='btnMod' class='btn btn-primary btn-xs glyphicon glyphicon-pencil'></button>"
                                + "<button data-toggle='tooltip' data-placement='left' title='Agregar a venta' id='btnVenta' class='btn btn-success btn-xs glyphicon glyphicon-shopping-cart'></button>"
                                + "<button data-toggle='tooltip' data-placement='left' title='Alta de inventario' id='btnAltaIn' class='btn btn-info btn-xs glyphicon glyphicon-plus'></button>"
                                + "<button data-toggle='tooltip' data-placement='left' title='Transferencia' id='btnTransferencia' class='btn btn-default btn-xs glyphicon glyphicon-transfer'></button>"
            }

        ]

    });

}

$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        var almacen = $('#inputAlmacen').val();
        var almacenT = data[7]; // use data for the age column

        if (almacen == almacenT || almacen=="") {
            return true;
        }
        return false;
    }
);

//ocultar modal
function ocultarModal(modal) {
    $(modal).modal('toggle');
}

//Función para mostrar div de carga al dar de alta
function runWaitMe(contenedor) {
    $(contenedor).waitMe({
        effect: 'roundBounce',
        text: 'Procesando registro, por favor espere...',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000',
        sizeW: '',
        sizeH: '',
        source: '/Content/img/img.svg'
    });
}
