using Newtonsoft.Json;
using SIGA_Model;
using SIGA_Model.StoredProcContexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGA.Models.ViewModels
{
    public class UsuarioViewModel
    {

        private SIGAEntities db = new SIGAEntities();

        //[Required]
        //[JsonProperty(PropertyName = "usuarioItem")]
        public UsuarioItem UsuarioItem { get; set; }

        //public List<SelectListItem> TipoUsuario { get; set; }

        public UsuarioViewModel()
        {
            //this.TipoUsuario = new List<SelectListItem>();

            this.UsuarioItem = new UsuarioItem();

            //var tipoUsuarios = db.TipoUsuario.ToList();
            //this.TipoUsuario.Add(new SelectListItem { Text = "-- Elegir --", Value = "-- Elegir --" });
            //foreach (var tipoUsuario in tipoUsuarios)
            //{
            //    this.TipoUsuario.Add(new SelectListItem { Text = tipoUsuario.TipoUser_Descrip, Value = tipoUsuario.TipoUser_Id.ToString() });
            //}
        }

    }

    public class UsuarioItem
    {
        [JsonProperty(PropertyName = "user_id")]
        public int User_Id { get; set; }

        [JsonProperty(PropertyName = "per_dni")]
        public int Per_Dni { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [JsonProperty(PropertyName = "per_nombre")]
        public string Per_Nombre { get; set; }

        [JsonProperty(PropertyName = "per_apepaterno")]
        public string Per_ApePaterno { get; set; }

        [JsonProperty(PropertyName = "per_apematerno")]
        public string Per_ApeMaterno { get; set; }

        [JsonProperty(PropertyName = "per_sexo")]
        public string Per_Sexo { get; set; }

        [JsonProperty(PropertyName = "per_dir")]
        public string Per_Dir { get; set; }

        [JsonProperty(PropertyName = "per_cel")]
        public string Per_Cel { get; set; }

        [JsonProperty(PropertyName = "per_tel")]
        public string Per_Tel { get; set; }

        [JsonProperty(PropertyName = "per_email")]
        public string Per_Email { get; set; }

        [JsonProperty(PropertyName = "user_nombre")]
        public string User_Nombre { get; set; }

        [JsonProperty(PropertyName = "tipouser_descrip")]
        public string TipoUser_Descrip { get; set; }

        [JsonProperty(PropertyName = "alumnoItem")]
        public AlumnoItem AlumnoItem { get; set; } 

    }

    public class AlumnoItem
    {
        [JsonProperty(PropertyName = "user_id")]
        public int User_Id  { get; set; }

        [JsonProperty(PropertyName = "alu_apoderado")]
        public string Alu_Apoderado { get; set; }

        [JsonProperty(PropertyName = "alu_fechaingreso")]
        public DateTime Alu_FechaIngreso { get; set; }

        [JsonProperty(PropertyName = "alu_fecharegistro")]
        public DateTime Alu_FechaRegistro { get; set; }

    }


}