using SIGA_Model.StoredProcContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGA.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public List<UsuarioInformation> UsuarioInformationItems { get; set; }
        //public IPagedList PagedList { get; set; }
    }
}