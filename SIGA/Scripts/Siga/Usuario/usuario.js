var usuarioVM = null;

function usuarioViewModel(usuario) {
    
    usuarioVM = this;

    usuarioVM.ReturnStatus = ko.observable();

    usuarioVM.nombre = ko.observable('Binding is working');

    usuarioVM.tipoUsuarioSelected = ko.observable();

    usuarioVM.setTipoUsuarioSelected = function (data) {
        usuarioVM.tipoUsuarioSelected(data);
        $('#li' + data + '').addClass('active');
        $('#li' + data + ' a').css('color', '#2c7ecc', 'font-weight','bold');
        $('#tipoUsuarioPanel li').not('#li' + data + '').removeClass('active');
        $('#tipoUsuarioPanel li a').not('#li' + data + ' a').css('color', '#454242');
    }

    usuarioVM.deleteUserId = ko.observable(0);

    usuarioVM.usuarioList = ko.observableArray([]);

    usuarioVM.showActionResponseContent = ko.observable(false);

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
    
    usuarioVM.isCreating = usuario.UsuarioItem.User_Id == 0;
 
    activeUserTabOnDetail(usuario.UsuarioItem.TipoUser_Descrip);

    usuarioVM.usuarioData = {
        UsuarioItem: {
            User_Id: usuario.UsuarioItem.User_Id,
            Per_Nombre: ko.observable(usuario.UsuarioItem.Per_Nombre),
            Per_ApePaterno: ko.observable(usuario.UsuarioItem.Per_ApePaterno),
            Per_ApeMaterno: ko.observable(usuario.UsuarioItem.Per_ApeMaterno),
            Per_Email: ko.observable(usuario.UsuarioItem.Per_Email),
            Per_Dni: ko.observable(usuario.UsuarioItem.Per_Dni),
            Per_Dir: ko.observable(usuario.UsuarioItem.Per_Dir),
            Per_Cel: ko.observable(usuario.UsuarioItem.Per_Cel),
            Per_Tel: ko.observable(usuario.UsuarioItem.Per_Tel),
            Per_Sexo: ko.observable(usuario.UsuarioItem.Per_Sexo),
            User_Nombre: ko.observable(usuario.UsuarioItem.User_Nombre),
            TipoUser_Descrip: ko.observable(usuarioVM.tipoUsuarioSelected),
            AlumnoItem : {
                Alu_FechaIngreso: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_FechaIngreso),
                Alu_FechaRegistro: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_FechaRegistro),
                Alu_Apoderado: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_Apoderado),
            }
        }
    }

    usuarioVM.validateAndSave = function (form) {
        if (!$(form).valid())
            return false;
        usuarioVM.sending(true);

        $.ajax({
            url: '/api/usuario',
            type: (usuarioVM.isCreating) ? 'Post' : 'Put',
            contentType: 'application/json',
            data: ko.toJSON(usuarioVM.usuarioData)
        }) 
        .success(usuarioVM.successfulSave) 
        .error(usuarioVM.errorSave)
        .complete(function () { usuarioVM.sending(false) });
    };

    usuarioVM.successfulSave = function () {
        usuarioVM.saveCompleted(true);
        
        $('.body-content').prepend('<div class="alert alert-success"><strong>Success!</strong> El Usuario fue guardado con exito.</div>');
        setTimeout(function () {
            if (usuarioVM.isCreating)
                //location.href = '/';
                loadUsuario();
            else
                loadUsuario();
        }, 1000);
    };

    usuarioVM.errorSave = function () {
        $('.body-content').prepend('<div class="alert alert-danger"><strong>Error!</strong> Se produjo un error al guardar los datos del usuario</div>');
    };

    usuarioVM.showUsuarioDeleteModal = function (userid) {
        showConfirmDialog(usuarioDelete(userid), "Confirmacion", "Estas seguro que quieres eliminar este usuario?", "Aceptar", "Cancelar", null);
        return false;
    }

    usuarioVM.usuarioDelete = function (userid) {
        var URL = '/usuario/Delete?userid=' + userid;
        $.ajax(
        {
            url: URL,
            type: "Delete",
            data: '',
            async: true,
            success: function (data, textStatus, jqXHR) {
                searchUsuario();
                $("#mainContainer").html(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                searchUsuario();
                alert(getAjaxErrorText(xhr));
            }
        });
    }

}


function activeUserTabOnDetail(data) {
    usuarioVM.setTipoUsuarioSelected(data);
}

function searchUsuario() {

    var txtPrimerNombre = ($("#txtPrimerNombre").val() == null || $("#txtPrimerNombre").val() == "")
                        ? "" : $("#txtPrimerNombre").val();

    var txtApellidoPaterno = ($("#txtApellidoPaterno").val() == null || $("#txtApellidoPaterno").val() == "")
                        ? "" : $("#txtApellidoPaterno").val();

    var txtEmail = ($("#txtEmail").val() == null || $("#txtEmail").val() == "")
                        ? "" : $("#txtEmail").val();

    var cboTipoUsuario = ($("#cboTipoUsuario").val() == null || $("#cboTipoUsuario").val() == "")
                        ? "Todos" : $("#cboTipoUsuario").val();
    
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

function getDetailsUsuario(userid) {
    $("#mainContainer").load('/Usuario/GetDetails?userid=' + userid);
    return;
}
