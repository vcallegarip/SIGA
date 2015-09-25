//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using SIGA_WebApplication.Models;
//using SIGA_WebApplication.Models.ViewModels;

//namespace SIGA_WebApplication.Controllers
//{
//    public class ProfesorController : Controller
//    {
        
//        private static readonly UsuarioViewModel profesorViewModel = new UsuarioViewModel();

//        // GET api/Profesor
//        public ActionResult Index()
//        {
//            return PartialView(profesorViewModel);
//        }

//        public ActionResult Profesor()
//        {
//            return PartialView(profesorViewModel);
//        }

//        public JsonResult GetProfesores()
//        {
//            return Json(profesorViewModel.GetProfesores(), JsonRequestBehavior.AllowGet);
//        }

//        [HttpGet]
//        public ActionResult Edit(int id)
//        {
//            return View(profesorViewModel.GetProfesor(id));
//        }

//        [HttpPost]
//        public ActionResult Edit(UsuarioDTO profesor)
//        {
//            profesorViewModel.UpdateProfesor(profesor);
//            return RedirectToAction("/Profesor");
//        }

//    }
//}
