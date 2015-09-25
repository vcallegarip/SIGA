
var usuarioVM = null;
var usuarioViewModel = function (usuario) {

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

        // include the anti forgery token
        self.author.__RequestVerificationToken = form[0].value;

        $.ajax({
            url: '/api/Usuario',
            type: (self.isCreating) ? 'post' : 'put',
            contentType: 'application/json',
            data: ko.toJSON(self.author)
        })
        .success(self.successfulSave)
        .error(self.errorSave)
        .complete(function () { self.sending(false) });
    };

    usuarioVM.successfulSave = function () {
        self.saveCompleted(true);

        $('.body-content').prepend('<div class="alert alert-success"><strong>Success!</strong> The author has been saved.</div>');
        setTimeout(function () {
            if (self.isCreating)
                location.href = './';
            else
                location.href = '../';
        }, 1000);
    };

    usuarioVM.errorSave = function () {
        $('.body-content').prepend('<div class="alert alert-danger"><strong>Error!</strong> There was an error saving the author.</div>');
    };

    //usuarioVM.CurrentPageNumber = ko.observable();
    //usuarioVM.PageSize = ko.observable();
    //usuarioVM.User_ID = ko.observable();
    //usuarioVM.Per_Nombre = ko.observable();
    //usuarioVM.Per_ApellidoPaterno = ko.observable();
    //usuarioVM.Per_Email = ko.observable();
    //usuarioVM.TipoUser_Descrip = ko.observable();
    //usuarioVM.SortDirection = ko.observable();
    //usuarioVM.SortExpression = ko.observable();

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

function resetFilters () {
    $("#txtPrimerNombre").val("");
    $("#txtApellidoPaterno").val("");
    $("#txtEmail").val("");
    $("#cboTipoUsuario").val("All");
    InitializeData();
};


function UsuarioRequest() {
    this.CurrentPageNumber;
    this.PageSize;
    this.User_ID;
    this.Per_Nombre;
    this.Per_ApellidoPaterno;
    this.Per_Email;
    this.TipoUser_Descrip;
    this.SortDirection;
    this.SortExpression;
    this.ReturnStatus;
    this.usuarioList;
};

function getUsuarios() {
    
    var url = "/Usuario/Index";
    
    var usuarioRequest = new UsuarioRequest();

    usuarioRequest.User_ID = "";
    usuarioRequest.Per_Nombre = $("#txtPrimerNombre").val();
    usuarioRequest.Per_ApellidoPaterno = $("#txtApellidoPaterno").val();
    usuarioRequest.Per_Email = $("#txtEmail").val();
    usuarioRequest.TipoUser_Descrip = $("#cboTipoUsuario").val();
    usuarioRequest.CurrentPageNumber = 1;
    usuarioRequest.PageSize = 15;

    $.post(url, usuarioRequest, function (data, textStatus) {
        GetUsuariosComplete(data);

    });

};



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


//function loadPartialList(pageNumber) {
//    var pageNumber = pageNumber || 1;
//    var pageSize = $('#selectListPageSize option:selected').text();
//    var showOption = ($('#chkIncludeClosedItem').is(':checked')) ? "Closed" : "Open";
//    var params = new Array();
//    params.push("?filterDesc=" + encodeURIComponent(searchParams));//searchParams comes from ViewBag declared in the script of razor
//    params.push("pageNumber=" + pageNumber);
//    params.push("pageSize=" + pageSize);
//    params.push("showOption=" + showOption);
//    loadBusyImageTab("inboxMain");
//    $("#inboxSubContainer").load('/Inbox/MainInboxList' + params.join('&'), function (response, status, xhr) {
//        unloadBusyImageTab("inboxMain");
//    });
//}

//$(document).ready(function () {
//    getUsuarios();
//    usuarioVM = new usuarioViewModel();
//    ko.applyBindings(usuarioVM, $('#UsuarioContainer')[0]);
//});
