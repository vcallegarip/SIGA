using Newtonsoft.Json;
using SIGA_Model;
using SIGA_Model.StoredProcContexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGA.Models.ViewModels
{
    public class ModuloDTO
    {
        public int ModId { get; set; }
        public string ModNivel { get; set; }
        public string ModCategroria { get; set; }

        [Required(ErrorMessage = "Apellido Paterno es requerido.")]
        public string ModNombre { get; set; }
        public int ModNumHoras { get; set; }
        public int ModNumMes { get; set; }
        public int ModNumCursos { get; set; }
        public List<CursoDTO> Cursos { get; set; }
    }

    public class CursoDTO
    {
        public int CurId { get; set; }
        //public int CurId
        //{
        //    get
        //    {
        //        if (_curId == null) _curId = 0;
        //        return _curId;
        //    }

        //    set
        //    {
        //        _curId = value;
        //    }
        //}

        public string CurName { get; set; }
        //public string CurName
        //{
        //    get
        //    {
        //        if (_curName == null) _curName = "";
        //        return _curName;
        //    }

        //    set
        //    {
        //        _curName = value;
        //    }
        //}

        public int? CurNumHoras { get; set; }
        //public int? CurNumHoras
        //{
        //    get
        //    {
        //        if (_curNumHoras == null) _curNumHoras = 0;
        //        return _curNumHoras;
        //    }

        //    set
        //    {
        //        _curNumHoras = value;
        //    }
        //}

        public decimal? CurPrecio;
        //public decimal? CurPrecio
        //{
        //    get
        //    {
        //        if(_curPrecio == null) _curPrecio = (decimal)0.00;
        //        return _curPrecio;
        //    }

        //    set
        //    {
        //        _curPrecio = value;
        //    }
        //}

    }

}