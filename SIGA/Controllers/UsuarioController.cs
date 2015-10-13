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

        public UsuarioViewModel GetUsuario(int userid)
        {

            //List<UsuarioInformation> singleUsuario = GetUsuarios(userid,"","","","");
            UsuarioInfoCollection singleUsuario = GetUsuarios(userid,"","","","Todos");

            UsuarioViewModel usuarioViewModel = new UsuarioViewModel()
            {
                User_Id = singleUsuario.UsuarioInformationItems[0].User_Id,
                Per_Nombre = singleUsuario.UsuarioInformationItems[0].Per_Nombre.ToString(),
                Per_ApePaterno = singleUsuario.UsuarioInformationItems[0].Per_ApePaterno.ToString(),
                Per_ApeMaterno = singleUsuario.UsuarioInformationItems[0].Per_ApeMaterno.ToString(),
                Per_Email = singleUsuario.UsuarioInformationItems[0].Per_Email.ToString(),
                Per_Dni = singleUsuario.UsuarioInformationItems[0].Per_Dni,
                Per_Dir = singleUsuario.UsuarioInformationItems[0].Per_Dir.ToString(),
                Per_Cel = singleUsuario.UsuarioInformationItems[0].Per_Cel.ToString(),
                Per_Tel = singleUsuario.UsuarioInformationItems[0].Per_Tel.ToString(),
                Per_Sexo = singleUsuario.UsuarioInformationItems[0].Per_Sexo.ToString(),
                TipoUser_Descrip = singleUsuario.UsuarioInformationItems[0].TipoUser_Descrip.ToString(),
            };

            return usuarioViewModel;

        }

        public ActionResult Create()
        {
            return PartialView("Form", new UsuarioViewModel());
        }

        public ActionResult Edit(int? userid)
        {
            if (userid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return PartialView("Form", GetUsuario(userid.Value));
        }

        //public ActionResult Edit(int id, FormCollection collection)
        //{
            
        //            // DateTime effectiveDate = Utils.ConvertToDate(collection["ModulePayChange.EffectiveDateForDisplay"], "dddd, MMMM d, yyyy");
        //            pafModuleItem.PAF_Module_Item_Data.Add(createItemData("BonusPercent", collection["ModulePayChange.BonusPercent"], "percent"));

        //            var bonusType = collection["ModulePayChange.BonusType"] == null ? "None" : collection["ModulePayChange.BonusType"];
   
        //            pafModuleItem.PAF_Module_Item_Data.Add(createItemData("BonusType", bonusTypeElements[0], "varchar"));

        //            pafModuleItem.PAF_Module_Item_Data.Add(createItemData("EffectiveDate", collection["ModulePayChange.EffectiveDateForDisplay"], "varchar"));

        //            var newPayCompPercent = collection["ModulePayChange.NewCompPercent"] == null ? "" : collection["ModulePayChange.NewCompPercent"];
        //            pafModuleItem.PAF_Module_Item_Data.Add(createItemData("NewCompPercent", newPayCompPercent, "percent"));

        //            pafModuleItem.PAF_Module_Item_Data.Add(createItemData("NewCompRate", collection["ModulePayChange.NewCompRate"], "money"));

        //            var reason = collection["ModulePayChange.Reason"];
        //            string[] reasonElements = reason.Split(',');
        //            pafModuleItem.PAF_Module_Item_Data.Add(createItemData("Reason", reasonElements[0], "varchar"));

        //            db.PAF_Request.Add(pafRequest);

        //        }

        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {

        //            ViewBag.Exception = ex.Message;
        //            //TODO: Log it
        //            return PartialView("PayChangeExceptionPartialView");
        //        }
        //    }

        //    return RedirectToAction("Get", new
        //    {
        //        requestGuid = requestGuid,
        //        requestModuleGuid = moduleGuid,
        //        requestModuleItemGuid = moduleItemGuid,
        //        clockId = clockId,
        //        editable = true

        //    });



        //}

        //private PAF_Module_Item_Data createItemData(string key, string value, string dataType)
        //{
        //    PAF_Module_Item_Data data = new PAF_Module_Item_Data();
        //    data.Key = key;
        //    data.Value = value;
        //    data.ValueDataType = dataType;
        //    return data;
        //}

    }
}

