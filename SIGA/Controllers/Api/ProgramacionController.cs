using SIGA.Models.ViewModels;
using SIGA_Model;
using SIGA_Model.StoredProcContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGA.Controllers.Api
{
    public class ProgramacionController : ApiController
    {

        SIGAEntities db = new SIGAEntities();

        public List<Modulo> GetCursoModuloNombres()
        {
            List<Modulo> modulos = new List<Modulo>();

            using (var db = new SIGAEntities())
            {
                try 
                {
                    modulos = db.Modulo.ToList();
                }
                catch (Exception e)
                {
		
                    throw;
                }

                return modulos;
            }

        }
    }

}
