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

namespace SIGA.Controllers.api
{
    public class UsuarioApiController : ApiController
    {

        SIGAEntities db = new SIGAEntities();

        public UsuarioApiController()
        {
            AutoMapper.Mapper.CreateMap<Persona, UsuarioViewModel>();
            AutoMapper.Mapper.CreateMap<UsuarioViewModel, Persona>();

            AutoMapper.Mapper.CreateMap<Usuario, UsuarioViewModel>();
            AutoMapper.Mapper.CreateMap<UsuarioViewModel, Usuario>();

            AutoMapper.Mapper.CreateMap<Alumno, UsuarioViewModel>();
            AutoMapper.Mapper.CreateMap<UsuarioViewModel, Alumno>();

            //AutoMapper.Mapper.CreateMap<Persona, UsuarioController>();
            //AutoMapper.Mapper.CreateMap<UsuarioController, Persona>();
        }


        // PUT: api/usuario/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(UsuarioViewModel usuarioViewModel)
        {

            var persona = AutoMapper.Mapper.Map<UsuarioViewModel, Persona>(usuarioViewModel);
            int personaIdentity = 0;
            using (var db = new SIGAEntities())
            {
                try
                {
                    db.Persona.Add(persona);
                    db.SaveChanges();
                    personaIdentity = persona.Per_Id;

                    if (personaIdentity > 0)
                    {
                        var usuario = AutoMapper.Mapper.Map<UsuarioViewModel, Usuario>(usuarioViewModel);
                        int usuarioIdentity = 0;
                        usuario.Per_Id = personaIdentity;

                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                        usuarioIdentity = usuario.User_Id;

                        if (usuarioIdentity > 0)
                        {
                            var alumno = AutoMapper.Mapper.Map<UsuarioViewModel, Alumno>(usuarioViewModel);
                            alumno.User_Id = usuarioIdentity;
                            db.Alumno.Add(alumno);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //[HttpPost]
        // POST: api/usuario
        [ResponseType(typeof(UsuarioViewModel))]
        public HttpResponseMessage Post(UsuarioViewModel usuarioViewModel)
        {
            var persona = AutoMapper.Mapper.Map<UsuarioViewModel, Persona>(usuarioViewModel);
            int personaIdentity = 0;
            using (var db = new SIGAEntities())
            {
                try
                {
                    db.Persona.Add(persona);
                    db.SaveChanges();
                    personaIdentity = persona.Per_Id;

                    if (personaIdentity > 0)
                    {
                        var usuario = AutoMapper.Mapper.Map<UsuarioViewModel, Usuario>(usuarioViewModel);
                        int usuarioIdentity = 0;
                        usuario.Per_Id = personaIdentity;

                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                        usuarioIdentity = usuario.User_Id;

                        if (usuarioIdentity > 0)
                        {
                            var alumno = AutoMapper.Mapper.Map<UsuarioViewModel, Alumno>(usuarioViewModel);
                            alumno.User_Id = usuarioIdentity;
                            db.Alumno.Add(alumno);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
            //return CreatedAtRoute("DefaultApi", new { User_Id = usuarioViewModel.User_Id }, usuarioViewModel);
        }
    }
}
