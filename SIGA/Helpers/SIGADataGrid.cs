using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGA.Helpers
{
    public static class SIGADataGrid
    {
        public static MvcHtmlString RenderSigaDataGrid(this HtmlHelper html, SigaDataGridSystem dataGrid)
        {

            string control = dataGrid.CreateControl();

            return MvcHtmlString.Create(control);

        }
    }
}