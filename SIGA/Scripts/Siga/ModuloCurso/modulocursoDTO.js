
var Modulo = function (data) {
    this.ModId = data.ModId || '';
    this.ModCategroria = data.ModCategroria || '';
    this.ModNivel = data.ModNivel || '';
    this.ModNombre = data.ModNombre || '';
    this.ModNumHoras = data.ModNumHoras || '';
    this.ModNumMes = data.ModNumMes || '';
    this.ModNumCursos = data.ModNumCursos || '';
    this.Cursos = $.map(data.Cursos, function (item) { return new Curso(item) });
    return this;
}
var Curso = function (data) {
    this.CurId = ko.observable(data.CurId || '');
    this.CurName = ko.observable(data.CurName || '');
    this.CurNumHoras = ko.observable(data.CurNumHoras || '');
    this.CurPrecio = ko.observable(data.CurPrecio || '');
    //this.SelectedEvent =  data.SelectedEvent || false);
}