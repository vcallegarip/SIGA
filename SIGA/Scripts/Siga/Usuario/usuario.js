
var usuarioVM = null;
var usuarioViewModel = function(usuario) {
    usuarioVM = this;

    usuarioVM.saveCompleted = ko.observable(false);
    usuarioVM.sending = ko.observable(false);

    usuarioVM.isCreating = usuario.User_Id == 0;

    usuarioVM.usuario = {
        User_Id: usuario.User_Id,        
        User_Nombre: ko.observable(usuario.User_Nombre),
        Per_Nombre: ko.observable(usuario.Per_Nombre),
        Per_ApePaterno: ko.observable(usuario.Per_ApePaterno),
        Per_ApeMaterno : ko.observable(usuario.Per_ApeMaterno),
        Per_Email: ko.observable(usuario.Per_Email),
        Per_Cel: ko.observable(usuario.Per_Cel),
        Per_Tel: ko.observable(usuario.Per_Tel),
        TipoUser_Descrip: ko.observable(usuario.TipoUser_Descrip),
        User_Pass: ko.observable(usuario.User_Pass),
        Per_Dir: ko.observable(usuario.Per_Dir),
        Per_Dni: ko.observable(usuario.Per_Dni),
        Per_Id: ko.observable(usuario.Per_Id),
        Per_Sexo: ko.observable(usuario.Per_Sexo),

    };

    usuarioVM.validateAndSave = function (form) {
        if (!$(form).valid())
            return false;

        self.sending(true);

        self.author.__RequestVerificationToken = form[0].value;
     
        $.ajax({
            url: '/api/Usuario',
            type: (usuarioVM.isCreating) ? 'post' : 'put',
            contentType: 'application/json',
            data: ko.toJSON(usuarioVM.usuario)
        })
        .success(usuarioVM.successfulSave)
        .error(usuarioVM.errorSave)
        .complete(function () { usuarioVM.sending(false) });
    };

    usuarioVM.successfulSave = function () {
        usuarioVM.saveCompleted(true);

        $('.body-content').prepend('<div class="alert alert-success"><strong>Success!</strong> The author has been saved.</div>');
        setTimeout(function () {
            if (usuarioVM.isCreating)
                location.href = './';
            else
                location.href = '../';
        }, 1000);
    };

    usuarioVM.errorSave = function () {
        $('.body-content').prepend('<div class="alert alert-danger"><strong>Error!</strong> There was an error saving the author.</div>');
    };

    usuarioVM.ReturnStatus = ko.observable();
    
    usuarioVM.usuarioList = ko.observableArray([]);
    usuarioVM.tipoUsuarioClick = ko.observable();
    usuarioVM.tipoUsuarioItems = ko.observableArray([
        { name: 'Alumno' },
        { name: 'Profesor' },
        { name: 'Administrador' }
    ]);

    usuarioVM.getUsuarios = function () {
        getUsuarios();
    };

    //usuarioVM.resetFilters();
}

var searchInbox = function () {
    var searchParam = ($(".paf-menu-item.active").attr('data-id') == null || $(".paf-menu-item.active").attr('data-id') == "")
                        ? "" : "type:'" + $(".paf-menu-item.active").attr('data-id') + "'";

    var txtPrimerNombre = ($("#txtPrimerNombre").val() == null || $("#txtPrimerNombre").val() == "")
                        ? searchParam : $("#txtPrimerNombre").val();

    var txtApellidoPaterno = ($("#txtApellidoPaterno").val() == null || $("#txtApellidoPaterno").val() == "")
                        ? searchParam : $("#txtApellidoPaterno").val();

    var txtEmail = ($("#txtEmail").val() == null || $("#txtEmail").val() == "")
                        ? searchParam : $("#txtEmail").val();

    var cboTipoUsuario = ($("#cboTipoUsuario").val() == null || $("#cboTipoUsuario").val() == "")
                        ? searchParam : $("#cboTipoUsuario").val();


    var params = new Array();
    params.push("?primerNombre=" + encodeURI(txtPrimerNombre));
    params.push("apellidoPaterno=" + encodeURI(txtApellidoPaterno));
    params.push("email=" + encodeURI(txtEmail));
    params.push("tipoUSuario=" + encodeURI(cboTipoUsuario));

    $("#usuarioSubContainer").load('/usuario/UsuarioList' + params.join('&'));

}



//$(document).ready(function () {
//    usuarioVM = new usuarioViewModel();
//    ko.applyBindings(usuarioVM, $('#UsuarioContainer')[0]);
//});


//$(document).ready(function () {
//    viewModel = new usuarioViewModel(@Html.HtmlConvertToJson(Model));
//    ko.applyBindings(viewModel, $('#UsuarioCreateContainer')[0]);
//});