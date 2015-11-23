
var Modulo = function (data) {
    this.ModId = data.ModId;
    this.ModCategroria = data.ModCategroria;
    this.ModNivel = data.ModNivel;
    this.ModNombre = data.ModNombre;
    this.ModNumHoras = data.ModNumHoras;
    this.ModNumMes = data.ModNumMes;
    this.ModNumCursos = data.ModNumCursos;
    this.Cursos = $.map(data.Cursos, function (item) { return new Curso(item) });
    return this;
}
var Curso = function (data) {
    this.CurId = data.CurId;
    this.CurName = data.CurName;
    this.CurNumHoras = data.CurNumHoras;
    this.CurPrecio = data.CurPrecio;
    //this.SelectedEvent =  data.SelectedEvent || false);
}