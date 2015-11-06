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

        // POST: UsuarioChange/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            using (var db = new SIGAEntities())
            {
                try
                {
                    TipoUsuario tipoUsuario = new TipoUsuario();
                    string tipoUserValue = "Alumno"; // collection["UsuarioItem.TipoUser_Descrip"];
                    var tipoUserId = db.TipoUsuario.Where(t => t.TipoUser_Descrip == tipoUserValue).FirstOrDefault();

                    Usuario usuario = db.Usuario.First(u => u.User_Id == id);
                    usuario.TipoUser_Id = Convert.ToInt16(tipoUserId.TipoUser_Id);
                    usuario.User_Nombre = "usename"; // persona.Per_Nombre.Substring(0, persona.Per_Nombre.Length) + " " + persona.Per_ApeMaterno.Substring(0, 0);
                    usuario.User_Pass = "";

                    Persona persona = db.Persona.First(p => p.Per_Id == usuario.Per_Id);
                    persona.Per_Dni = Convert.ToInt32(collection["UsuarioItem.Per_Dni"]);
                    persona.Per_Nombre = collection["UsuarioItem.Per_Nombre"];
                    persona.Per_ApePaterno = collection["UsuarioItem.Per_ApePaterno"];
                    persona.Per_ApeMaterno = collection["UsuarioItem.Per_ApeMaterno"];
                    persona.Per_Sexo = collection["UsuarioItem.Per_Sexo"];
                    persona.Per_Dir = collection["UsuarioItem.Per_Dir"];
                    persona.Per_Cel = collection["UsuarioItem.Per_Cel"];
                    persona.Per_Tel = collection["UsuarioItem.Per_Tel"];
                    persona.Per_Email = collection["UsuarioItem.Per_Email"];

                    Alumno alumno = db.Alumno.First(a => a.User_Id == id);
                    alumno.Alu_Apoderado = collection["UsuarioItem.AlumnoItem.Alu_Apoderado"];
                    alumno.Alu_FechaIngreso = Convert.ToDateTime(collection["UsuarioItem.AlumnoItem.Alu_FechaIngreso"]);
                    alumno.Alu_FechaRegistro = Convert.ToDateTime(collection["UsuarioItem.AlumnoItem.Alu_FechaRegistro"]);  //DateTime.UtcNow;
                    alumno.Alu_Estado = true;

                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    throw;
                }

                return RedirectToAction("GetDetails", new
                {
                    userid = id,
                    //editable = true
                });

            }

        }

    }
}

