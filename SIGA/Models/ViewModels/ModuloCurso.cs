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
        public string ModCategroria { get; set; }
        public string ModNivel { get; set; }
        public string ModNombre { get; set; }
        public int ModNumHoras { get; set; }
        public int ModNumMes { get; set; }
        public int ModNumCursos { get; set; }
        public List<CursoDTO> Cursos { get; set; }
    }

    public class CursoDTO
    {
        public int CurId { get; set; }
        public string CurName { get; set; }
        public int? CurNumHoras { get; set; }
        public decimal? CurPrecio { get; set; }

    }


}