var usuarioVM = null;
function usuarioViewModel(usuario) {
    
    usuarioVM = this;
    //usuarioVM.usuarioInfo = ko.observable(new UsuarioInfo(''));
    usuarioVM.ReturnStatus = ko.observable();
    usuarioVM.nombre = ko.observable('Victor');

    //usuarioVM.usuarioItem = ko.observable(new UsuarioItem(usuario.usuarioItem));
    
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
        LoadCreateViewForUsuario();
    };

    usuarioVM.saveCompleted = ko.observable(false);
    usuarioVM.sending = ko.observable(false);
    usuarioVM.isCreating = 0 == 0;

    usuarioVM.usuario = {
        usuarioItem : {
            user_id: usuario.user_id,
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
        }
    };
    
    usuarioVM.validateAndSave = function (form) {
        if (!$(form).valid())
            return false;

        usuarioVM.sending(true);
        $.ajax({
            url: '/api/usuario',
            type: (usuarioVM.isCreating) ? 'Post' : 'Put',
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
        editUsuario(userid);
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

function LoadCreateViewForUsuario() {
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
    $("#mainContainer").load('/Usuario/Edit?userid=' + userid);
    return;
}

function saveNewUsuario() {
    debugger
    $.ajax({
        url: '',
        type: 'POST',
        cache: false,
        data: jQuery("#UsuarioCreateForm").serialize(),
        success: function (result) {
            // do accordingly as per your result                 
        }
    });
}


