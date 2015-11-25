var mcVM = null;

function modulocursoViewModel() {

    mcVM = this;

    mcVM.nombre = ko.observable('tato');

    mcVM.modulos = ko.observableArray([]);
   
    

    mcVM.getModulos = function () {
        
        $.ajax({
            url: "/api/ModuloCurso",
            dataType: "Json",
            data: '',
            success: function (data) {
                var modulos = $.map(data, function (item) { return new Modulo(item) });
                mcVM.modulos(modulos);
            },
            error: function (xhr, result, status) {
                alert(getAjaxErrorText(xhr));
            }
        });
    }
}


$(document).ready(function () {
    ko.applyBindings(modulocursoViewModel, $('#moduloCursoSubContainer')[0]);
});

function getModulos() {
    mcVM.getModulos();
}