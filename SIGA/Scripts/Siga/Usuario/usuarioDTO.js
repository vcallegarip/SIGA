
var UsuarioItem = function (data) {

    this.user_Id = data.user_Id || '';
    this.per_Dni = data.per_Dni || '';
    this.per_Nombre = data.per_Nombre || '';
    this.per_ApePaterno = data.per_ApePaterno || '';
    this.per_ApeMaterno = data.per_ApeMaterno || '';
    this.per_Sexo = data.per_Sexo || '';
    this.per_Dir = data.per_Dir || '';
    this.per_Cel = data.per_Cel || '';
    this.per_Tel = data.per_Tel || '';
    this.per_Email = data.per_Email || '';
    this.user_Nombre = data.user_Nombre || '';
    this.tipoUser_Descrip = data.tipoUser_Descrip || '';
    //this.AlumnoInfo = $.map(data.AlumnoInfo, function (item) { return new AlumnoInfo(item) });
}


var AlumnoInfo = function (data) {
    this.User_Id = ko.observable(data.user_Id || '');
    this.Alu_FechNac = data.alu_FechNac || '';
    this.Alu_Apoderado = data.alu_Apoderado || '';
    this.Alu_FechIngreso = data.alu_FechIngreso || '';
}




//var UsuarioItem = function (data) {

//    this.User_Id = ko.observable(data.user_Id || '');
//    this.Per_Dni = ko.observable(data.per_Dni || '');
//    this.Per_Nombre = ko.observable(data.per_Nombre || '');
//    this.Per_ApePaterno = ko.observable(data.per_ApePaterno || '');
//    this.Per_ApeMaterno = ko.observable(data.per_ApeMaterno || '');
//    this.Per_Sexo = ko.observable(data.per_Sexo || '');
//    this.Per_Dir = ko.observable(data.per_Dir || '');
//    this.Per_Cel = ko.observable(data.per_Cel || '');
//    this.Per_Tel = ko.observable(data.per_Tel || '');
//    this.Per_Email = ko.observable(data.per_Email || '');
//    this.User_Nombre = ko.observable(data.user_Nombre || '');
//    this.TipoUser_Descrip = ko.observable(data.tipoUser_Descrip || '');
//    //this.AlumnoInfo = $.map(ko.observable(data.AlumnoInfo, function (item) { return new AlumnoInfo(item) });
//}


//var AlumnoInfo = function (data) {
//    this.User_Id = ko.observable(ko.observable(data.user_Id || '');
//    this.Alu_FechNac = ko.observable(data.alu_FechNac || '');
//    this.Alu_Apoderado = ko.observable(data.alu_Apoderado || '');
//    this.Alu_FechIngreso = ko.observable(data.alu_FechIngreso || '');
//}
