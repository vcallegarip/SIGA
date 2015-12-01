using SIGA.Models.ViewModels;
using SIGA_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SIGA.Controllers.Api
{
    public class ModuloCursoController : ApiController
    {

        private SIGAEntities db = new SIGAEntities();


        // GET: api/Modulo
        public List<ModuloDTO> GetModulos()
        {
            try
            {
                List<ModuloDTO> ModulosNestedList = new List<ModuloDTO>();

                foreach (var modulocurso in db.ModuloCurso.GroupBy(mc => mc.ModId).OrderBy(mc => mc.Key))
                {
                    ModuloDTO moduloDTO = new ModuloDTO();
                    moduloDTO.ModId = modulocurso.Key;
                    moduloDTO.ModCategroria = (from m in db.Modulo
                                              join mc in db.ModuloCategoria
                                              on m.ModCatId equals mc.ModCatId
                                              where m.ModId == modulocurso.Key
                                              select mc.ModCatNombre).FirstOrDefault().ToString();

                    moduloDTO.ModNivel = (from m in db.Modulo
                                          join mn in db.ModuloNivel
                                          on m.ModNivelId equals mn.ModNivelId
                                          where m.ModId == modulocurso.Key
                                          select mn.ModNivelNombre).FirstOrDefault().ToString();

                    moduloDTO.ModNombre = db.Modulo.Where(m => m.ModId == modulocurso.Key).Select(m => m.ModNombre).FirstOrDefault().ToString();
                    moduloDTO.ModNumHoras = db.Modulo.Where(m => m.ModId == modulocurso.Key).Select(m => m.ModNumHoras).FirstOrDefault();
                    moduloDTO.ModNumMes = db.Modulo.Where(m => m.ModId == modulocurso.Key).Select(m => m.ModNumMes).FirstOrDefault();   
                    moduloDTO.ModNumCursos = db.Modulo.Where(m => m.ModId == modulocurso.Key).Select(m => m.ModNumCursos).FirstOrDefault();

                    moduloDTO.Cursos = (from m in db.ModuloCurso
                                        join c in db.Curso
                                        on m.CurId equals c.CurId
                                        where m.ModId == modulocurso.Key
                                        select new CursoDTO
                                        {
                                            CurId = c.CurId,
                                            CurName = c.CurName,
                                            CurNumHoras = c.CurNumHoras == null ? 0 : c.CurNumHoras,
                                            CurPrecio =  c.CurPrecio == null ? (decimal)0.00 : c.CurPrecio,
                                        }).ToList();

                    ModulosNestedList.Add(moduloDTO);
                }

                return ModulosNestedList;

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }


        // GET: api/Modulo/5
        [ResponseType(typeof(ModuloDTO))]
        public IHttpActionResult GetModulo(int id)
        {

            ModuloDTO moduloDTO = new ModuloDTO();
            

            Modulo modulo = db.Modulo.Find(id);

            if (modulo != null)
            {

                moduloDTO.ModId = modulo.ModId;
                moduloDTO.ModCategroria = (from m in db.Modulo
                                           join mc in db.ModuloCategoria
                                           on m.ModCatId equals mc.ModCatId
                                           select mc.ModCatNombre).FirstOrDefault().ToString();

                moduloDTO.ModNivel = (from m in db.Modulo
                                      join mn in db.ModuloNivel
                                      on m.ModNivelId equals mn.ModNivelId
                                      select mn.ModNivelNombre).FirstOrDefault().ToString();

                moduloDTO.ModNombre = modulo.ModNombre;
                moduloDTO.ModNumHoras = modulo.ModNumHoras;
                moduloDTO.ModNumMes = modulo.ModNumMes;
                moduloDTO.ModNumCursos = modulo.ModNumCursos;

                moduloDTO.Cursos = (from m in db.ModuloCurso
                                    join c in db.Curso
                                    on m.CurId equals c.CurId
                                    select new CursoDTO
                                    {
                                        CurId = c.CurId,
                                        CurName = c.CurName,
                                        CurNumHoras = c.CurNumHoras == null ? 0 : c.CurNumHoras,
                                        CurPrecio = c.CurPrecio == null ? (decimal)0.00 : c.CurPrecio,
                                    }).ToList();
            }

            if (id == 0)
            {
                moduloDTO.Cursos = new List<CursoDTO>();
                moduloDTO.Cursos.Add(new CursoDTO());

                return Ok(moduloDTO);
            }

            
            if (modulo == null || id != 0)
            {
                string message = string.Format("No se encontro el modulo con id = {0}.", id.ToString());
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            return Ok(moduloDTO);
        }


        // PUT: api/Modulo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModulo(int id, Modulo modulo)
        {
            string message = "Un error ocurrio mientras se actualizaba el modulo: ";
            if (!ModelState.IsValid)
            {
                message += "Modulo ModelState no es valido.";
                return BadRequest(ModelState);
            }

            if (id != modulo.ModId)
            {
                message += "El Id and Modulo.Id no son iguales.";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            db.Entry(modulo).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ModuloExists(id))
                {
                    message = "An error ocurred while updating event: No event found with requested event id = " + id.ToString();
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message + " " + ex));
                }
                else
                {
                    message = "Un error ocurrio mientras se actualizaba el modulo " + ex;
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/Modulo
        [ResponseType(typeof(ModuloDTO))]
        public IHttpActionResult PostModulo([FromBody] ModuloDTO moduloDTO)
        {
            string message = "Un error ocurrio cuando se estaba creando el modulo: ";
            if (!ModelState.IsValid)
            {
                message += "Modulo ModelState es no valido.";
                return BadRequest(ModelState);
            }


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ModuloExists(moduloDTO.ModId))
                {
                    message = "An error ocurred while creating event: Event with EventId = " + moduloDTO.ModId.ToString() + " " + ex;
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, message));
                }
                else
                {
                    message = "An error ocurred while creating event: " + ex;
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = moduloDTO.ModId }, moduloDTO);
        }


        // DELETE: api/Events/5
        [ResponseType(typeof(Modulo))]
        public IHttpActionResult DeleteEvent(int id)
        {
            Modulo modulo = db.Modulo.Find(id);
            if (modulo == null)
            {
                string message = "An error ocurred while deleting event: No event found with requested event id = " + id.ToString();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }
            try
            {
                db.Modulo.Remove(modulo);
                db.SaveChanges();

                return Ok(modulo);
            }
            catch (Exception ex)
            {
                string message = "An error ocurred while deleting event: " + ex;
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));

            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModuloExists(int id)
        {
            return db.Modulo.Count(e => e.ModId == id) > 0;
        }

    }
}
