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


        // POST: api/Authors
        [ResponseType(typeof(ProgramacionViewModel))]
        public IHttpActionResult Post(ProgramacionViewModel programacionViewModel)
        {

            using (var db = new SIGAEntities())
            {
                try
                {
                    Programa programa = new Programa();
                    programa.ProgNombre = programacionViewModel.ProgramacionItem.ProgNombre;
                    programa.ProgDescripcion = programacionViewModel.ProgramacionItem.ProgDescripcion;
                    programa.ProgFechaRegistro = DateTime.UtcNow;
                    programa.ProgFechaInicio = DateTime.UtcNow; //Convert.ToDateTime(programacionViewModel.ProgramacionItem.ProgFechaInicio);
                    programa.ProgFechaFin = DateTime.UtcNow; // Convert.ToDateTime(programacionViewModel.ProgramacionItem.ProgFechaFin);
                    programa.ModId = programacionViewModel.ProgramacionItem.ModId;
                    programa.AulId = programacionViewModel.ProgramacionItem.AulId;
                    programa.HorId = programacionViewModel.ProgramacionItem.HorId;
                    programa.EsVigente = programacionViewModel.ProgramacionItem.EsVigente;

                    db.Programa.Add(programa);

                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    throw;
                }

                return CreatedAtRoute("DefaultApi", new { id = programacionViewModel.ProgramacionItem.Prog_Id }, programacionViewModel);
            }


        }





    }

}
