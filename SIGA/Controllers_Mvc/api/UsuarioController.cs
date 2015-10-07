
using SIGA.Models.ViewModels;
using SIGA_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace SIGA.Controllers_Mvc.api
{
    public class UsuarioController : ApiController
    {

        public UsuarioController()
        {
            AutoMapper.Mapper.CreateMap<Persona, UsuarioController>();
            AutoMapper.Mapper.CreateMap<UsuarioController, Persona>();

            AutoMapper.Mapper.CreateMap<Usuario, UsuarioController>();
            AutoMapper.Mapper.CreateMap<UsuarioController, Usuario>();

            AutoMapper.Mapper.CreateMap<Alumno, UsuarioController>();
            AutoMapper.Mapper.CreateMap<UsuarioController, Alumno>();

            //AutoMapper.Mapper.CreateMap<Persona, UsuarioController>();
            //AutoMapper.Mapper.CreateMap<UsuarioController, Persona>();
        }


        // POST: api/Authors
        [ResponseType(typeof(UsuarioViewModel))]
        public IHttpActionResult Post(UsuarioViewModel usuarioViewModel)
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

            return CreatedAtRoute("DefaultApi", new { User_Id = usuarioViewModel.User_Id }, usuarioViewModel);
        }
    }
}
