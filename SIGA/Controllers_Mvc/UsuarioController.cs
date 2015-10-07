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

namespace SIGA.Controllers
{
    public class UsuarioController : Controller
    {

        public ActionResult Usuario([System.Web.Http.FromUri] string primerNombre, string apellidoPaterno, string email, string tipoUsuario)
        {
            ViewBag.Title = "Usuario";
            return PartialView("UsuarioPartialView", GetUsuarios(primerNombre, apellidoPaterno, email, tipoUsuario));
        }

        public ActionResult UsuarioList([System.Web.Http.FromUri] string primerNombre, string apellidoPaterno, string email, string tipoUsuario)
        {
            ViewBag.Title = "Usuario";
            return PartialView("UsuarioListPartialView", GetUsuarios(primerNombre, apellidoPaterno, email, tipoUsuario));
        }

        public UsuarioViewModel GetUsuarios(string primerNombre, string apellidoPaterno, string email, string tipoUsuario)
        {

            UsuarioInfoInputParams usuarioInfoInputParams = new UsuarioInfoInputParams()
            {
                PrimerNombre = primerNombre,
                ApellidoPaterno = apellidoPaterno,
                Email = email,
                TipoUsuario = tipoUsuario
            };

            UsuarioInfoCollection results = new UsuarioInfo().Execute(usuarioInfoInputParams);

            UsuarioViewModel usuarioViewModel = new UsuarioViewModel
            {
                UsuarioInformationItems = results.UsuarioInformationItems
            };

            return usuarioViewModel;

        }

        public ActionResult CreateUsuario()
        {
            return PartialView("Form", new UsuarioViewModel());
        }

    }
}
