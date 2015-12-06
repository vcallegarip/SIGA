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

                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}

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
                    string tipoUserValue = usuarioViewModel.UsuarioItem.TipoUser_Descrip;
                    var tipoUserId = db.TipoUsuario.Where(t => t.TipoUser_Descrip == tipoUserValue).FirstOrDefault();

                    Usuario usuario = new Usuario();
                    usuario.Per_Id = persona.Per_Id;
                    usuario.TipoUser_Id = Convert.ToInt16(tipoUserId.TipoUser_Id);
                    usuario.User_Nombre = usuarioViewModel.UsuarioItem.User_Nombre;
                    //usuario.User_Pass = "";

                    db.Usuario.Add(usuario);

                    if (tipoUserValue == "Alumno")
                    {
                        Alumno alumno = new Alumno();
                        alumno.User_Id = usuario.User_Id;
                        alumno.Alu_Apoderado = usuarioViewModel.UsuarioItem.AlumnoItem.Alu_Apoderado;
                        alumno.Alu_FechaIngreso = (DateTime)usuarioViewModel.UsuarioItem.AlumnoItem.Alu_FechaIngreso;
                        alumno.Alu_FechaRegistro = DateTime.UtcNow;
                        alumno.Alu_Estado = true;

                        db.Alumno.Add(alumno);
                    }

                    if (tipoUserValue == "Profesor")
                    {
                        Profesor profesor = new Profesor();
                        profesor.User_Id = usuario.User_Id;
                        profesor.Cur_Id = usuarioViewModel.UsuarioItem.ProfesorItem.Cur_Id;
                        profesor.Prof_Especialidad = usuarioViewModel.UsuarioItem.ProfesorItem.Prof_Especialidad;
                        profesor.Prof_Procedencia = usuarioViewModel.UsuarioItem.ProfesorItem.Prof_Procedencia;
                        profesor.Prof_LugarEstudio = usuarioViewModel.UsuarioItem.ProfesorItem.Prof_LugarEstudio;

                        db.Profesor.Add(profesor);
                    }

                    if (tipoUserValue == "Administrador")
                    {
                        Administrador administrador = new Administrador();
                        // add code

                        db.Administrador.Add(administrador);
                    }


                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    throw;
                }

                return CreatedAtRoute("DefaultApi", new { id = usuarioViewModel.UsuarioItem.User_Id }, usuarioViewModel);
            }


        }


        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(UsuarioViewModel usuarioViewModel)
        {
            using (var db = new SIGAEntities())
            {
                try
                {
                    TipoUsuario tipoUsuario = new TipoUsuario();
                    string tipoUserValue = usuarioViewModel.UsuarioItem.TipoUser_Descrip; // collection["UsuarioItem.TipoUser_Descrip"];
                    var tipoUserId = db.TipoUsuario.Where(t => t.TipoUser_Descrip == tipoUserValue).FirstOrDefault();

                    Usuario usuario = db.Usuario.First(u => u.User_Id == usuarioViewModel.UsuarioItem.User_Id);
                    usuario.TipoUser_Id = Convert.ToInt16(tipoUserId.TipoUser_Id);
                    usuario.User_Nombre = "usename"; // persona.Per_Nombre.Substring(0, persona.Per_Nombre.Length) + " " + persona.Per_ApeMaterno.Substring(0, 0);
                    usuario.User_Pass = "";

                    Persona persona = db.Persona.First(p => p.Per_Id == usuario.Per_Id);
                    persona.Per_Dni = usuarioViewModel.UsuarioItem.Per_Dni; // Convert.ToInt32(collection["UsuarioItem.Per_Dni"]);
                    persona.Per_Nombre = usuarioViewModel.UsuarioItem.Per_Nombre; // collection["UsuarioItem.Per_Nombre"];
                    persona.Per_ApePaterno = usuarioViewModel.UsuarioItem.Per_ApePaterno; // collection["UsuarioItem.Per_ApePaterno"];
                    persona.Per_ApeMaterno = usuarioViewModel.UsuarioItem.Per_ApeMaterno; // collection["UsuarioItem.Per_ApeMaterno"];
                    persona.Per_Sexo = usuarioViewModel.UsuarioItem.Per_Sexo; // collection["UsuarioItem.Per_Sexo"];
                    persona.Per_Dir = usuarioViewModel.UsuarioItem.Per_Dir; // collection["UsuarioItem.Per_Dir"];
                    persona.Per_Cel = usuarioViewModel.UsuarioItem.Per_Cel; // collection["UsuarioItem.Per_Cel"];
                    persona.Per_Tel = usuarioViewModel.UsuarioItem.Per_Tel; // collection["UsuarioItem.Per_Tel"];
                    persona.Per_Email = usuarioViewModel.UsuarioItem.Per_Email; // collection["UsuarioItem.Per_Email"];


                    if (tipoUserValue == "Alumno")
                    {
                        Alumno alumno = db.Alumno.First(a => a.User_Id == usuarioViewModel.UsuarioItem.User_Id);
                        alumno.Alu_Apoderado = usuarioViewModel.UsuarioItem.AlumnoItem.Alu_Apoderado; // collection["UsuarioItem.AlumnoItem.Alu_Apoderado"];
                        alumno.Alu_FechaIngreso = Convert.ToDateTime(usuarioViewModel.UsuarioItem.AlumnoItem.Alu_FechaIngreso); // Convert.ToDateTime(collection["UsuarioItem.AlumnoItem.Alu_FechaIngreso"]);
                        alumno.Alu_FechaRegistro = Convert.ToDateTime(usuarioViewModel.UsuarioItem.AlumnoItem.Alu_FechaRegistro); // Convert.ToDateTime(collection["UsuarioItem.AlumnoItem.Alu_FechaRegistro"]);  //DateTime.UtcNow;
                        alumno.Alu_Estado = true;
                    }

                    if (tipoUserValue == "Profesor")
                    {
                        Profesor profesor = db.Profesor.First(a => a.User_Id == usuarioViewModel.UsuarioItem.User_Id);
                        profesor.Cur_Id = usuarioViewModel.UsuarioItem.ProfesorItem.Cur_Id;
                        profesor.Prof_Especialidad = usuarioViewModel.UsuarioItem.ProfesorItem.Prof_Especialidad;
                        profesor.Prof_Procedencia = usuarioViewModel.UsuarioItem.ProfesorItem.Prof_Procedencia;
                        profesor.Prof_LugarEstudio = usuarioViewModel.UsuarioItem.ProfesorItem.Prof_LugarEstudio;
                    }

                    if (tipoUserValue == "Administrador")
                    {
                        Administrador administrador = new Administrador();
                        // add code

                        db.Administrador.Add(administrador);
                    }

                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    throw;
                }

                return StatusCode(HttpStatusCode.NoContent);
            }

        }

    }

}
