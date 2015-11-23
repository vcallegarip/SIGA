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
                                              select new { mc.ModCatNombre }).FirstOrDefault().ToString();

                    moduloDTO.ModNivel = (from m in db.Modulo
                                          join mn in db.ModuloNivel
                                          on m.ModNivelId equals mn.ModNivelId
                                          where m.ModId == modulocurso.Key
                                          select new { mn.ModNivelNombre }).FirstOrDefault().ToString();

                    moduloDTO.ModNombre = db.Modulo.Where(m => m.ModId == modulocurso.Key).Select(m => m.ModNombre).FirstOrDefault().ToString();
                    moduloDTO.ModNumHoras = Convert.ToInt32(db.Modulo.Where(m => m.ModId == modulocurso.Key).Select(m => m.ModNumHoras).FirstOrDefault());
                    moduloDTO.ModNumMes = Convert.ToInt32(db.Modulo.Where(m => m.ModId == modulocurso.Key).Select(m => m.ModNumMes).FirstOrDefault());   
                    moduloDTO.ModNumCursos = Convert.ToInt32(db.Modulo.Where(m => m.ModId == modulocurso.Key).Select(m => m.ModNumCursos).FirstOrDefault());

                    moduloDTO.Cursos = (from m in db.ModuloCurso
                                        join c in db.Curso
                                        on m.CurId equals c.CurId
                                        where m.ModId == modulocurso.Key
                                        select new CursoDTO
                                        {
                                            CurId = c.CurId,
                                            CurName = c.CurName,
                                            CurNumHoras = c.CurNumHoras,
                                            CurPrecio = c.CurPrecio,
                                        }).ToList();

                    //db.Modulo.Join(db.ModuloCategoria,
                    //                                m => m.ModId,
                    //                                mc => mc.ModCatId,
                    //                                (m, mn) => new
                    //                                {
                    //                                    Modulo = m,
                    //                                    ModuloCategoria = mn
                    //                                })
                    //                                .Where(w => w.Modulo.ModId == modulocurso.Key)
                    //                                .Select(categoria => categoria.ModuloCategoria.ModCatNombre).FirstOrDefault().ToString();

                    //db.Modulo.Join(db.ModuloNivel, 
                    //                                m => m.ModId, 
                    //                                mn => mn.ModNivelId,
                    //                                (m, mn) => new 
                    //                                { 
                    //                                    Modulo = m, 
                    //                                    ModuloNivel = mn 
                    //                                })
                    //                                .Where(w => w.Modulo.ModId == modulocurso.Key)
                    //                                .Select(nivel => nivel.ModuloNivel.ModNivelNombre).FirstOrDefault().ToString();
                        
                    //db.ModuloCurso.Join(db.Curso,
                    //                                mc => mc.ModId,
                    //                                c => c.CurId,
                    //                                (mc, c) => new
                    //                                {
                    //                                    ModuloCurso = mc,
                    //                                    Curso = c
                    //                                })
                    //                                .Where(w => w.ModuloCurso.ModId == modulocurso.Key)
                    //                                .Select(cursos => new 
                    //                                {
                                                            
                    //                                });

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
        [ResponseType(typeof(Modulo))]
        public IHttpActionResult GetModulo(int id)
        {
            Modulo modulo = db.Modulo.Find(id);
            if (modulo == null)
            {
                string message = string.Format("No se encontro el modulo con id = {0}.", id.ToString());
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
            }

            return Ok(modulo);
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


        // POST: api/Events
        [ResponseType(typeof(Modulo))]
        public IHttpActionResult PostEvent(Modulo modulo)
        {
            string message = "Un error ocurrio cuando se estaba creando el modulo: ";
            if (!ModelState.IsValid)
            {
                message += "Modulo ModelState es no valido.";
                return BadRequest(ModelState);
            }

            db.Modulo.Add(modulo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ModuloExists(modulo.ModId))
                {
                    message = "An error ocurred while creating event: Event with EventId = " + modulo.ModId.ToString() + " " + ex;
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, message));
                }
                else
                {
                    message = "An error ocurred while creating event: " + ex;
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = modulo.ModId }, modulo);
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
