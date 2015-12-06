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

    usuarioVM.txtPrimerNombre = ko.observable('');
    usuarioVM.txtApellidoPaterno = ko.observable('');
    usuarioVM.txtEmail = ko.observable('');
    usuarioVM.cboTipoUsuario = ko.observable('Todos');

    usuarioVM.searchUsuario = function () {
        usuarioVM.txtPrimerNombre($("#txtPrimerNombre").val());
        usuarioVM.txtApellidoPaterno($("#txtApellidoPaterno").val());
        usuarioVM.txtEmail($("#txtEmail").val());
        usuarioVM.cboTipoUsuario($("#cboTipoUsuario").val() == "" ? "Todos" : $("#cboTipoUsuario").val());

        var params = new Array();
        params.push("?primerNombre=" + encodeURI(usuarioVM.txtPrimerNombre()));
        params.push("apellidoPaterno=" + encodeURI(usuarioVM.txtApellidoPaterno()));
        params.push("email=" + encodeURI(usuarioVM.txtEmail()));
        params.push("tipoUSuario=" + encodeURI(usuarioVM.cboTipoUsuario()));

        $("#usuarioSubContainer").load('/usuario/UsuarioList' + params.join('&'));

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
            AlumnoItem: {
                Alu_FechaIngreso: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_FechaIngreso),
                Alu_FechaRegistro: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_FechaRegistro),
                Alu_Apoderado: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_Apoderado),
            },
            ProfesorItem: {
                Cur_Id: ko.observable(usuario.UsuarioItem.ProfesorItem.Cur_Id),
                Prof_Especialidad: ko.observable(usuario.UsuarioItem.ProfesorItem.Prof_Especialidad),
                Prof_Procedencia: ko.observable(usuario.UsuarioItem.ProfesorItem.Prof_Procedencia),
                Prof_LugarEstudio: ko.observable(usuario.UsuarioItem.ProfesorItem.Prof_LugarEstudio),
            }
        }
    }

    usuarioVM.usuarioAlumnoData = {
        UsuarioItem: {
            User_Id: usuarioVM.usuarioData.UsuarioItem.User_Id, // usuario.UsuarioItem.User_Id,
            Per_Nombre: usuarioVM.usuarioData.UsuarioItem.Per_Nombre, //ko.observable(usuario.UsuarioItem.Per_Nombre),
            Per_ApePaterno: usuarioVM.usuarioData.UsuarioItem.Per_ApePaterno, //ko.observable(usuario.UsuarioItem.Per_ApePaterno),
            Per_ApeMaterno: usuarioVM.usuarioData.UsuarioItem.Per_ApeMaterno, //ko.observable(usuario.UsuarioItem.Per_ApeMaterno),
            Per_Email: usuarioVM.usuarioData.UsuarioItem.Per_Email, //ko.observable(usuario.UsuarioItem.Per_Email),
            Per_Dni: usuarioVM.usuarioData.UsuarioItem.Per_Dni, //ko.observable(usuario.UsuarioItem.Per_Dni),
            Per_Dir: usuarioVM.usuarioData.UsuarioItem.Per_Dir, //ko.observable(usuario.UsuarioItem.Per_Dir),
            Per_Cel: usuarioVM.usuarioData.UsuarioItem.Per_Cel, //ko.observable(usuario.UsuarioItem.Per_Cel),
            Per_Tel: usuarioVM.usuarioData.UsuarioItem.Per_Tel, //ko.observable(usuario.UsuarioItem.Per_Tel),
            Per_Sexo: usuarioVM.usuarioData.UsuarioItem.Per_Sexo, //ko.observable(usuario.UsuarioItem.Per_Sexo),
            User_Nombre: usuarioVM.usuarioData.UsuarioItem.User_Nombre, //ko.observable(usuario.UsuarioItem.User_Nombre),
            TipoUser_Descrip: usuarioVM.usuarioData.UsuarioItem.TipoUser_Descrip, //ko.observable(usuarioVM.tipoUsuarioSelected),
            AlumnoItem : {
                Alu_FechaIngreso: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_FechaIngreso),
                Alu_FechaRegistro: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_FechaRegistro),
                Alu_Apoderado: ko.observable(usuario.UsuarioItem.AlumnoItem.Alu_Apoderado),
            }
        }
    }


    usuarioVM.usuarioProfesorData = {
        UsuarioItem: {
            User_Id: usuarioVM.usuarioData.UsuarioItem.User_Id, // usuario.UsuarioItem.User_Id,
            Per_Nombre: usuarioVM.usuarioData.UsuarioItem.Per_Nombre, //ko.observable(usuario.UsuarioItem.Per_Nombre),
            Per_ApePaterno: usuarioVM.usuarioData.UsuarioItem.Per_ApePaterno, //ko.observable(usuario.UsuarioItem.Per_ApePaterno),
            Per_ApeMaterno: usuarioVM.usuarioData.UsuarioItem.Per_ApeMaterno, //ko.observable(usuario.UsuarioItem.Per_ApeMaterno),
            Per_Email: usuarioVM.usuarioData.UsuarioItem.Per_Email, //ko.observable(usuario.UsuarioItem.Per_Email),
            Per_Dni: usuarioVM.usuarioData.UsuarioItem.Per_Dni, //ko.observable(usuario.UsuarioItem.Per_Dni),
            Per_Dir: usuarioVM.usuarioData.UsuarioItem.Per_Dir, //ko.observable(usuario.UsuarioItem.Per_Dir),
            Per_Cel: usuarioVM.usuarioData.UsuarioItem.Per_Cel, //ko.observable(usuario.UsuarioItem.Per_Cel),
            Per_Tel: usuarioVM.usuarioData.UsuarioItem.Per_Tel, //ko.observable(usuario.UsuarioItem.Per_Tel),
            Per_Sexo: usuarioVM.usuarioData.UsuarioItem.Per_Sexo, //ko.observable(usuario.UsuarioItem.Per_Sexo),
            User_Nombre: usuarioVM.usuarioData.UsuarioItem.User_Nombre, //ko.observable(usuario.UsuarioItem.User_Nombre),
            TipoUser_Descrip: usuarioVM.usuarioData.UsuarioItem.TipoUser_Descrip, //ko.observable(usuarioVM.tipoUsuarioSelected),
            ProfesorItem: {
                Cur_Id: ko.observable(usuario.UsuarioItem.ProfesorItem.Cur_Id),
                Prof_Especialidad: ko.observable(usuario.UsuarioItem.ProfesorItem.Prof_Especialidad),
                Prof_Procedencia: ko.observable(usuario.UsuarioItem.ProfesorItem.Prof_Procedencia),
                Prof_LugarEstudio: ko.observable(usuario.UsuarioItem.ProfesorItem.Prof_LugarEstudio),
            }
        }
    }
    

    usuarioVM.validateAndSave = function (form) {
        //if (!$(form).valid())
        //    return false;
        usuarioVM.sending(true);
        
        if (usuarioVM.tipoUsuarioSelected() == "Alumno") {
            
            $.ajax({
                url: '/api/usuario',
                type: (usuarioVM.isCreating) ? 'Post' : 'Put',
                contentType: 'application/json',
                data: ko.toJSON(usuarioVM.usuarioAlumnoData)
            })
        .success(usuarioVM.successfulSave)
        .error(usuarioVM.errorSave)
        .complete(function () { usuarioVM.sending(false) });
        }

        if (usuarioVM.tipoUsuarioSelected() == "Profesor") {
            
            $.ajax({
                url: '/api/usuario',
                type: (usuarioVM.isCreating) ? 'Post' : 'Put',
                contentType: 'application/json',
                data: ko.toJSON(usuarioVM.usuarioProfesorData)
            })
        .success(usuarioVM.successfulSave)
        .error(usuarioVM.errorSave)
        .complete(function () { usuarioVM.sending(false) });
        }

        
    };

    usuarioVM.successfulSave = function () {
        usuarioVM.saveCompleted(true);
        
        $('.body-content').prepend('<div class="alert alert-success"><strong>Exito!</strong> El Usuario fue guardado con exito.</div>');
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

    usuarioVM.userIdToDelete = 0;

    usuarioVM.showUsuarioDeleteModal = function (userid) {
        usuarioVM.userIdToDelete = userid;
        showConfirmDialog(usuarioVM.usuarioDelete, "Confirmacion", "Estas seguro que quieres eliminar este usuario?", "Ok", "Cancel", null);
        return false;
    }

    usuarioVM.usuarioDelete = function () {
        var URL = '/usuario/Delete?userid=' + usuarioVM.userIdToDelete;
        $.ajax(
        {
            url: URL,
            type: "Delete",
            data: '',
            async: true,
            success: function (data, textStatus, jqXHR) {
                usuarioVM.searchUsuario();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(getAjaxErrorText(xhr));
            }
        });
    }

}


function activeUserTabOnDetail(data) {
    usuarioVM.setTipoUsuarioSelected(data);
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
    usuarioVM.searchUsuario();
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
