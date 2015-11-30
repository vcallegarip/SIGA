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
        [JsonProperty(PropertyName = "ModId")]
        public int ModId { get; set; }

        [JsonProperty(PropertyName = "ModCategroria")]
        public string ModCategroria { get; set; }

        [JsonProperty(PropertyName = "ModNivel")]
        public string ModNivel { get; set; }

        [JsonProperty(PropertyName = "ModNombre")]
        public string ModNombre { get; set; }

        [JsonProperty(PropertyName = "ModNumHoras")]
        public int ModNumHoras { get; set; }

        [JsonProperty(PropertyName = "ModNumMes")]
        public int ModNumMes { get; set; }

        [JsonProperty(PropertyName = "ModNumCursos")]
        public int ModNumCursos { get; set; }

        [JsonProperty(PropertyName = "Cursos")]
        public List<CursoDTO> Cursos { get; set; }
    }

    public class CursoDTO
    {
        [JsonProperty(PropertyName = "CurId")]
        public int CurId { get; set; }

        [JsonProperty(PropertyName = "CurName")]
        public string CurName { get; set; }

        [JsonProperty(PropertyName = "CurNumHoras")]
        public int? CurNumHoras { get; set; }

        [JsonProperty(PropertyName = "CurPrecio")]
        private decimal? _curPrecio;
        public decimal? CurPrecio
        {
            get
            {
                if (_curPrecio == (decimal)0.00) _curPrecio = (decimal)0.00;
                return _curPrecio;
            }
            set
            {
                _curPrecio = value;
            }

        }

    }

}