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

                foreach (var modulocurso in db.Modulo)
                {
                    ModuloDTO moduloDTO = new ModuloDTO();
                    moduloDTO.ModId = modulocurso.ModId;
                    moduloDTO.ModCategroria = (from m in db.Modulo
                                              join mc in db.ModuloCategoria
                                              on m.ModCatId equals mc.ModCatId
                                               where m.ModId == modulocurso.ModId
                                              select mc.ModCatNombre).FirstOrDefault().ToString();

                    moduloDTO.ModNivel = (from m in db.Modulo
                                          join mn in db.ModuloNivel
                                          on m.ModNivelId equals mn.ModNivelId
                                          where m.ModId == modulocurso.ModId
                                          select mn.ModNivelNombre).FirstOrDefault().ToString();

                    moduloDTO.ModNombre = modulocurso.ModNombre;
                    moduloDTO.ModNumHoras = modulocurso.ModNumHoras;
                    moduloDTO.ModNumMes = modulocurso.ModNumMes;
                    moduloDTO.ModNumCursos = modulocurso.ModNumCursos;

                    moduloDTO.Cursos = (from m in db.ModuloCurso
                                        join c in db.Curso
                                        on m.CurId equals c.CurId
                                        where m.ModId == modulocurso.ModId
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
                                        CurNumHoras = c.CurNumHoras,
                                        CurPrecio = c.CurPrecio,
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


        // POST: api/ModuloCurso
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
                Modulo modulo = new Modulo();
                modulo.ModNivelId = (int)db.ModuloNivel.Where(mn => mn.ModNivelNombre == moduloDTO.ModNivel).Select(id => id.ModNivelId).FirstOrDefault();
                modulo.ModCatId = (int)db.ModuloCategoria.Where(mn => mn.ModCatNombre == moduloDTO.ModCategroria).Select(id => id.ModCatId).FirstOrDefault();
                modulo.ModNombre = moduloDTO.ModNombre;
                modulo.ModNumHoras = moduloDTO.ModNumHoras;
                modulo.ModNumMes = moduloDTO.ModNumMes;
                modulo.ModNumCursos = moduloDTO.ModNumCursos;

                db.Modulo.Add(modulo);

                if (moduloDTO.Cursos.Count() > 0)
                {
                    List<Curso> cursoList = new List<Curso>();
                    foreach (var moduloCursoItem in moduloDTO.Cursos)
	                {
                        if (!String.IsNullOrEmpty(moduloCursoItem.CurName))
                        {

                            var cursoFound = db.Curso.Where(c => c.CurName == moduloCursoItem.CurName).FirstOrDefault();

                            Curso curso = new Curso();
                            if (cursoFound == null)
                            {
                                curso.CurName = moduloCursoItem.CurName;
                                curso.CurNumHoras = moduloCursoItem.CurNumHoras;
                                curso.CurPrecio = moduloCursoItem.CurPrecio;
                                db.Curso.Add(curso);
                                db.SaveChanges();
                            }

                            cursoList.Add(curso);

                            ModuloCurso moduloCurso = new ModuloCurso();
                            moduloCurso.MCFechaRegistro = DateTime.UtcNow;
                            moduloCurso.ModId = modulo.ModId;
                            moduloCurso.CurId = curso.CurId;

                            db.ModuloCurso.Add(moduloCurso);
                        }
	                }
                }

                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ModuloExists(moduloDTO.ModId))
                {
                    message = "An error ocurred while creating event: Event with EventId = " + moduloDTO.ModId.ToString() + " " + ex;
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, message));
                }
                else if (moduloDTO.ModNivel == "-- Elegir ModuloNivel --")
                {
                    message = "Elegir el Nivel del Modulo";
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
