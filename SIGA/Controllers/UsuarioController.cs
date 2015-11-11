using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SIGA_Model.StoredProcContexts;
using SIGA.Models.ViewModels;
using SIGA_Model;
using SIGA.Helpers;
using System.Collections.Generic;

namespace SIGA.Controllers
{
    [NoCache]
    public class UsuarioController : Controller
    {

        public ActionResult Index()
        {
            return PartialView("UsuarioPartialView");
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

        public UsuarioViewModel GetUsuarios(int userid, string primerNombre, string apellidoPaterno, string email, string tipoUsuario)
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

            UsuarioViewModel usuarioViewModel = new UsuarioViewModel();

            usuarioViewModel.UsuarioItemList = (from u in usuarioInfoCollection.UsuarioInformationItems
                                              select new UsuarioItem
                                              {
                                                  User_Id = u.User_Id,
                                                  Per_Nombre = u.Per_Nombre,
                                                  Per_ApePaterno = u.Per_ApePaterno,
                                                  Per_ApeMaterno = u.Per_ApeMaterno,
                                                  Per_Email = u.Per_Email,
                                                  Per_Dni = u.Per_Dni,
                                                  Per_Dir = u.Per_Dir,
                                                  Per_Cel = u.Per_Cel,
                                                  Per_Tel = u.Per_Tel,
                                                  Per_Sexo = u.Per_Sexo,
                                                  User_Nombre = u.User_Nombre,
                                                  TipoUser_Descrip = u.TipoUser_Descrip,
                                                  AlumnoItem = new AlumnoItem
                                                  {
                                                      Alu_FechaIngreso = u.Alu_FechaIngreso,
                                                      Alu_FechaRegistro = u.Alu_FechaRegistro,
                                                      Alu_Apoderado = u.Alu_Apoderado,
                                                  }
                                              }).ToList();

            return usuarioViewModel;

        }

        public UsuarioViewModel GetUsuario(int userid)
        {

            UsuarioViewModel usuarioViewModel = GetUsuarios(userid, "", "", "", "Todos");

            usuarioViewModel.UsuarioItem = new UsuarioItem()
            {
                User_Id = usuarioViewModel.UsuarioItemList[0].User_Id,
                Per_Nombre = usuarioViewModel.UsuarioItemList[0].Per_Nombre == null ? "" : usuarioViewModel.UsuarioItemList[0].Per_Nombre,
                Per_ApePaterno = usuarioViewModel.UsuarioItemList[0].Per_ApePaterno == null ? "" : usuarioViewModel.UsuarioItemList[0].Per_ApePaterno,
                Per_ApeMaterno = usuarioViewModel.UsuarioItemList[0].Per_ApeMaterno == null ? "" : usuarioViewModel.UsuarioItemList[0].Per_ApeMaterno,
                Per_Email = usuarioViewModel.UsuarioItemList[0].Per_Email == null ? "" : usuarioViewModel.UsuarioItemList[0].Per_Email,
                Per_Dni = usuarioViewModel.UsuarioItemList[0].Per_Dni == null ? 0 : usuarioViewModel.UsuarioItemList[0].Per_Dni,
                Per_Dir = usuarioViewModel.UsuarioItemList[0].Per_Dir == null ? "" : usuarioViewModel.UsuarioItemList[0].Per_Dir,
                Per_Cel = usuarioViewModel.UsuarioItemList[0].Per_Cel == null ? "" : usuarioViewModel.UsuarioItemList[0].Per_Cel,
                Per_Tel = usuarioViewModel.UsuarioItemList[0].Per_Tel == null ? "" : usuarioViewModel.UsuarioItemList[0].Per_Tel,
                Per_Sexo = usuarioViewModel.UsuarioItemList[0].Per_Sexo == null ? "" : usuarioViewModel.UsuarioItemList[0].Per_Sexo,
                TipoUser_Descrip = usuarioViewModel.UsuarioItemList[0].TipoUser_Descrip == null ? "" : usuarioViewModel.UsuarioItemList[0].TipoUser_Descrip,
                AlumnoItem = new AlumnoItem{
                                Alu_FechaIngreso = usuarioViewModel.UsuarioItemList[0].AlumnoItem.Alu_FechaIngreso == null ? (DateTime?)null : usuarioViewModel.UsuarioItemList[0].AlumnoItem.Alu_FechaIngreso,
                                Alu_FechaRegistro = usuarioViewModel.UsuarioItemList[0].AlumnoItem.Alu_FechaRegistro == null ? (DateTime?)null : usuarioViewModel.UsuarioItemList[0].AlumnoItem.Alu_FechaRegistro,
                                Alu_Apoderado = usuarioViewModel.UsuarioItemList[0].AlumnoItem.Alu_Apoderado == null ? "" : usuarioViewModel.UsuarioItemList[0].AlumnoItem.Alu_Apoderado,
                           }
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
            using (SIGAEntities db = new SIGAEntities())
            {
                var user = db.Usuario.FirstOrDefault(u => u.User_Id == userid);
                if (user != null)
                {
                    user.User_Inactivo = true;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction("Usuario");
        }

    }
}

