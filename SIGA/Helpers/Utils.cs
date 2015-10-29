using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGA.Helpers
{
    public class Utils
    {
        public static string GetBaseUrl()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.Url == null)
            {
                return ""; // a relative url is the best we can do here.
            }

            Uri uri = HttpContext.Current.Request.Url;
            if (string.IsNullOrEmpty(uri.PathAndQuery)) return uri.ToString(); // THIS NEVER HAPPENS... when the root page is visited, the PathAndQuery is "/"...    the  the url IS the base Url, no additional pages or query parameters

            string fullUrl = uri.ToString();
            return fullUrl.Substring(0, fullUrl.LastIndexOf(uri.PathAndQuery));

        }
    }
}