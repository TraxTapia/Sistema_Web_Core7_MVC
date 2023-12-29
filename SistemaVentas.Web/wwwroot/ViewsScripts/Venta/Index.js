let productos = [];
$(document).ready(function () {
    MostrarPrecios();
})
$("#buscarProducto").autocomplete({
    source: function (request, response) {

        let _Req = {
            Search: request.term
        }
        jQuery.ajax({
            url: '/Venta/Autocomplete',
            //url: "@Url.Action("AutoCompleteProducto","Home")?search=" + request.term,
            type: "POST",
            data: _Req,
            //dataType: "json",
            success: function (data) {

                response(
                    $.map(data.autocomplete, function (item) {
                        return {
                            label: item.label
                            , value: item.value
                        }
                    })
                )
            },
            error: function (error) {
                console.log("bad")
            }
        })
    },
    minLength: 1,
    select: function (event, ui) {
        event.preventDefault();

        //$(this).val(ui.item.label);
        //$(this).data("idproducto",ui.item.value);
        AgregarProducto(ui.item.value)
        $(this).val("");
    },
    focus: function (event, ui) {
        event.preventDefault();
        $(this).val(ui.item.label);
    }
})
$("#txtPagoCon").on("keyup", function (event) {
    if (event.keyCode === 13) {
        calcularCambio();
    }
})
function calcularCambio() {

    let total = productos.reduce(function (accumulator, item) {
        return accumulator + item.Total
    }, 0)

    var montopago = $("#txtPagoCon").val().trim() == "" ? total : parseFloat($("#txtPagoCon").val().trim());
    var cambio = 0;
    cambio = (montopago <= total ? total : montopago) - total;
    if ($("#txtPagoCon").val().trim() == "") {
        $("#txtPagoCon").val(total.toFixed(2));
    }


    $("#txtCambio").val("$ " + cambio.toFixed(2));
}
function AgregarProducto(_Idproducto) {
    let _Req = {
        idproducto: _Idproducto
    }
    $.ajax({
        //url: "@Url.Action("ObtenerProducto","Home")?idproducto=" + idproducto,
        url: '/Venta/ObtenerProducto',
        type: "POST",
        data: _Req,
        success: function (data) {

            const existe = productos.some(element => element.IdProducto === data._Product.idProducto);

            if (existe) {
                Object.keys(productos).forEach(index => {

                    if (productos[index].IdProducto === data._Product.idProducto) {
                        productos[index] = {
                            IdProducto: data._Product.idProducto,
                            Descripcion: data._Product.descripcion,
                            PrecioVenta: data._Product.precioVenta,
                            Cantidad: productos[index].Cantidad + 1,
                            Total: data._Product.precioVenta * (productos[index].Cantidad + 1)
                        }
                    }
                });

            } else {
                var producto = {
                    IdProducto: data._Product.idProducto,
                    Descripcion: data._Product.descripcion,
                    PrecioVenta: data._Product.precioVenta,
                    Cantidad: 1,
                    Total: data._Product.precioVenta
                }

                productos.push(producto);
            }
            $("#tabla tbody").html("");
            $.each(productos, function (i, item) {
                $("<tr>").append(
                    $("<td>").append(item.Descripcion),
                    $("<td>").append(item.Cantidad),
                    $("<td>").append(item.PrecioVenta.toFixed(2)),
                    $("<td>").append(item.Total.toFixed(2)),
                    $("<td>").append(
                        $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                            $("<i>").addClass("fa fa-trash ")
                        )
                    ),
                ).data("idproducto", item.IdProducto).appendTo("#tabla tbody")
            })
            MostrarPrecios();
        },
        error: function (error) {
            console.log("bad")
        }
    })
}
function MostrarPrecios(tipoPago = false) {
    let total = productos.reduce(function (accumulator, item) {
        return accumulator + item.Total
    }, 0)

    let base = total / 1.18;
    let igv = total - base;

    $("#pVentaTotal").html("$ " + total.toFixed(2))
    $("#txtSubTotal").val("$ " + base.toFixed(2))
    $("#txtIgv").val("$ " + igv.toFixed(2))
    $("#txtTotal").val("$ " + total.toFixed(2))

    if (tipoPago) {
        $("#txtPagoCon").val("$ " + total.toFixed(2))
        $("#txtCambio").val("$  0.00")
    }
}
function mostrarListaVacia() {
    if (productos.length === 0) {
        $("#tabla tbody").html("<tr><td colspan='5'><p class='text-warning text-center'>Ningún producto seleccionado</p></td></tr>");
    }
}



$("#tabla tbody").on("click", ".btn-eliminar", function () {

    var idproducto = $(this).closest('tr').data("idproducto");

    const newArray = productos.filter(object => {
        return object.IdProducto !== idproducto;
    });

    productos = newArray;
    $(this).closest('tr').remove();

    MostrarPrecios();

    $("#txtPagoCon").val("");
    $("#txtCambio").val("");

})

$("#cbotipopago").on("change", function () {

    if ($(this).val() !== "Efectivo") {
        $("#txtPagoCon").attr({ "disabled": true });
        MostrarPrecios(true)
    } else {
        $("#txtPagoCon").removeAttr("disabled");
        $("#txtPagoCon").val("")
        $("#txtCambio").val("")
    }

})

$("#btnLimpiarLista").on("click", function () {
    $("#tabla tbody").html("");
    productos = [];
    mostrarListaVacia();
    MostrarPrecios();
    $("#txtPagoCon").val("");
    $("#txtCambio").val("");
})


$("#btnTerminarVenta").on("click", function () {

    if (productos.length == 0) {
        Swal.fire({
            icon: 'error',
            title: 'No existen productos',
        })

        return;
    }

    calcularCambio();


    let _Req = {
        TipoPago: $("#cbotipopago option:selected").val(),
        DocumentoCliente: $("#txtdocumentocliente").val("001"),
        NombreCliente: $("#txtnombrecliente").val("Venta Online"),
        MontoPagoCon: parseFloat($("#txtPagoCon").val()),
        MontoCambio: parseFloat($("#txtCambio").val().replace("$ ", "", "gi")),
        MontoSubTotal: parseFloat($("#txtSubTotal").val().replace("$ ", "", "gi")),
        MontoIGV: parseFloat($("#txtIgv").val().replace("$ ", "", "gi")),
        MontoTotal: parseFloat($("#txtTotal").val().replace("$ ", "", "gi")),
        DetalleVenta: $.map(productos, function (item) {
            return {
                IdProducto: item.IdProducto,
                PrecioVenta: item.PrecioVenta,
                Cantidad: item.Cantidad,
                Total: item.Total
            }
        })
    }


    jQuery.ajax({
        //url: '@Url.Action("RegistrarVenta", "Home")',
        url: '/Venta/RegisterVentas',
        type: "POST",
        data: _Req,
        success: function (data) {
            console.log(data)
            return
            if (data.respuesta !== "") {

                Swal.fire({
                    icon: 'success',
                    title: 'Venta Registrada',
                    text: 'Nro Documento: ' + data.respuesta
                })

                $("#tabla tbody").html("");
                productos = [];
                mostrarListaVacia();
                MostrarPrecios();
                $("#txtPagoCon").val("");
                $("#txtCambio").val("");
                $("#txtdocumentocliente").val("");
                $("#txtnombrecliente").val("");
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'No se puedo completar la venta',
                })
            }
        },
        error: function (error) {
        },
        beforeSend: function () {
        }
    });



})