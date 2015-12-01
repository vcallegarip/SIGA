using SIGA.Models.ViewModels;
using SIGA_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIGA.Controllers.Api
{
    public class CursoSearchController : ApiController
    {

        // GET: api/CursoInfo
        public List<CursoDTO> Get(string search)
        {
            List<CursoDTO> cursoList = new List<CursoDTO>();

            if (search == null) search = "";

            try
            {
                using(SIGAEntities db = new SIGAEntities())
	            {
		           cursoList = db.Curso.Where(c => c.CurName.Contains(search) || c.CurName == "")
                                .Select(c => new CursoDTO 
                                { 
                                    CurId = c.CurId,
                                    CurName = c.CurName,
                                    CurNumHoras = (int)c.CurNumHoras,
                                    CurPrecio = c.CurPrecio
                                }).ToList();
	            }

                if (cursoList.Count() == 0)
                {
                    List<CursoDTO> notFound = new List<CursoDTO>();
                    var cl = new CursoDTO { CurId = 0, CurName = "", CurNumHoras = 0, CurPrecio = (decimal)0.00 };
                    notFound.Add(cl);
                    return notFound;
                }

                return cursoList;

            }
            catch (Exception ex)
            {
                string message = "An error ocurred while getting employee list: " + ex;
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }
        }

    }
}
