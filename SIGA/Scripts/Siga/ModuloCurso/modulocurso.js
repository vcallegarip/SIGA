var mcVM = null;

function modulocursoViewModel() {

    mcVM = this;
    mcVM.modulos = ko.observableArray([]);
    mcVM.cursos = ko.observableArray([{ CurId:0, CurName:"", CurNumHoras:"", CurPrecio:"" }]);
    mcVM.nombreCursos = ko.observableArray([]);

    mcVM.addCurso = function () {
        addCurso();
    }

    mcVM.removeCurso = function (curso) {
        mcVM.cursos.remove(curso);
    };

    mcVM.getModulos = function () {
        $.ajax({
            url: "/api/ModuloCurso",
            dataType: "Json",
            data: '',
            success: function (data) {
                var modulos = $.map(data, function (item) { return new Modulo(item) });
                var data = ko.utils.arrayMap(modulos, function (item) {
                    return new toggleCursoDiv(item);
                });
                mcVM.modulos(data);

            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    };


    //mcVM.getNombresCursos = function () {
    //    $.ajax({
    //        url: "/api/GetNombresdeCursos",
    //        dataType: "Json",
    //        data: '',
    //        success: function (data) {
    //            var nombres = $.map(data, function (item) { return new Modulo(item) });
    //            mcVM.nombreCursos(data)
    //        },
    //        error: function (xhr, result, status) {
    //            alert(getAjaxErrorText(xhr));
    //        }
    //    });
    //}

    //crVM.crearCurso = function () {
    //    var postData = mcVM.modulos;
    //    var url = "api/CreateModulo";
    //    $.ajax({
    //        url: url,
    //        type: 'Post',
    //        dataType: "Json",
    //        contentType: 'application/json;charset=utf-8',
    //        data: ko.toJSON(postData),
    //        success: function (data) {
    //            //var request = new Request(data.Request);
    //            //crVM.request(request);
    //            //crVM.LoggedInUser = data.UserName;
    //            //crVM.modulePaneLoad(request.MappedRequestModules[0]); // load the first module for display
    //            //crVM.step3ActiveModuleTab(request.MappedRequestModules[0]);
    //        },
    //        error: function (xhr, result, status) {
    //            alert(getAjaxErrorText(xhr));
    //        }
    //    });
    //}

    //crVM.createModulo = function () {
    //    var postData = mcVM.modulos;
    //    var url = "api/CreateModulo";
    //    $.ajax({
    //        url: url,
    //        type: 'Post',
    //        dataType: "Json",
    //        contentType: 'application/json;charset=utf-8',
    //        data: ko.toJSON(postData),
    //        success: function (data) {
    //            //var request = new Request(data.Request);
    //            //crVM.request(request);
    //            //crVM.LoggedInUser = data.UserName;
    //            //crVM.modulePaneLoad(request.MappedRequestModules[0]); // load the first module for display
    //            //crVM.step3ActiveModuleTab(request.MappedRequestModules[0]);
    //        },
    //        error: function (xhr, result, status) {
    //            alert(getAjaxErrorText(xhr));
    //        }
    //    });
    //}

}

function toggleCursoDiv(item) {
    var self = this;
    self.ModId = ko.observable(item.ModId);
    self.ModCategroria = ko.observable(item.ModCategroria);
    self.ModNivel = ko.observable(item.ModNivel);
    self.ModNombre = ko.observable(item.ModNombre);
    self.ModNumHoras = ko.observable(item.ModNumHoras);
    self.ModNumMes = ko.observable(item.ModNumMes);
    self.ModNumCursos = ko.observable(item.ModNumCursos);
    self.Cursos = ko.observable(item.Cursos);
    
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
    return;
}

function addCurso() {

    mcVM.cursos.push(new Curso(''));
    $(".txtCurName").autocomplete({
        source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            $.ajax({
                url: "/api/CursoSearch?search=",
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (c, i) {
                        var text = c.CurName;
                        if (text && (!request.term || matcher.test(text))) {
                            return {
                                label: c.CurName,
                                value: c.CurName,
                                curid: c.CurId,
                                numHoras: c.CurNumHoras,
                                precio: c.CurPrecio,
                            };
                        }
                    }));
                }
            });
        },
        select: function (event, ui) {
            $('#txtCurName').val(ui.item.label);
            $('#txtCurNumHoras').val(ui.item.numHoras);
            $('#txtCurPrecio').val(ui.item.precio);
        }
    });

};