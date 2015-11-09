using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SIGA_Model.StoredProcContexts;
using SIGA.Models.ViewModels;
using SIGA_Model;
using SIGA.Helpers;

namespace SIGA.Controllers
{
    [NoCache]
    public class UsuarioController : Controller
    {

        SIGAEntities db = new SIGAEntities();

        public ActionResult Index()
        {
            return Content("There is no home page for this module.");
        }

        public ActionResult Usuario([System.Web.Http.FromUri] string primerNombre, string apellidoPaterno, string email, string tipoUsuario)
        {
            ViewBag.Title = "Usuario";
            return PartialView("UsuarioPartialView", GetUsuarios(0, primerNombre, apellidoPaterno, email, tipoUsuario));
        }

        public ActionResult UsuarioList([System.Web.Http.FromUri] string primerNombre, string apellidoPaterno, string email, string tipoUsuario)
        {
            ViewBag.Title = "Usuario";
            return PartialView("UsuarioListPartialView", GetUsuarios(0, primerNombre, apellidoPaterno, email, tipoUsuario));
        }

        public UsuarioInfoCollection GetUsuarios(int userid, string primerNombre, string apellidoPaterno, string email, string tipoUsuario)
        {

            UsuarioInfoInputParams usuarioInfoInputParams = new UsuarioInfoInputParams()
            {
                UserID = userid,
                PrimerNombre = primerNombre,
                ApellidoPaterno = apellidoPaterno,
                Email = email,
                TipoUsuario = tipoUsuario
            };

            UsuarioInfoCollection usuarioInfoCollection = new UsuarioInfo().Execute(usuarioInfoInputParams);

            return usuarioInfoCollection;

        }

        public UsuarioViewModel GetUsuario(int user_id)
        {

            //List<UsuarioInformation> singleUsuario = GetUsuarios(userid,"","","","");
            UsuarioInfoCollection singleUsuario = GetUsuarios(user_id, "", "", "", "Todos");

            UsuarioViewModel usuarioViewModel = new UsuarioViewModel();

            usuarioViewModel.UsuarioItem = new UsuarioItem()
            {
                User_Id = singleUsuario.UsuarioInformationItems[0].User_Id,
                Per_Nombre = singleUsuario.UsuarioInformationItems[0].Per_Nombre == null ? "" : singleUsuario.UsuarioInformationItems[0].Per_Nombre,
                Per_ApePaterno = singleUsuario.UsuarioInformationItems[0].Per_ApePaterno == null ? "" : singleUsuario.UsuarioInformationItems[0].Per_ApePaterno,
                Per_ApeMaterno = singleUsuario.UsuarioInformationItems[0].Per_ApeMaterno == null ? "" : singleUsuario.UsuarioInformationItems[0].Per_ApeMaterno,
                Per_Email = singleUsuario.UsuarioInformationItems[0].Per_Email == null ? "" : singleUsuario.UsuarioInformationItems[0].Per_Email,
                Per_Dni = singleUsuario.UsuarioInformationItems[0].Per_Dni == null ? 0 : singleUsuario.UsuarioInformationItems[0].Per_Dni,
                Per_Dir = singleUsuario.UsuarioInformationItems[0].Per_Dir == null ? "" : singleUsuario.UsuarioInformationItems[0].Per_Dir,
                Per_Cel = singleUsuario.UsuarioInformationItems[0].Per_Cel == null ? "" : singleUsuario.UsuarioInformationItems[0].Per_Cel,
                Per_Tel = singleUsuario.UsuarioInformationItems[0].Per_Tel == null ? "" : singleUsuario.UsuarioInformationItems[0].Per_Tel,
                Per_Sexo = singleUsuario.UsuarioInformationItems[0].Per_Sexo == null ? "" : singleUsuario.UsuarioInformationItems[0].Per_Sexo,
                TipoUser_Descrip = singleUsuario.UsuarioInformationItems[0].TipoUser_Descrip == null ? "" : singleUsuario.UsuarioInformationItems[0].TipoUser_Descrip,
            };

            usuarioViewModel.UsuarioItem.AlumnoItem = new AlumnoItem()
            {
                Alu_FechaIngreso = singleUsuario.UsuarioInformationItems[0].Alu_FechaIngreso == null ? (DateTime?)null : singleUsuario.UsuarioInformationItems[0].Alu_FechaIngreso,
                Alu_FechaRegistro = singleUsuario.UsuarioInformationItems[0].Alu_FechaRegistro == null ? (DateTime?)null : singleUsuario.UsuarioInformationItems[0].Alu_FechaRegistro,
                Alu_Apoderado = singleUsuario.UsuarioInformationItems[0].Alu_Apoderado == null ? "" : singleUsuario.UsuarioInformationItems[0].Alu_Apoderado,
            };

            return usuarioViewModel;

        }

        public ActionResult GetDetails(int userid)
        {
            return PartialView("UsuarioDetailsPartialView", GetUsuario(userid));
        }

        public ActionResult Create()
        {
            return PartialView("UsuarioCreateEditPartialView", new UsuarioViewModel());
        }

        public ActionResult Edit(int? userid)
        {
            if (userid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return PartialView("UsuarioCreateEditPartialView", GetUsuario(userid.Value));
        }

        
        public ActionResult Delete(int? userid)
        {
            if (userid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuario.First(u => u.User_Id == userid);
            usuario.User_Inactivo = true;

            db.SaveChanges();

            return RedirectToAction("Index");
            //return PartialView("Delete", GetUsuario(userid.Value));
        }


        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int userid)
        {
            Usuario usuario = db.Usuario.First(u => u.User_Id == userid);
            usuario.User_Inactivo = true;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}

