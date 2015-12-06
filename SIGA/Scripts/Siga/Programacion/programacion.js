var progVM = null;

function programacionViewModel(programacion) {
    
    progVM = this;

    progVM.deleteProgId = ko.observable(0);

    progVM.programacionList = ko.observableArray([]);

    progVM.showActionResponseContent = ko.observable(false);

    progVM.txtProgNombre = ko.observable('');
    progVM.txtModuloNombre = ko.observable('');

    progVM.searchProgramacion = function () {
        
        progVM.txtProgNombre($("#txtProgNombre").val() == "" || $("#txtProgNombre").val() == null ? "" : $("#txtProgNombre").val());
        progVM.txtModuloNombre($("#txtModuloNombre").val() == "" || $("#txtModuloNombre").val() == null ? "" : $("#txtModuloNombre").val());
        
        var params = new Array();
        params.push("?progNombre=" + encodeURI(progVM.txtProgNombre()));
        params.push("moduloNombre=" + encodeURI(progVM.txtModuloNombre()));

        $("#programacionSubContainer").load('/programacion/ProgramacionList' + params.join('&'));

    };

    progVM.createProgramacion = function () {
        LoadCreateViewForProgramacion();
    };

    progVM.saveCompleted = ko.observable(false);

    progVM.sending = ko.observable(false);
    
    progVM.isCreating = programacion.ProgramacionItem.Prog_Id == 0;

    progVM.programacionData = {
        ProgramacionItem: {
            Prog_Id : programacion.ProgramacionItem.Prog_Id,
            ModId : ko.observable(programacion.ProgramacionItem.ModId),
            AulId : ko.observable(programacion.ProgramacionItem.AulId),
            HorId : ko.observable(programacion.ProgramacionItem.HorId),
            ProgNombre : ko.observable(programacion.ProgramacionItem.ProgNombre),
            ProgDescripcion: ko.observable(programacion.ProgramacionItem.ProgDescripcion),
            ProgFechaRegistro: ko.observable(programacion.ProgramacionItem.ProgFechaRegistro),
            ProgFechaInicio: ko.observable(programacion.ProgramacionItem.ProgFechaInicio),
            ProgFechaFin: ko.observable(programacion.ProgramacionItem.ProgFechaFin),
            EsVigente: ko.observable(programacion.ProgramacionItem.EsVigente),

            //AulaItem : {
            //    AulId : ko.observable(programacion.AulaItem.AulId),
            //    AulCapacidad : ko.observable(programacion.AulaItem.AulCapacidad)
            //},
            //HorarioItem : {
            //    HorId : ko.observable(programacion.HorarioItem.HorId),
            //    HorTurno : ko.observable(programacion.HorarioItem.HorTurno),
            //    HorDia : ko.observable(programacion.HorarioItem.HorTurno),
            //    HorHoraIni : ko.observable(programacion.HorarioItem.HorHoraIni),
            //    HorHoraFin : ko.observable(programacion.HorarioItem.HorHoraFin)
            //},
            //ModuloItem : {
            //    ModId : ko.observable(programacion.ModuloItem.ModId),
            //    ModCatId : ko.observable(programacion.ModuloItem.ModCatId),
            //    ModNivelId : ko.observable(programacion.ModuloItem.ModNivelId),
            //    ModNombre : ko.observable(programacion.ModuloItem.ModNombre),
            //    ModNumHoras : ko.observable(programacion.ModuloItem.ModNumHoras),
            //    ModNumMes : ko.observable(programacion.ModuloItem.ModNumMes),
            //    ModNumCursos : ko.observable(programacion.ModuloItem.ModNumCursos)
            //}
        }
    }


    progVM.validateAndSave = function (form) {
        if (!$(form).valid())
            return false;
        progVM.sending(true);

        $.ajax({
            url: '/api/programacion',
            type: (progVM.isCreating) ? 'Post' : 'Put',
            contentType: 'application/json',
            data: ko.toJSON(progVM.programacionData)
        }) 
        .success(progVM.successfulSave) 
        .error(progVM.errorSave)
        .complete(function () { progVM.sending(false) });
    };

    progVM.successfulSave = function () {
        progVM.saveCompleted(true);
        
        $('.body-content').prepend('<div class="alert alert-success"><strong>Exito!</strong> La programacion fue guardado con exito.</div>');
        setTimeout(function () {
            if (progVM.isCreating)
                //location.href = '/';
                loadProgramacion();
            else
                loadProgramacion();
        }, 1000);
    };

    progVM.errorSave = function () {
        $('.body-content').prepend('<div class="alert alert-danger"><strong>Error!</strong> Se produjo un error al guardar los datos de la programacion</div>');
    };

    progVM.userIdToDelete = 0;

    progVM.showProgramacionDeleteModal = function (userid) {
        progVM.userIdToDelete = userid;
        showConfirmDialog(progVM.programacionDelete, "Confirmacion", "Estas seguro que quieres eliminar esta programacion?", "Si", "Cancelar", null);
        return false;
    }

    progVM.programacionDelete = function () {
        var URL = '/programacion/Delete?progid=' + progVM.userIdToDelete;
        $.ajax(
        {
            url: URL,
            type: "Delete",
            data: '',
            async: true,
            success: function (data, textStatus, jqXHR) {
                progVM.searchProgramacion();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(getAjaxErrorText(xhr));
            }
        });
    }

    progVM.getCursoPorModulo = function () {
        var URL = '/programacion/cursopormodulo?progid=' + progVM.ProgramacionItem.ModId;
        $.ajax(
        {
            url: URL,
            type: "Get",
            data: '',
            async: true,
            success: function (data, textStatus, jqXHR) {
                progVM.searchProgramacion();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(getAjaxErrorText(xhr));
            }
        });
    }

}

function resetFilters() {
    progVM.searchProgramacion();
    return;
}

function LoadCreateViewForProgramacion() {
    $("#mainContainer").load('/programacion/create');
    return;
}

function resetFilters() {
    $('#txtProgNombre').val('');
    $('#txtModuloNombre').val('');
    progVM.searchProgramacion();
    return;
}

function editProgramacion(progid) {
    $("#mainContainer").load('/programacion/Edit?progid=' + progid);
    return;
}

function getDetailsProgramacion(progid) {
    $("#mainContainer").load('/programacion/GetDetails?progid=' + progid);
    return;
}

function loadModuleCurso(data) {
    var params = new Array();
    params.push("?modNombre=" + encodeURI(data));
    $("#programacionCursoSubContainer").load('/programacion/CursoPorModuloList' + params.join('&'));
}