$(document).ready(function () {
    GetCategory();
})
function GetCategory() {
    $.ajax({
        beforeSend: function () {
            $.blockUI({
                theme: true,
                message: '<div class="row"><div class="col-lg-12"><br /><p style="font-size:small; text-align: center;"><img src="img/loading.gif" style="width: 35px;" /></p><p style="font-size:small; text-align: center;">Buscando Registros Espere un Momento Por favor...</p><br /></div></div>',
                baseZ: 10000
            });
        },
        url: '/Categorias/GetCategorias',
        type: 'GET',
        dataType: 'html',
        success: function (data) {
            $.unblockUI();
            if (data != undefined || data != null) {
                $('#divResult').html(data);
                var table = document.getElementById("tblCategorias");

                if (table != null || table != undefined) {
                    var rows = table.getElementsByTagName("tr");
                    input = document.getElementById("FilterTable");
                    input.addEventListener("input", function () {
                        var filter = input.value.toUpperCase();
                        for (var i = 1; i < rows.length; i++) {
                            var cells = rows[i].getElementsByTagName("td");
                            var found = false;
                            for (var j = 0; j < cells.length; j++) {
                                var cell = cells[j];
                                if (cell) {
                                    var textValue = cell.textContent || cell.innerText;
                                    if (textValue.toUpperCase().indexOf(filter) > -1) {
                                        found = true;
                                        break;
                                    }
                                }
                            }
                            if (found) {
                                rows[i].style.display = "";
                            } else {
                                rows[i].style.display = "none";
                            }
                        }
                    });
                } 
            }
        },
    });
}
function SaveCategory(button) {
    if (button != undefined) {
        let _txtDescripcion = document.getElementById('txtDescripcion');
        if (_txtDescripcion.value === '') {

            Swal.fire({
                title: "Warning!",
                text: "¡Los campos son requeridos!",
                icon: "warning"
            });
            return
        }
        let _Req = {
            _Descripcion: _txtDescripcion.value
        }
        $.ajax({
            beforeSend: function () {
                $.blockUI({
                    theme: true,
                    message: '<div class="row"><div class="col-md-12"><p><img src="img/loading.gif" style="width: 35px;" /><span> Espere por favor...</span></p></div></div>',
                    baseZ: 10000
                });
            },
            url: '/Categorias/SaveCategoria',
            type: 'POST',
            data: _Req,
            success: function (data) {
                $.unblockUI();
                if (data.isOK) {
                    Swal.fire({
                        title: "success",
                        icon: "success"
                    });
                    ResetInput();
                    $('#collapseExample').collapse('hide');
                    GetCategory();
                    //CloseMdlProducts();
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
}
function ResetInput() {
    let _txtDescripcion = document.getElementById('txtDescripcion');
    _txtDescripcion.value = '';
}
function RemoveCategory(button) {
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
                    _IdCategoria: parseInt($(button).closest('tr').find('td:eq(0)').text()),
                };
                $.ajax({
                    beforeSend: function () {
                        $.blockUI({
                            theme: true,
                            message: '<div class="row"><div class="col-md-12"><p><img src="img/loading.gif" style="width: 35px;" /><span> Espere por favor...</span></p></div></div>',
                            baseZ: 10000
                        });
                    },
                    url: '/Categorias/DeleteCategoria',
                    type: 'POST',
                    data: _Req,
                    success: function (data) {
                        $.unblockUI();
                        if (data.isOK) {
                            Swal.fire({
                                text: "Registro Eliminado",
                                icon: "success",
                            });

                            GetCategory();
                        } else {
                            Swal.fire({
                                text: data.error,
                                icon: "error",
                            });
                        }
                    }
                    //, error: function (jqXHR, status, error) {
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
function UpdateCategory() {
    let _Element = document.getElementsByClassName("Id")
    let _Descripcion = document.getElementById('txtDescripcionEdit');
    let _text = _Element[0];
    let _Id = _text.innerHTML;
    if (_Id === '', _Descripcion.value === '') {
        Swal.fire({
            title: "Warning!",
            text: "¡Los campos son requeridos!",
            icon: "warning"
        });
        return
    }
    let _Req = {
        IdCategoria: parseInt(_Id),
        Descripcion: _Descripcion.value
    }
    $.ajax({
        beforeSend: function () {
            $.blockUI({
                theme: true,
                message: '<div class="row"><div class="col-md-12"><p><img src="img/loading.gif" style="width: 35px;" /><span> Espere por favor...</span></p></div></div>',
                baseZ: 10000
            });
        },
        url: '/Categorias/UpdateCategoria',
        type: 'Post',
        data: _Req,
        success: function (data) {
            $.unblockUI();
            if (data.isOK) {
                Swal.fire({
                    title: "success",
                    icon: "success"
                });
                ResetInputsedit();
                GetCategory();
                CloseMdlEditCategory();
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
function OpenMdlEdit(button) {
    if (button != undefined) {
        let Id = $(button).closest('tr').find('td:eq(0)').text().trim();
        let _DescripcionEdit = $(button).closest('tr').find('td:eq(1)').text().trim();
        $('.Id').text(Id);
        $('#txtDescripcionEdit').val(_DescripcionEdit);
        $('#MdlEditCategory').modal('show');
    }   
}
function CloseMdlEditCategory() {
    //ResetSelectEdit();
    //ResetInputsEdit();
    $('#MdlEditCategory').modal('hide');
}
function ResetInputsedit() {
    document.getElementsByClassName("Id").value = "";
    document.getElementById('txtDescripcionEdit');
}