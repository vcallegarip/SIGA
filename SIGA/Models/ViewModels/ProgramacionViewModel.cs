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
        public int Prog_Id { get; set; }
        public int ModId { get; set; }
        public int AulId { get; set; }
        public int HorId { get; set; }
        public string ProgNombre { get; set; }
        private DateTime? _progFechaInicio { get; set; }
        public DateTime? ProgFechaInicio
        {
            get
            {
                if (_progFechaInicio == null) _progFechaInicio = DateTime.Now.Date;
                return _progFechaInicio.Value.Date;
            }
            set
            {
                _progFechaInicio = value;
    
            }
        }
        public AulaItem AulaItem { get; set; }
        public HorarioItem HorarioItem { get; set; }
        public ModuloItem ModuloItem { get; set; }
        public List<ModuloCursoItem> ModuloCursoList { get; set; }
    }

    public class AulaItem
    {
        public int AulId { get; set; }
        public int? AulNumero { get; set; }
        public int? AulCapacidad { get; set; }

    }

    public class HorarioItem
    {

        public int HorId { get; set; }
        public string HorTurno { get; set; }
        public string HorDia { get; set; }
        public Nullable<System.TimeSpan> HorHoraIni { get; set; }
        public Nullable<System.TimeSpan> HorHoraFin { get; set; }

    }

    public class ModuloItem
    {

        public int ModId { get; set; }
        public int ModCatId { get; set; }
        public int ModNivelId { get; set; }
        public string ModNombre { get; set; }
        public int ModNumHoras { get; set; }
        public int ModNumMes { get; set; }
        public int ModNumCursos { get; set; }

    }

    public class ModuloCursoItem
    {
        public int CudID { get; set; }
        public string CurNombre { get; set; }
        public int CurNumHoras { get; set; }
        public decimal CurPrecio { get; set; }

    }


}