var usuarioVM = null;
function usuarioViewModel(usuario) {
    usuarioVM = this;
    
    usuarioVM.ReturnStatus = ko.observable();
    
    usuarioVM.usuarioList = ko.observableArray([]);
    usuarioVM.tipoUsuarioClick = ko.observable();
    usuarioVM.tipoUsuarioItems = ko.observableArray([
        { name: 'Alumno' },
        { name: 'Profesor' },
        { name: 'Administrador' }
    ]);

    usuarioVM.searchUsuario = function () {
        searchUsuario();
    };

    usuarioVM.createUsuario = function () {
        createUsuario();
    };


    usuarioVM.saveCompleted = ko.observable(false);
    usuarioVM.sending = ko.observable(false);
    usuarioVM.isCreating = usuario.id == 0;

    usuarioVM.usuario = {
        id: usuario.id,
        //per_nombre: ko.observable('Percy'),
        //per_apepaterno: ko.observable('Magallanes'),
        //per_apematerno: ko.observable('Garcia'),
        //per_email: ko.observable('percym@gmail.com'),
        //per_dni: ko.observable(978654),
        //per_dir: ko.observable('Los Olivos'),
        //per_cel: ko.observable(987654321),
        //per_tel: ko.observable(987654321),
        //per_sexo: ko.observable('M'),
        //tipouser_descrip: ko.observable('Alumno'),

        per_nombre: ko.observable(usuario.per_nombre),
        per_apepaterno: ko.observable(usuario.per_apepaterno),
        per_apematerno: ko.observable(usuario.per_apematerno),
        per_email: ko.observable(usuario.per_email),
        per_dni: ko.observable(usuario.per_dni),
        per_dir: ko.observable(usuario.per_dir),
        per_cel: ko.observable(usuario.per_cel),
        per_tel: ko.observable(usuario.per_tel),
        per_sexo: ko.observable(usuario.per_sexo),
        tipouser_descrip: ko.observable(usuario.tipouser_descrip),

    };
    
    usuarioVM.validateAndSave = function (form) {
        
        if (!$(form).valid())
            return false;

        usuarioVM.sending(true);
        //usuarioVM.usuario.__RequestVerificationToken = form[0].value;

        $.ajax({
            url: '/api/usuario',
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

    usuarioVM.resetFilters = function () {
        resetFilters();
    };

    usuarioVM.editUsuario = function (userid) {
        editUsuario(userid)
    };

}

function searchUsuario() {
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

function createUsuario() {
    $("#mainContainer").load('/usuario/create');
    return;
}

function resetFilters() {
    $('#txtPrimerNombre').val('');
    $('#txtApellidoPaterno').val('');
    $('#txtEmail').val('');
    $('#cboTipoUsuario').val('Todos');
    searchUsuario();
    return;
}

function editUsuario(userid) {
    $("#mainContainer").load('/usuario/edit?userid=' + userid);
    return;
}