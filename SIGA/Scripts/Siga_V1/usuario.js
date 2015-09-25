
var usuarioVM = null;
var usuarioViewModel = function (data) {
    usuarioVM = this;
    usuarioVM.search_PrimerNombre = ko.observable('');  // required to trigger the search when changes occur
    usuarioVM.search_ApellidoPaterno = ko.observable('');
    usuarioVM.search_Email = ko.observable('');
    usuarioVM.UsuarioInfo = ko.observableArray([]);
    usuarioVM.tipoUsuarioClick = ko.observable();
    usuarioVM.tipoUsuarioItems = ko.observableArray([
        { name: 'Alumno' },
        { name: 'Profesor' },
        { name: 'Administrador' }
    ]);

    usuarioVM.tipoUsuarioClick = function (data, event) {
        if (data.name == "Alumno") {
            
            $('#dataUsuarioContainer').load("/Usuario/Alumno", function () {
                window.history.pushState(null, 'Alumno', '/Usuario/Alumno')
            });
        }
        else if (data.name == "Profesor") {

            $('#dataUsuarioContainer').load("/Usuario/Profesor", function () {
                
                //usuarioVM.Profesor = ko.observable(new ProfesorModel());
                //window.history.pushState(null, 'Profesor','/Usuario/Profesor')
            });
        }
        else if (data.name == "Administrador") {
            $('#dataUsuarioContainer').load("/Usuario/Alumno", function () {
                window.history.pushState(null, 'Administrador', '/Usuario/Administrador')
            });
        }
    };

    if (data != null) {
        ko.mapping.fromJS(data, { UsuarioInfo: usuarioMapping }, self);
    }

    usuarioVM.getUsuariosFromServer = function () {
        $.ajax("/Usuario/ListarUsuarios/", {
            type: "GET",
            cache: false,
        }).done(function (jsondata) {
            var jobj = $.parseJSON(jsondata);
            ko.mapping.fromJS(jobj, { UsuarioInfo: usuarioMapping }, self);
        });
    }

    usuarioVM.filteredRecords = ko.computed(function () {

        return ko.utils.arrayFilter(usuarioVM.UsuarioInfo(), function (rec) {
            return (
                        (usuarioVM.search_PrimerNombre().length == 0 ||
                            rec.Per_Nombre().toLowerCase().indexOf(usuarioVM.search_PrimerNombre().toLowerCase()) > -1)
                        &&
                        (usuarioVM.search_ApellidoPaterno().length == 0 ||
                            rec.Per_ApePaterno().toLowerCase().indexOf(usuarioVM.search_ApellidoPaterno().toLowerCase()) > -1)
                        &&
                        (usuarioVM.search_Email().length == 0 ||
                            rec.Per_Email().toLowerCase().indexOf(usuarioVM.search_Email().toLowerCase()) > -1)
                    )
        })
    });

}

var UsuarioInfo = function (data) {
    var self = this;
    if (data != null) {
        self.User_Id = ko.observable(data.User_Id);
        self.User_Nombre = ko.observable(data.User_Nombre);
        self.Per_Nombre = ko.observable(data.Per_Nombre);
        self.Per_ApePaterno = ko.observable(data.Per_ApePaterno);
        self.Per_ApeMaterno = ko.observable(data.Per_ApeMaterno);
        self.Per_Email = ko.observable(data.Per_Email);
        self.Per_Cel = ko.observable(data.Per_Cel);
        self.Per_Tel = ko.observable(data.Per_Tel);
        self.TipoUser_Descrip = ko.observable(data.TipoUser_Descrip);
    }
}

var usuarioMapping = {
    create: function (options) {
        return new UsuarioInfo(options.data);
    }
};



$(document).ready(function () {
    usuariovm = new usuarioviewmodel();
    ko.applybindings(usuariovm, $('#usuariocontainer')[0]);
});