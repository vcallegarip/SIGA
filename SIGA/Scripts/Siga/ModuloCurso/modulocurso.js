var mcVM = null;

function modulocursoViewModel() {

    mcVM = this;

    /////////////////////
    //// modulo form ////
    /////////////////////
    //mcVM.ModId = ko.observable('');
    //mcVM.ModCategroria = ko.observable('');
    //mcVM.ModNivel = ko.observable('');
    //mcVM.ModNombre = ko.observable('');
    //mcVM.ModNumHoras = ko.observable('');
    //mcVM.ModNumMes = ko.observable('');
    //mcVM.ModNumCursos = ko.observable('');
    /////////////////////

    //mcVM.modulo = ko.observable(new Modulo(''));

    mcVM.modulo = ko.observable();
    mcVM.modulos = ko.observableArray([]);
    //mcVM.cursos = ko.observableArray([{ CurId: 0, CurName: "", CurNumHoras: "", CurPrecio: "" }]);
    mcVM.cursoPushIndex = ko.observable(0);
    mcVM.lastPushedCursoName = ko.observable('');

    mcVM.addCurso = function () {
        addCurso();
    }

    mcVM.removeCurso = function (curso) {
        mcVM.cursos.remove(curso);
    };

    mcVM.getModulo = function (id) {
        
        $.ajax({
            url: "/api/ModuloCurso?id=" + id,
            dataType: "Json",
            type: 'GET',
            data: '',
            success: function (data) {
                debugger
                mcVM.modulo(data);
            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    };

    //mcVM.getModulos = function () {
    //    $.ajax({
    //        url: "/api/ModuloCurso",
    //        dataType: "Json",
    //        data: '',
    //        success: function (data) {
    //            var modulos = $.map(data, function (item) { return new Modulo(item) });
    //            var data = ko.utils.arrayMap(modulos, function (item) {
    //                return new toggleCursoDiv(item);
    //            });
    //            mcVM.modulos(data);

    //        },
    //        error: function (xhr, result, status) {
    //            alert(getAjaxErrorText(xhr));
    //        }
    //    });
    //};

    mcVM.guardarModulo = function () {
        debugger
        var postData = ko.toJSON(mcVM.modulo());
        var url = "api/ModuloCurso";
        $.ajax({
            url: url,
            type: 'POST',
            dataType: "Json",
            //contentType: 'application/json; charset=utf-8',
            data: postData,
            //data: ({
            //    ModId: '10',
            //    ModNivel: 'sdfs',
            //    ModCategroria: 'sdfsd',
            //    ModNombre: 'sdfs',
            //    ModNumHoras: '12',
            //    ModNumMes: '2',
            //    ModNumCursos: '2',
            //}),
            success: function (data) {
                //var request = new Request(data.Request);
                //crVM.request(request);
                //crVM.LoggedInUser = data.UserName;
                //crVM.modulePaneLoad(request.MappedRequestModules[0]); // load the first module for display
                //crVM.step3ActiveModuleTab(request.MappedRequestModules[0]);
            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    }

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
    mcVM.getModulo(0);
    return;
}

function addCurso() {

    var cursoArrayLength = mcVM.cursos().length;
    if (mcVM.lastPushedCursoName() == "")
        return false;

    mcVM.cursos.push(new Curso(''));
    mcVM.lastPushedCursoName('');
    mcVM.cursoPushIndex(cursoArrayLength);

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
            var search = ui.item.CurName;
            $.ajax({
                url: '/api/CursoSearch?search=' + search,
                dataType: "json",
                success: function (data) {
                    mcVM.cursos()[mcVM.cursoPushIndex()].CurId(ui.item.CurId);
                    mcVM.cursos()[mcVM.cursoPushIndex()].CurName(ui.item.CurName);
                    mcVM.cursos()[mcVM.cursoPushIndex()].CurNumHoras(ui.item.CurNumHoras);
                    mcVM.cursos()[mcVM.cursoPushIndex()].CurPrecio(ui.item.CurPrecio);
                }
            });

            mcVM.lastPushedCursoName(ui.item.CurName);
        }
    });

};
