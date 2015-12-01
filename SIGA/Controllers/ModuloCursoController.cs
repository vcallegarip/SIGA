using SIGA_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGA.Controllers
{
    public class ModuloCursoController : Controller
    {

        public ActionResult Index()
        {
            return PartialView("ModuloCursoPartialView", new ModuloCurso());
        }

        public ActionResult Dashboard()
        {
            ViewBag.Title = "Home Page";
            return PartialView("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return PartialView();
        }


        public ActionResult Create()
        {
            return PartialView("ModuloCursoCreateEditPartialView");
        }

        //public ActionResult Index()
        //{
        //    ViewBag.Title = "New PAF";
        //    ViewBag.DoSkipPafRequestLockCheck = Convert.ToBoolean(ConfigurationManager.AppSettings["DoSkipPafRequestLockCheck"]);
        //    return PartialView("Creation");
        //}
        //public ActionResult EditRequest(Guid requestGuid)
        //{
        //    var db = new PAFCenterEntities();
        //    string currentUserName = Identity.GetUser().NTLogin;
        //    // string currentUserName = Identity.GetUserName();
        //    try
        //    {
        //        RequestLock requestLock = RequestLocks.getLock(requestGuid);
        //        ViewBag.RequestId = db.Requests.Where(r => r.RequestGuid == requestGuid).Select(r => r.RequestId).FirstOrDefault();

        //        if (requestLock.UserName != currentUserName)
        //        {
        //            ViewBag.Message = "This document is currently being edited by " + requestLock.UserName + " and can not be edited";
        //            return PartialView("RequestDetails");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "";
        //            return PartialView("Creation");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = "An error occurred while editing request.";
        //        log.Error(message);
        //        throw;
        //    }

        //}
        //public ActionResult GetRequest(Guid requestGuid)
        //{
        //    var db = new PAFCenterEntities();
        //    ViewBag.RequestId = db.Requests.Where(r => r.RequestGuid == requestGuid).Select(r => r.RequestId).FirstOrDefault();
        //    return PartialView("RequestDetails");
        //}

    }
}
