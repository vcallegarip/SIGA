using Newtonsoft.Json;
using SIGA_Model;
using SIGA_Model.StoredProcContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGA.Models.ViewModels
{
    public class UsuarioViewModel
    {

        private SIGAEntities db = new SIGAEntities();

        public UsuarioItem UsuarioItem { get; set; }
        public List<SelectListItem> TipoUsuario { get; set; }

        public UsuarioViewModel()
        {
            this.TipoUsuario = new List<SelectListItem>();

            var tipoUsuarios = db.TipoUsuario.ToList();
            this.TipoUsuario.Add(new SelectListItem { Text = "-- Elegir --", Value = "-- Elegir --" });
            foreach (var tipoUsuario in tipoUsuarios)
            {
                this.TipoUsuario.Add(new SelectListItem { Text = tipoUsuario.TipoUser_Descrip, Value = tipoUsuario.TipoUser_Id.ToString() });
            }

            
        }
    }

    public class UsuarioItem
    {
        public int User_Id { get; set; }
        public int Per_Dni { get; set; }
        public string Per_Nombre { get; set; }
        public string Per_ApePaterno { get; set; }
        public string Per_ApeMaterno { get; set; }
        public string Per_Sexo { get; set; }
        public string Per_Dir { get; set; }
        public string Per_Cel { get; set; }
        public string Per_Tel { get; set; }
        public string Per_Email { get; set; }
        public string User_Nombre { get; set; }
        public string TipoUser_Descrip { get; set; }
        public AlumnoItem AlumnoItem { get; set; } 

    }

    public class AlumnoItem
    {
        public int User_Id  { get; set; }
        public DateTime Alu_FechNac { get; set; }
        public string Alu_Apoderado { get; set; }
        public DateTime Alu_FechIngreso { get; set; }

    }


}