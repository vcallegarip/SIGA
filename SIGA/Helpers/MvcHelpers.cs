//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Text;
//using System.IO;

//namespace SIGA.Helpers
//{

//    public static class MvcHelpers
//    {
//        public static string RenderPartialView(this Controller controller, string viewName, object model)
//        {
//            if (string.IsNullOrEmpty(viewName))
//                return null;

//            //if (string.IsNullOrEmpty(viewName))            
//            //    viewName = controller.ControllerContext.RouteData.GetRequiredString("action"); 

//            controller.ViewData.Model = model;

//            using (var sw = new StringWriter())
//            {
//                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
//                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
//                viewResult.View.Render(viewContext, sw); return sw.GetStringBuilder().ToString();
//            }
//        }


//        public static string ReturnFormCollectionValue(string array, string field, int index, FormCollection formCollection)
//        {
//            String key = array + "[" + index + "][" + field + "]";
//            return formCollection[key].ToString();
//        }

//        public static MvcHtmlString RenderNorthwindDataGrid(this HtmlHelper html, NorthwindWebControls.NorthwindDataGrid dataGrid)
//        {

//            string control = dataGrid.CreateControl();

//            return MvcHtmlString.Create(control);

//        }

//    }

//}