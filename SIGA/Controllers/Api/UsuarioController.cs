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
    public class UsuarioController : ApiController
    {

        SIGAEntities db = new SIGAEntities();

        // POST: api/Authors
        [ResponseType(typeof(UsuarioViewModel))]
        public IHttpActionResult Post(UsuarioViewModel usuarioViewModel)
        {

            using (var db = new SIGAEntities())
            {

                string message = "An error ocurred while creating event: ";
                if (!ModelState.IsValid)
                {
                    message += "Event ModelState is not valid.";
                    return BadRequest(ModelState);
                }

                try
                {

                    Persona persona = new Persona();
                    persona.Per_Dni = usuarioViewModel.UsuarioItem.Per_Dni;
                    persona.Per_Nombre = usuarioViewModel.UsuarioItem.Per_Nombre;
                    persona.Per_ApePaterno = usuarioViewModel.UsuarioItem.Per_ApePaterno;
                    persona.Per_ApeMaterno = usuarioViewModel.UsuarioItem.Per_ApeMaterno;
                    persona.Per_Sexo = usuarioViewModel.UsuarioItem.Per_Sexo;
                    persona.Per_Dir = usuarioViewModel.UsuarioItem.Per_Dir;
                    persona.Per_Cel = usuarioViewModel.UsuarioItem.Per_Cel;
                    persona.Per_Tel = usuarioViewModel.UsuarioItem.Per_Tel;
                    persona.Per_Email = usuarioViewModel.UsuarioItem.Per_Email;

                    db.Persona.Add(persona);

                    TipoUsuario tipoUsuario = new TipoUsuario();
                    string tipoUserValue = "Alumno"; // usuarioViewModel.UsuarioItem.TipoUser_Descrip"];
                    var tipoUserId = db.TipoUsuario.Where(t => t.TipoUser_Descrip == tipoUserValue).FirstOrDefault();

                    Usuario usuario = new Usuario();
                    usuario.Per_Id = persona.Per_Id;
                    usuario.TipoUser_Id = Convert.ToInt16(tipoUserId.TipoUser_Id);
                    usuario.User_Nombre = "usename"; // persona.Per_Nombre.Substring(0, persona.Per_Nombre.Length) + " " + persona.Per_ApeMaterno.Substring(0, 0);
                    usuario.User_Pass = "";

                    db.Usuario.Add(usuario);


                    Alumno alumno = new Alumno();
                    alumno.User_Id = usuario.User_Id;
                    alumno.Alu_FechNac = DateTime.UtcNow; //usuarioViewModel.UsuarioItem;
                    alumno.Alu_Apoderado = "Apoderado"; //usuarioViewModel.UsuarioItem.Per_Dir"];
                    alumno.Alu_FechIngreso = DateTime.UtcNow;
                    alumno.Alu_Estado = true;

                    db.Alumno.Add(alumno);

                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    throw;
                }

                return CreatedAtRoute("DefaultApi", new { id = usuarioViewModel.UsuarioItem.User_Id }, usuarioViewModel);
            }


        }

    }
}
