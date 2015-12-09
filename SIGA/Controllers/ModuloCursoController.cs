using SIGA_Model;
using SIGA_Model.StoredProcContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGA.Controllers
{
    public class ModuloCursoController : Controller
    {

        private SIGAEntities db = new SIGAEntities();

        public ActionResult Index()
        {
            return PartialView("ModuloCursoPartialView");
        }

        public ActionResult Create()
        {

            List<SelectListItem> selectListNivelItem = new List<SelectListItem>();
            var moduloNivelList = (from mn in db.ModuloNivel
                               select new SelectListItem
                               {
                                   Text = mn.ModNivelNombre,
                                   Value = mn.ModNivelNombre
                               }).ToList();

            selectListNivelItem.Add(new SelectListItem { Text = "-- Elegir Nivel --", Value = "-- Elegir Nivel --" });

            foreach (var moduloNivel in moduloNivelList)
            {
                selectListNivelItem.Add(new SelectListItem { Text = moduloNivel.Value, Value = moduloNivel.Value });
            }

            ViewBag.ModuloNivelList = selectListNivelItem;

            List<SelectListItem> selectListCategoriItem = new List<SelectListItem>();
            var moduloCategoriaList = (from mc in db.ModuloCategoria
                                   select new SelectListItem
                                   {
                                       Text = mc.ModCatNombre,
                                       Value = mc.ModCatNombre
                                   }).ToList();

            selectListCategoriItem.Add(new SelectListItem { Text = "-- Elegir Categoria --", Value = "-- Elegir Categoria --" });

            foreach (var moduloCategoria in moduloCategoriaList)
            {
                selectListCategoriItem.Add(new SelectListItem { Text = moduloCategoria.Value, Value = moduloCategoria.Value });
            }

            ViewBag.ModuloCategoriaList = selectListCategoriItem;




            return PartialView("ModuloCursoCreateEditPartialView");
        }

        public ActionResult Details()
        {

            List<SelectListItem> selectListNivelItem = new List<SelectListItem>();
            var moduloNivelList = (from mn in db.ModuloNivel
                                   select new SelectListItem
                                   {
                                       Text = mn.ModNivelNombre,
                                       Value = mn.ModNivelNombre
                                   }).ToList();

            selectListNivelItem.Add(new SelectListItem { Text = "-- Elegir Nivel --", Value = "-- Elegir Nivel --" });

            foreach (var moduloNivel in moduloNivelList)
            {
                selectListNivelItem.Add(new SelectListItem { Text = moduloNivel.Value, Value = moduloNivel.Value });
            }

            ViewBag.ModuloNivelList = selectListNivelItem;

            List<SelectListItem> selectListCategoriItem = new List<SelectListItem>();
            var moduloCategoriaList = (from mc in db.ModuloCategoria
                                       select new SelectListItem
                                       {
                                           Text = mc.ModCatNombre,
                                           Value = mc.ModCatNombre
                                       }).ToList();

            selectListCategoriItem.Add(new SelectListItem { Text = "-- Elegir Categoria --", Value = "-- Elegir Categoria --" });

            foreach (var moduloCategoria in moduloCategoriaList)
            {
                selectListCategoriItem.Add(new SelectListItem { Text = moduloCategoria.Value, Value = moduloCategoria.Value });
            }

            ViewBag.ModuloCategoriaList = selectListCategoriItem;

            return PartialView("ModuloCursoDetailsPartialView");
        }

        public ActionResult Edit()
        {

            List<SelectListItem> selectListNivelItem = new List<SelectListItem>();
            var moduloNivelList = (from mn in db.ModuloNivel
                                   select new SelectListItem
                                   {
                                       Text = mn.ModNivelNombre,
                                       Value = mn.ModNivelNombre
                                   }).ToList();

            selectListNivelItem.Add(new SelectListItem { Text = "-- Elegir Nivel --", Value = "-- Elegir Nivel --" });

            foreach (var moduloNivel in moduloNivelList)
            {
                selectListNivelItem.Add(new SelectListItem { Text = moduloNivel.Value, Value = moduloNivel.Value });
            }

            ViewBag.ModuloNivelList = selectListNivelItem;

            List<SelectListItem> selectListCategoriItem = new List<SelectListItem>();
            var moduloCategoriaList = (from mc in db.ModuloCategoria
                                       select new SelectListItem
                                       {
                                           Text = mc.ModCatNombre,
                                           Value = mc.ModCatNombre
                                       }).ToList();

            selectListCategoriItem.Add(new SelectListItem { Text = "-- Elegir Categoria --", Value = "-- Elegir Categoria --" });

            foreach (var moduloCategoria in moduloCategoriaList)
            {
                selectListCategoriItem.Add(new SelectListItem { Text = moduloCategoria.Value, Value = moduloCategoria.Value });
            }

            ViewBag.ModuloCategoriaList = selectListCategoriItem;

            return PartialView("ModuloCursoCreateEditPartialView");
        }

        public ActionResult Delete(int moduloid)
        {
            Modulo modulo = db.Modulo.Find(moduloid);
            if (modulo == null)
            {
                string message = "Un Error ha ocurrido. No se encontro el modulo id = " + moduloid.ToString();
                return Content(message);
            }
            try
            {
                DeleteModuloCursoInputParams deleteModuloCursoInputParams = new DeleteModuloCursoInputParams()
                {
                    ModId = moduloid
                };

                ModuloCursoAllSpProcess ModuloCursoAllSPPRocess = new ModuloCursoAllSpProcess();
                ModuloCursoAllSPPRocess.EliminarModulo(deleteModuloCursoInputParams);

            }
            catch (Exception ex)
            {
                string message = "An error ocurred while deleting event: " + ex;
                return Content(message);

            }

            return Content("Ok");
        }

    }
}
