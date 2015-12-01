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
        //[JsonProperty(PropertyName = "ModId")]
        public int ModId { get; set; }

        //[JsonProperty(PropertyName = "ModCategroria")]
        public string ModCategroria { get; set; }

        //[JsonProperty(PropertyName = "ModNivel")]
        public string ModNivel { get; set; }

        //[JsonProperty(PropertyName = "ModNombre")]
        public string ModNombre { get; set; }

        //[JsonProperty(PropertyName = "ModNumHoras")]
        public int ModNumHoras { get; set; }

        //[JsonProperty(PropertyName = "ModNumMes")]
        public int ModNumMes { get; set; }

        //[JsonProperty(PropertyName = "ModNumCursos")]
        public int ModNumCursos { get; set; }

        //[JsonProperty(PropertyName = "Cursos")]
        public List<CursoDTO> Cursos { get; set; }
    }

    public class CursoDTO
    {
        private int _curId { get; set; }
        public int CurId
        {
            get
            {
                if (_curId == null) _curId = 0;
                return _curId;
            }

            set
            {
                _curId = value;
            }
        }

        private string _curName { get; set; }
        public string CurName
        {
            get
            {
                if (_curName == null) _curName = "nombre";
                return _curName;
            }

            set
            {
                _curName = value;
            }
        }

        private int? _curNumHoras { get; set; }
        public int? CurNumHoras
        {
            get
            {
                if (_curNumHoras == null) _curNumHoras = 10;
                return _curNumHoras;
            }

            set
            {
                _curNumHoras = value;
            }
        }

        private decimal? _curPrecio;
        public decimal? CurPrecio
        {
            get
            {
                if(_curPrecio == null) _curPrecio = (decimal)0.00;
                return _curPrecio;
            }

            set
            {
                _curPrecio = value;
            }
        }

    }

}