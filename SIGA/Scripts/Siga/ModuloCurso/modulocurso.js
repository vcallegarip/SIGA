var mcVM = null;

function modulocursoViewModel() {
    mcVM = this;

    mcVM.modulo = ko.observableArray([]);
    mcVM.modulos = ko.observableArray([]);
    mcVM.Cursos = ko.observableArray([]);
    mcVM.moduloCursosNew = ko.observableArray([]);
    mcVM.cursoPushIndex = ko.observable(0);
    mcVM.lastPushedCursoName = ko.observable('');

    mcVM.isCreating = ko.observable(0);

    mcVM.addCurso = function () {
        
        //for (var i = 0; i < mcVM.Cursos().length; i++) {
        //    if (mcVM.Cursos()[i].CurName == "") {
        //        return false;
        //    }
        //};

        //mcVM.Cursos = ko.observableArray(ko.utils.arrayMap(mcVM.modulo().Cursos, function (curso) {
        //    return new Curso(curso)
        //}));

        //mcVM.modulo().Cursos.push.apply(mcVM.modulo().Cursos, [new Curso({ CurId: 0, CurName: "", CurNumHoras: "", CurPrecio: "" })]);
        mcVM.Cursos.push(new Curso(''));
        mcVM.lastPushedCursoName('');
        mcVM.cursoPushIndex(mcVM.cursoPushIndex() + 1);

        enableAutoComplete();
    }

    mcVM.removeCurso = function (curso) {
        mcVM.Cursos.remove(curso);
        mcVM.cursoPushIndex(mcVM.cursoPushIndex() - 1)
    };

    mcVM.getModulo = function (id) {
        
        $.ajax({
            url: "/api/ModuloCurso?id=" + id,
            dataType: "Json",
            type: 'GET',
            data: '',
            success: function (data) {
                mcVM.modulo(data);
                mcVM.isCreating(data.ModId);
                for (var i = 0; i < mcVM.modulo().Cursos.length; i++) {
                    mcVM.Cursos.push(mcVM.modulo().Cursos[i]);
                }

                if (id == 0) {
                    mcVM.lastPushedCursoName("")
                }
                enableAutoComplete();
            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    };

    mcVM.getModulos = function () {
        $.ajax({
            url: "/api/ModuloCurso",
            dataType: "Json",
            data: '',
            success: function (data) {
                var modulos = $.map(data, function (item) { return new Modulo(item) });
                var dataMapped = ko.utils.arrayMap(modulos, function (item) {
                    return new toggleCursoDiv(item);
                });
                mcVM.modulos(dataMapped);
            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    };

    mcVM.guardarModulo = function () {

        //mcVM.modulo().Cursos.splice(0, 1);
        //uniqueArray(mcVM.modulo().Cursos);

        var cursoObservableUnwrapped = ko.toJS(mcVM.Cursos());
        mcVM.modulo().Cursos = cursoObservableUnwrapped;
        var postData = mcVM.modulo();

        var url = "api/ModuloCurso";
        $.ajax({
            url: url,
            type: mcVM.isCreating() < 1 ? 'Post' : 'Put',
            dataType: "Json",
            data: JSON.stringify(postData),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('.body-content').prepend('<div class="alert alert-success"><strong>Exito!</strong> El Modulo fue guardado con exito.</div>');
                setTimeout(function () {
                    if (mcVM.isCreating() < 1)
                        $("#mainContainer").load('/ModuloCurso');
                    else
                        editModuloCurso(mcVM.isCreating());
                }, 3000);
            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    }

    mcVM.moduloToDelete = 0;

    mcVM.showModuloDeleteModal = function (moduloid) {
        mcVM.moduloToDelete = moduloid;
        showConfirmDialog(mcVM.moduloDelete, "Confirmacion", "Estas seguro que desea eliminar este modulo?", "Si", "Cancelar", null);
        return false;
    }

    mcVM.moduloDelete = function () {
        var URL = "ModuloCurso/Delete?moduloid=" + mcVM.moduloToDelete;
        $.ajax(
        {
            url: URL,
            type: "Delete",
            data: '',
            async: true,
            success: function (data) {
                if (data == "OK") {
                    $('.body-content').prepend('<div class="alert alert-success"><strong>Exito!</strong> El Modulo fue eliminado con exito.</div>');
                    $("#mainContainer").load('/ModuloCurso');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('.body-content').prepend('<div class="alert alert-danger">' + jqXHR + '</div>');
                //alert(getAjaxErrorText(xhr));
            }
        });
    }

}


function uniqueArray(list) {
    var result = [];
    $.each(list, function (i, e) {
        if ($.inArray(e, result) == -1) result.push(e);
    });
    return result;
}


function toggleCursoDiv(item) {
    var self = this;
    self.ModId = item.ModId || '';
    self.ModCategroria = item.ModCategroria || '';
    self.ModNivel = item.ModNivel || '';
    self.ModNombre = item.ModNombre || '';
    self.ModNumHoras = item.ModNumHoras || '';
    self.ModNumMes = item.ModNumMes || '';
    self.ModNumCursos = item.ModNumCursos || '';
    self.Cursos = item.Cursos || '';
    
    self.expanded = ko.observable(false);
    self.toggle = function (item) {
        self.expanded(!self.expanded());
    };

    self.linkLabelCount = ko.computed(function () {
        return self.expanded() ? "<span style='position:relative; top:-5px;' class='badge'>" + item.Cursos.length + "</span>"
            : item.Cursos.length > 0 ? "<span style='position:relative; top:-5px;' class='badge'>" + item.Cursos.length + "</span>" : "";
    }, self);
    self.linkLabelIcon = ko.computed(function () {
        return self.expanded() ? "<i style='font-size:21px; position:relative; top:-2px;' class='fa fa-times-circle'></i> "
            : item.Cursos.length > 0 ? "<i style='font-size:20px;' class='fa fa-external-link'></i>" : "";
    }, self);

    
}

function getModulos() {
    mcVM.getModulos();
}

function loadCreateEditModuloCursoView() {
    $("#mainContainer").load('/ModuloCurso/create');
    mcVM.getModulo(0);
    return;
}

function enableAutoComplete() {

    $(".txtCurName").autocomplete({
        source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            $.ajax({
                url: "/api/CursoSearch?search=",
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (c, i) {
                        var text = c.CurName;
                        var index = i;
                        var cursosFound = c;
                        if (text && (!request.term || matcher.test(text))) {
                            return {
                                label: c.CurName,
                                value: c.CurName,
                                CurName: c.CurName,
                                CurId: c.CurId,
                                CurNumHoras: c.CurNumHoras,
                                CurPrecio: c.CurPrecio,
                                index: index,
                                cursosFound: c
                            };
                        }
                    }));
                }
            });
        },
        select: function (event, ui) {

            //for (var j = 0; j < mcVM.modulo().Cursos.length; j++) {
            //    if (ui.item.CurName == mcVM.modulo().Cursos[j].CurName()) {
            //        return false;
            //    }
            //}

            mcVM.modulo().Cursos[mcVM.cursoPushIndex()].CurId   = ui.item.CurId;
            mcVM.modulo().Cursos[mcVM.cursoPushIndex()].CurName      = ui.item.CurName;
            mcVM.modulo().Cursos[mcVM.cursoPushIndex()].CurNumHoras  = ui.item.CurNumHoras;
            mcVM.modulo().Cursos[mcVM.cursoPushIndex()].CurPrecio    = ui.item.CurPrecio;
        }
    });

};

function editModuloCurso(modulocursoid) {
    mcVM.getModulo(modulocursoid);
    $("#mainContainer").load('/ModuloCurso/Edit');
    return;
}

function detailModuloCurso(modulocursoid) {
    mcVM.getModulo(modulocursoid);
    $("#mainContainer").load('/ModuloCurso/Details');
    return;
}

function eliminarModuloCurso(modulocursoid) {
    mcVM.showModuloDeleteModal(modulocursoid);
    return;
}

