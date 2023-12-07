$(document).ready(function () {
    GetProducts();
    //Swal.fire({
    //    title: "Good job!",
    //    text: "You clicked the button!",
    //    icon: "success"
    //});

})
function GetProducts() {
    $.ajax({
        //beforeSend: function () {
        //    $.blockUI({
        //        theme: true,
        //        message: '<div class="row"><div class="col-lg-12"><br /><p style="font-size:small; text-align: center;"><img src="/SASE/Content/assets/img/loading.gif" style="width: 35px;" /></p><p style="font-size:small; text-align: center;">Buscando Registros Espere un Momento Por favor...</p><br /></div></div>',
        //        baseZ: 10000
        //    });
        //},
        url: '/Productos/GetProducts',
        type: 'GET',
        dataType: 'html', success: function (data) {
            if (data != undefined || data != null) {
                $('#divResult').html(data);
            }
        },
    });
}
function OpenMdlProducts(button) {
    SelectCategoria(true);
    $('#mdlAddProduct').modal('show');
}
function SelectCategoria(isNew) {
    let _isNew = isNew;
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: '/Productos/GetCategorias',
            type: 'POST',
            dataType: 'json',
            data: '{}',
            success: function (data) {
                if (_isNew) {
                    for (var i = 0; i < data.items.length; i++) {
                        $('#selectCategoria').append('<option value =' + data.items[i].idCategoria + '>' + data.items[i].descripcion + '</option>');
                    }
                } else {
                    for (var i = 0; i < data.items.length; i++) {
                        $('#selectCategoriaEdit').append('<option value =' + data.items[i].idCategoria + '>' + data.items[i].descripcion + '</option>');
                    }
                }

                resolve(true);
            },
            error: function (jqXHR, status, error) {
                swal({
                    title: "¡Error!",
                    text: 'Surgio un error inesperado. \n' + jqXHR.status + ' ' + jqXHR.statusText,
                    icon: "warning",
                });
                reject(error);
            }
        });
    });
}
function SaveProduct() {
    let _Descripcion = document.getElementById('txtDescripcion');
    let _PrecioCompra = document.getElementById('txtPrecioCompra');
    let _PrecioVenta = document.getElementById('txtPrecioVenta');
    let _Stock = document.getElementById('txtStock');
    let _Categoria = document.getElementById('selectCategoria');
    if (_Descripcion.value === '' || _PrecioCompra.value === ''
        || _PrecioVenta.value === '' || _Stock.value === '' || _Categoria.value === '') {

        Swal.fire({
            title: "Warning!",
            text: "¡Los campos son requeridos!",
            icon: "warning"
        });
    }
    let _Req = {
        descripcion: _Descripcion.value,
        precioCompra: _PrecioCompra.value,
        precioVenta: _PrecioVenta.value,
        stock: _Stock.value,
        idCategoria: _Categoria.value

    }
    $.ajax({
        beforeSend: function () {
            $.blockUI({
                theme: true,
                message: '<div class="row"><div class="col-md-12"><p><img src="img/loading.gif" style="width: 35px;" /><span> Espere por favor...</span></p></div></div>',
                baseZ: 10000
            });
        },
        url: '/Productos/SaveProducto',
        type: 'Post',
        data: _Req,
        success: function (data) {
            $.unblockUI();
            if (data.isOK) {
                Swal.fire({
                    title: "success",
                    icon: "success"
                });
                ResetInputs();
                GetProducts();
                CloseMdlProducts();
            } else {
                Swal.fire({
                    title: "Error!",
                    text: data.error,
                    icon: "warning"
                });
            }

        },
    });
}
function ResetInputs() {
    let _Descripcion = document.getElementById('txtDescripcion');
    let _PrecioCompra = document.getElementById('txtPrecioCompra');
    let _PrecioVenta = document.getElementById('txtPrecioVenta');
    let _Stock = document.getElementById('txtStock');
    let _Categoria = document.getElementById('selectCategoria');
    _Descripcion.value = ''
    _PrecioCompra.value = ''
    _PrecioVenta.value = ''
    _Stock.value = ''
    _Categoria.value = ''
}
function CloseMdlProducts() {
    $('#mdlAddProduct').modal('hide');
}
function RemoveProduct(button) {
    if (button != undefined) {
        Swal.fire({
            title: '¡Aviso!',
            text: '¿Deseas eliminar el registro?',
            icon: 'warning',
            buttons: {
                cancel: {
                    text: "Cancelar",
                    value: false,
                    visible: true,
                    className: "",
                    closeModal: true,
                },
                confirm: {
                    text: "Eliminar",
                    value: true,
                    visible: true,
                    className: "custom-delete-button",
                    closeModal: true
                }
            }
        }).then((result) => {
            if (result) {
                let _Req = {
                    _IdProducto: parseInt($(button).closest('tr').find('td:eq(0)').text()),
                };
                $.ajax({
                    beforeSend: function () {
                        $.blockUI({
                            theme: true,
                            message: '<div class="row"><div class="col-md-12"><p><img src="~/img/loading.gif" style="width: 35px;" /><span> Espere por favor...</span></p></div></div>',
                            baseZ: 10000
                        });
                    },
                    url: '/Productos/DeleteProduct',
                    type: 'POST',
                    data: _Req,
                    success: function (data) {
                        $.unblockUI();
                        if (data.isOK) {
                            Swal.fire({
                                text: "Registro Eliminado",
                                icon: "success",
                            });

                            GetProducts();
                        } else {
                            Swal.fire({
                                text: data.error,
                                icon: "error",
                            });
                        }
                    }
                    //error: function (jqXHR, status, error) {
                    //    //$.unblockUI();
                    //    Swal.fire({
                    //        icon: 'Error',
                    //        title: 'Aviso...',
                    //        text: 'Surgio un error inesperado. \n' + jqXHR.status + ' ' + jqXHR.statusText,
                    //        allowEscapeKey: true,
                    //        allowEnterKey: true,
                    //        focusCancel: true,
                    //        focusConfirm: true,
                    //    });
                    //}
                });
            }
            else {
                return;
            }
        });
    }
}
function UpdateProduct() {
   let _Element = document.getElementsByClassName("Id")
    let _IdCategoria = document.getElementById('selectCategoriaEdit');
    let _Descripcion = document.getElementById('txtDescripcionEdit');
    let _PrecioCompra = document.getElementById('txtPrecioCompraEdit');
    let _PrecioVenta = document.getElementById('txtPrecioVentaEdit');
    let _Stock = document.getElementById('txtStockEdit');
    let _text = _Element[0];
    let _Id = _text.innerHTML;

    if (_Id === '',_Descripcion.value === '' || _PrecioCompra.value === ''
        || _PrecioVenta.value === '' || _Stock.value === '' || _IdCategoria.value === '') {

        Swal.fire({
            title: "Warning!",
            text: "¡Los campos son requeridos!",
            icon: "warning"
        });
        return
    }
    let _Req = {
        IdProducto: parseInt(_Id),
        Descripcion: _Descripcion.value,
        PrecioCompra: _PrecioCompra.value,
        PrecioVenta: _PrecioVenta.value,
        Stock: _Stock.value,
        IdCategoria: _IdCategoria.value
    }
    $.ajax({
        //beforeSend: function () {
        //    //$.blockUI({
        //    //    theme: true,
        //    //    message: '<div class="row"><div class="col-lg-12"><br /><p style="font-size:small; text-align: center;"><img src="/SASE/Content/assets/img/loading.gif" style="width: 35px;" /></p><p style="font-size:small; text-align: center;">Buscando Registros Espere un Momento Por favor...</p><br /></div></div>',
        //    //    baseZ: 10000
        //    //});
        //},
        url: '/Productos/UpdateProducto',
        type: 'Post',
        data: _Req,
        success: function (data) {
            if (data.isOK) {
                Swal.fire({
                    title: "success",
                    icon: "success"
                });
                ResetInputs();
                GetProducts();
                CloseMdlProducts();
            } else {
                Swal.fire({
                    title: "Error!",
                    text: data.error,
                    icon: "warning"
                });
            }

        },
    });
}
function OpenEditMdlProducts(button) {
    ResetSelectEdit();
    SelectCategoria(false).then(function (data) {
        if (data) {


            let Id = $(button).closest('tr').find('td:eq(0)').text().trim();
            let Codigo = $(button).closest('tr').find('td:eq(1)').text().trim();
            let Categoria = parseInt($(button).closest('tr').find('td:eq(2)').text().trim());
            let Descripcion = $(button).closest('tr').find('td:eq(4)').text().trim();
            let PrecioCompra = $(button).closest('tr').find('td:eq(5)').text().trim().replace('$', '').replace(',', '');
            let PrecioVenta = $(button).closest('tr').find('td:eq(6)').text().trim().replace('$', '').replace(',', '');
            let Stock = $(button).closest('tr').find('td:eq(7)').text().trim();

            $('.Id').text(Id);
            $('.Codigo').text(Codigo);
            $('#txtDescripcionEdit').val(Descripcion);
            $('#selectCategoriaEdit').val(Categoria);
            $('#txtPrecioCompraEdit').val(PrecioCompra);
            $('#txtPrecioVentaEdit').val(PrecioVenta);
            $('#txtStockEdit').val(Stock);
        }
    }).catch(function (err) {
        console.log("error promise ==>", err);
    });
    $('#mdlEditProduct').modal('show');
}
function CloseMdlEditProduct() {
    ResetSelectEdit();
    ResetInputsEdit();
    $('#mdlEditProduct').modal('hide');
}
function ResetSelectEdit() {
    $('#selectCategoriaEdit option').each(function () {
        if ($(this).val() != '') {
            $(this).remove();
        }
    });
}
function ResetInputsEdit() {
    ResetSelectEdit();
    document.getElementsByClassName("Id").value = "";
    document.getElementsByClassName("Codigo").value = "";
    let _Descripcion = document.getElementById('txtDescripcionEdit');
    let _PrecioCompra = document.getElementById('txtPrecioCompraEdit');
    let _PrecioVenta = document.getElementById('txtPrecioVentaEdit');
    let _Stock = document.getElementById('txtStockEdit');
    let _Categoria = document.getElementById('selectCategoriaEdit');
    _Descripcion.value = ''
    _PrecioCompra.value = ''
    _PrecioVenta.value = ''
    _Stock.value = ''
    _Categoria.value = ''
}


//function SearchProveedor() {
//    let _txtCveProvNew = document.getElementById('txtCveProvNew').value;
//    let _txtNomProvNew = document.getElementById('txtNomProvNew').value;
//    if (_txtCveProvNew === '' && _txtNomProvNew === '') {

//        swal({
//            title: 'warning',
//            text: "Ingresa un parametro de búsqueda",
//            icon: "warning",
//        });
//        return;
//    }
//    let _Req = {
//        ClaveProveedor: _txtCveProvNew, NombreComercial: _txtNomProvNew
//    };
//    $.ajax({
//        beforeSend: function () {
//            $.blockUI({
//                theme: true,
//                message: '<div class="row"><div class="col-md-12"><p><img src="/SASE/Content/assets/img/loading.gif" style="width: 35px;" /><span> Espere por favor...</span></p></div></div>',
//                baseZ: 10000
//            });
//        },
//        url: '/SASE/Medicamentos/SearchProveedor',
//        type: 'POST',
//        data: _Req,
//        success: function (data) {
//            $.unblockUI();
//            $('#tableDetalle > tbody').empty();
//            let _TableData = "";
//            if (data.IsOK) {
//                if (data.data.length > 0) {
//                    $.each(data.data, function (index, item) {
//                        _TableData = "<tr>" +
//                            "<td style='font-size:small' id='ClaveProveedor'>" + item.ClaveProveedor + "</td>" +
//                            "<td style='font-size:small' id='NombreComercial'>" + item.NombreComercial + "</td>" +
//                            "<td style='font-size:small' id='otro'>" + "<button onclick='SaveProviderGab(this)' type='button' title='Agregar' class='btn-circle btn-circle-danger'><i class='fa fa-user-plus' aria-hidden='true'></i></button>" + "</td>" +
//                            "</tr>";
//                        $('#tableDetalle > tbody').append(_TableData);
//                    });
//                } else {
//                    _TableData += '<tr>';
//                    _TableData += '<td colspan="3"  style="color:white; background-color: crimson; text-align: center; vertical-align: middle;"> No hay datos a mostrar </td>';
//                    _TableData += '</tr>';
//                    $("#tableDetalle").append(_TableData);
//                }
//            } else {
//                swal({
//                    title: '¡Error!',
//                    text: data.Error,
//                    icon: "error",
//                });
//            }
//        },
//        error: function (jqXHR, status, error) {
//            $.unblockUI();
//            swal({
//                icon: 'error',
//                title: '¡Error!',
//                text: 'Surgio un error inesperado. \n' + jqXHR.status + ' ' + jqXHR.statusText,
//                allowEscapeKey: true,
//                allowEnterKey: true,
//                focusCancel: true,
//                focusConfirm: true,
//            });
//        }
//    });
//}
