
var UsuarioItem = function (data) {
    this.User_Id = ko.observable(data.User_Id || 0);
    this.Per_Nombre = data.Per_Nombre || '';
    this.Per_ApePaterno = ko.observable(data.Per_ApePaterno || '');
    this.Per_ApeMaterno = ko.observable(data.Per_ApeMaterno || '');
    this.Per_Email = ko.observable(data.Per_Email || '');
    this.Per_Dni = ko.observable(data.Per_Dni || '');
    this.Per_Dir = ko.observable(data.Per_Dir || '');
    this.Per_Cel = ko.observable(data.Per_Cel || '');
    this.Per_Tel = ko.observable(data.Per_Tel || '');
    this.Per_Sexo = ko.observable(data.Per_Sexo || '');
    this.User_Nombre = ko.observable(data.User_Nombre || '');
    this.Tipouser_Descrip = ko.observable(data.Tipouser_Descrip || '');
    this.AlumnoItem = new AlumnoItem(data) || '';
    
    //this.AlumnoItem = typeof (data.AlumnoItem) == 'undefined' ? [] : $.map(data.AlumnoItem, function (item) { return new AlumnoItem(item) });
    //this.AlumnoItem = $.map(data.AlumnoItem, function (item) { return new AlumnoItem(item) });
    //this.AlumnoItem = typeof (data.AlumnoItem) == 'undefined' ? [] : $.map(data.AlumnoItem, function (item) { return new AlumnoItem(item) });
}


var AlumnoItem = function (data) {
    //this.User_Id = data.User_Id || '';
    this.Alu_FechaIngreso = ko.observable(data.Alu_FechaIngreso || '');
    this.Alu_FechaRegistro = ko.observable(data.Alu_FechaRegistro || '');
    this.Alu_Apoderado = ko.observable(data.Alu_Apoderado || '');
}
