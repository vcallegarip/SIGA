var matrVM = null;

function modulocursoViewModel() {
    matrVM = this;

    matrVM.modulo = ko.observableArray([]);
    matrVM.modulos = ko.observableArray([]);
    matrVM.Cursos = ko.observableArray([]);
    matrVM.cursoPushIndex = ko.observable(0);
    matrVM.lastPushedCursoName = ko.observable('');

    matrVM.addCurso = function () {
        
        //for (var i = 0; i < matrVM.Cursos().length; i++) {
        //    if (matrVM.Cursos()[i].CurName == "") {
        //        return false;
        //    }
        //};

        //matrVM.Cursos = ko.observableArray(ko.utils.arrayMap(matrVM.modulo().Cursos, function (curso) {
        //    return new Curso(curso)
        //}));

        //matrVM.modulo().Cursos.push.apply(matrVM.modulo().Cursos, [new Curso({ CurId: 0, CurName: "", CurNumHoras: "", CurPrecio: "" })]);
        matrVM.Cursos.push(new Curso(''));
        matrVM.lastPushedCursoName('');
        matrVM.cursoPushIndex(matrVM.cursoPushIndex() + 1);

        enableAutoComplete();
    }

    matrVM.removeCurso = function (curso) {
        matrVM.Cursos.remove(curso);
        matrVM.cursoPushIndex(matrVM.cursoPushIndex() - 1)
    };

    matrVM.getModulo = function (id) {
        $.ajax({
            url: "/api/ModuloCurso?id=" + id,
            dataType: "Json",
            type: 'GET',
            data: '',
            success: function (data) {
                matrVM.modulo(data);
                if (id == 0) {
                    matrVM.lastPushedCursoName("")
                }
                enableAutoComplete();
            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    };

    matrVM.getModulos = function () {
        $.ajax({
            url: "/api/ModuloCurso",
            dataType: "Json",
            data: '',
            success: function (data) {
                var modulos = $.map(data, function (item) { return new Modulo(item) });
                var dataMapped = ko.utils.arrayMap(modulos, function (item) {
                    return new toggleCursoDiv(item);
                });
                matrVM.modulos(dataMapped);
            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    };

    matrVM.guardarModulo = function () {

        matrVM.modulo().Cursos.splice(0, 1);
        var cursoObservableUnwrapped = ko.toJS(matrVM.Cursos());
        matrVM.modulo().Cursos.push.apply(matrVM.modulo().Cursos, cursoObservableUnwrapped);
        var postData = matrVM.modulo();

        var url = "api/ModuloCurso";
        $.ajax({
            url: url,
            type: 'POST',
            dataType: "Json",
            data: JSON.stringify(postData),
            contentType: 'application/json; charset=utf-8',
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

    //crvm.crearcurso = function () {
    //    var postdata = mcvm.modulos;
    //    var url = "api/createmodulo";
    //    $.ajax({
    //        url: url,
    //        type: 'post',
    //        datatype: "json",
    //        contenttype: 'application/json;charset=utf-8',
    //        data: ko.tojson(postdata),
    //        success: function (data) {
    //            //var request = new request(data.request);
    //            //crvm.request(request);
    //            //crvm.loggedinuser = data.username;
    //            //crvm.modulepaneload(request.mappedrequestmodules[0]); // load the first module for display
    //            //crvm.step3activemoduletab(request.mappedrequestmodules[0]);
    //        },
    //        error: function (xhr, result, status) {
    //            alert(getajaxerrortext(xhr));
    //        }
    //    });
    //}

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
    matrVM.getModulos();
}

function loadCreateEditModuloCursoView() {
    $("#mainContainer").load('/ModuloCurso/create');
    matrVM.getModulo(0);
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

            //for (var j = 0; j < matrVM.modulo().Cursos.length; j++) {
            //    if (ui.item.CurName == matrVM.modulo().Cursos[j].CurName()) {
            //        return false;
            //    }
            //}

            matrVM.modulo().Cursos[matrVM.cursoPushIndex()].CurId   = ui.item.CurId;
            matrVM.modulo().Cursos[matrVM.cursoPushIndex()].CurName      = ui.item.CurName;
            matrVM.modulo().Cursos[matrVM.cursoPushIndex()].CurNumHoras  = ui.item.CurNumHoras;
            matrVM.modulo().Cursos[matrVM.cursoPushIndex()].CurPrecio    = ui.item.CurPrecio;
        }
    });

};