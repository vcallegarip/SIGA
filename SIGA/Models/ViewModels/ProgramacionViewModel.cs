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
    public class ProgramacionViewModel
    {

        private SIGAEntities db = new SIGAEntities();

        public ProgramacionItem ProgramacionItem { get; set; }
        public List<ProgramacionItem> ProgramacionItemList { get; set; }

        public ProgramacionViewModel()
        {
            ProgramacionItem = new ProgramacionItem();
            ProgramacionItemList = new List<ProgramacionItem>();
        }

    }

    public class ProgramacionItem
    {
        [JsonProperty(PropertyName = "Prog_Id")]
        public int Prog_Id { get; set; }

        [JsonProperty(PropertyName = "ModId")]
        public int ModId { get; set; }

        [JsonProperty(PropertyName = "AulId")]
        public int AulId { get; set; }

        [JsonProperty(PropertyName = "HorId")]
        public int HorId { get; set; }

        [JsonProperty(PropertyName = "ProgNombre")]
        public string ProgNombre { get; set; }

        [JsonProperty(PropertyName = "ProgDescripcion")]
        public string ProgDescripcion { get; set; }

        [JsonProperty(PropertyName = "ProgFechaRegistro")]
        public DateTime? ProgFechaRegistro { get; set; }

        [Required(ErrorMessage = "Fecha de Inicio es requerido.")]
        [JsonProperty(PropertyName = "ProgFechaInicio")]
        public DateTime? ProgFechaInicio { get; set; }

        [Required(ErrorMessage = "Fecha de Fin es requerido.")]
        [JsonProperty(PropertyName = "ProgFechaFin")]
        public DateTime? ProgFechaFin { get; set; }


        [JsonProperty(PropertyName = "EsVigente")]
        public bool EsVigente { get; set; }

        [JsonProperty(PropertyName = "AulaItem")]
        public AulaItem AulaItem { get; set; }

        [JsonProperty(PropertyName = "HorarioItem")]
        public HorarioItem HorarioItem { get; set; }

        [JsonProperty(PropertyName = "ModuloItem")]
        public ModuloItem ModuloItem { get; set; }

        [JsonProperty(PropertyName = "ModuloCursoList")]
        public List<ModuloCursoItem> ModuloCursoList { get; set; }
    }

    public class AulaItem
    {
        [JsonProperty(PropertyName = "AulId")]
        public int AulId { get; set; }

        [JsonProperty(PropertyName = "AulNumero")]
        public int? AulNumero { get; set; }

        [JsonProperty(PropertyName = "AulCapacidad")]
        public int? AulCapacidad { get; set; }

    }

    public class HorarioItem
    {
        [JsonProperty(PropertyName = "HorId")]
        public int HorId { get; set; }

        [JsonProperty(PropertyName = "HorTurno")]
        public string HorTurno { get; set; }

        [JsonProperty(PropertyName = "HorDia")]
        public string HorDia { get; set; }

        [JsonProperty(PropertyName = "HorHoraIni")]
        public Nullable<System.TimeSpan> HorHoraIni { get; set; }

        [JsonProperty(PropertyName = "HorHoraFin")]
        public Nullable<System.TimeSpan> HorHoraFin { get; set; }

    }

    public class ModuloItem
    {
        [JsonProperty(PropertyName = "ModId")]
        public int ModId { get; set; }

        [JsonProperty(PropertyName = "ModCatId")]
        public int ModCatId { get; set; }

        [JsonProperty(PropertyName = "ModNivelId")]
        public int ModNivelId { get; set; }

        [JsonProperty(PropertyName = "ModNombre")]
        public string ModNombre { get; set; }

        [JsonProperty(PropertyName = "ModNumHoras")]
        public int ModNumHoras { get; set; }

        [JsonProperty(PropertyName = "ModNumMes")]
        public int ModNumMes { get; set; }

        [JsonProperty(PropertyName = "ModNumCursos")]
        public int ModNumCursos { get; set; }

    }

    public class ModuloCursoItem
    {
        [JsonProperty(PropertyName = "CurId")]
        public int CurId { get; set; }

        [JsonProperty(PropertyName = "CurNombre")]
        public string CurNombre { get; set; }

        [JsonProperty(PropertyName = "CurNumHoras")]
        public int? CurNumHoras { get; set; }

        [JsonProperty(PropertyName = "CurPrecio")]
        public decimal? CurPrecio { get; set; }

    }


}