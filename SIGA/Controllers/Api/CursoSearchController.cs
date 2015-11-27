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
            List<CursoDTO> cursoList = null;

            try
            {
                using(SIGAEntities db = new SIGAEntities())
	            {
		           cursoList = db.Curso.Where(c => c.CurName.Contains(search))
                                .Select(c => new CursoDTO 
                                { 
                                    CurId = c.CurId,
                                    CurName = c.CurName,
                                    CurNumHoras = c.CurNumHoras,
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


        //[HttpGet]
        //[Route("api/NewHireSearch")]
        //public List<NewHireDTO> getNewHireSearch(string search)
        //{
        //    try
        //    {
        //        string NTLogin = Identity.GetUser().NTLogin;
        //        NewHireSearchInputParams NewHireSearchInputParams = new NewHireSearchInputParams()
        //        {
        //            SearchText = search == null ? "all" : search,
        //            NTLogin = NTLogin,
        //            EmployeeId = 0
        //        };

        //        NewHireSearchCollection results = NewHireSearch.Execute(NewHireSearchInputParams);

        //        if (results.NewHireSearchResults.Count() == 0)
        //        {
        //            List<NewHireDTO> notFound = new List<NewHireDTO>();
        //            var nf = new NewHireDTO { Name = "No match found", PayRollJobTitle = "", LoginID = "", Location = "", DistrictNumber = "0" };
        //            notFound.Add(nf);
        //            return notFound;
        //        }

        //        var newHires = results.NewHireSearchResults.Select(e => new NewHireDTO { Name = e.Name, PayRollJobTitle = e.PayRollJobTitle, LoginID = e.LoginID, Location = e.Location, DistrictNumber = e.DistrictNumber }).ToList();
        //        var notInList = new NewHireDTO { Name = "Add new employee", PayRollJobTitle = "", LoginID = "", Location = "", DistrictNumber = "0" };
        //        newHires.Add(notInList);
        //        return newHires;
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ExceptionHelper.GetFullMessage(ex, "An error ocurred while getting new hire list: ");
        //        log.Error(message);
        //        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
        //    }
        //}                                                     

    }
}
