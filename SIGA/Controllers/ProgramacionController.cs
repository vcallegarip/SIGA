using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGA_Model;
using SIGA.Models.ViewModels;
using SIGA.Helpers;

namespace SIGA.Controllers
{
    [NoCache]
    public class ProgramacionController : Controller
    {
        private SIGAEntities db = new SIGAEntities();

        // GET: Programacion
        public ActionResult Index()
        {
            return PartialView("ProgramacionPartialView");
        }


        public ActionResult Programacion([System.Web.Http.FromUri] string progNombre, string moduloNombre)
        {
            ViewBag.Title = "Programacion";
            return PartialView("ProgramacionPartialView", GetProgramaciones(progNombre, moduloNombre));
        }


        public ActionResult ProgramacionList([System.Web.Http.FromUri] string progNombre, string moduloNombre)
        {
            ViewBag.Title = "Programacion";
            return PartialView("ProgramacionListPartialView", GetProgramaciones(progNombre, moduloNombre));
        }

        public ProgramacionViewModel GetProgramaciones(string progNombre, string moduloNombre)
        {

            ProgramacionViewModel progViewModel = new ProgramacionViewModel();

            progViewModel.ProgramacionItemList = (from p in db.Programa.Include(p => p.Modulo)
                                                  join a in db.Aula on p.AulId equals a.AulId
                                                  join h in db.Horario on p.HorId equals h.HorId
                                                  where p.ProgNombre.Contains(progNombre) && p.Modulo.ModNombre.Contains(moduloNombre)
                                                  select new ProgramacionItem
                                                  {
                                                      Prog_Id = p.ProgId,
                                                      ModId = p.ModId,
                                                      AulId = a.AulId,
                                                      HorId = h.HorId,
                                                      ProgNombre = p.ProgNombre,
                                                      ProgFechaInicio = p.ProgFechaInicio,
                                                      AulaItem = new AulaItem
                                                      {
                                                          AulId = a.AulId,
                                                          AulNumero = a.AulNumero,
                                                          AulCapacidad = a.AulCapacidad
                                                      },
                                                      HorarioItem = new HorarioItem
                                                      {
                                                          HorId = h.HorId,
                                                          HorTurno = h.HorTurno,
                                                          HorDia = h.HorTurno,
                                                          HorHoraIni = h.HorHoraIni,
                                                          HorHoraFin = h.HorHoraFin
                                                      },
                                                      ModuloItem = new ModuloItem
                                                      {
                                                          ModId = p.Modulo.ModId,
                                                          ModCatId = p.Modulo.ModCatId,
                                                          ModNivelId = p.Modulo.ModNivelId,
                                                          ModNombre = p.Modulo.ModNombre,
                                                          ModNumHoras = p.Modulo.ModNumHoras,
                                                          ModNumMes = p.Modulo.ModNumMes,
                                                          ModNumCursos = p.Modulo.ModNumCursos
                                                      },
                                                      ModuloCursoList = (from mc in db.ModuloCurso
                                                                         join c in db.Curso on mc.CurId equals c.CurId
                                                                         where mc.ModId == p.Modulo.ModId
                                                                         select new ModuloCursoItem
                                                                         {
                                                                             CurId = c.CurId,
                                                                             CurNombre = c.CurName,
                                                                             CurNumHoras = (int)c.CurNumHoras,

                                                                         }).ToList()
                                                  }).ToList();

            return progViewModel;

        }

        public ActionResult CursoPorModuloList([System.Web.Http.FromUri] string modNombre)
        {
            ProgramacionViewModel programacionViewModel = new ProgramacionViewModel();

            using (var db = new SIGAEntities())
            {
                try
                {
                    programacionViewModel.ProgramacionItem.ModuloCursoList = (from mc in db.ModuloCurso
                             join c in db.Curso on mc.CurId equals c.CurId
                             where mc.Modulo.ModNombre == modNombre
                             select new ModuloCursoItem
                             {

                                 CurId = c.CurId,
                                 CurNombre = c.CurName,
                                 CurNumHoras = c.CurNumHoras,
                                 CurPrecio = c.CurPrecio

                             }).ToList();

                    if (programacionViewModel.ProgramacionItem.ModuloCursoList.Count() == 0)
                    {
                        ViewBag.ModuleCursoItemCount = 0;
                        return PartialView("ProgramacionCursoListPartialView", programacionViewModel);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return PartialView("ProgramacionCursoListPartialView", programacionViewModel);
        }

        public ActionResult Create()
        {
            ViewBag.AulId = new SelectList(db.Aula, "AulId", "AulCapacidad");
            ViewBag.HorId = new SelectList(db.Horario, "HorId", "HorDia");
            ViewBag.ModId = new SelectList(db.Modulo, "ModId", "ModNombre");

            ProgramacionViewModel programacionViewModel = new ProgramacionViewModel();
            programacionViewModel.ProgramacionItem.ModuloCursoList = new List<ModuloCursoItem>();

            return PartialView("ProgramacionCreateEditPartialView", programacionViewModel);
        }
        
    }
}
