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

        public UsuarioItem UsuarioItem { get; set; }
        public List<UsuarioItem> UsuarioItemList { get; set; }

        //public List<SelectListItem> TipoUsuario { get; set; }

        public UsuarioViewModel()
        {

            //this.TipoUsuario = new List<SelectListItem>();

            this.UsuarioItem = new UsuarioItem();
            this.UsuarioItemList = new List<UsuarioItem>();
            this.UsuarioItem.AlumnoItem = new AlumnoItem();

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
        [JsonProperty(PropertyName = "User_Id")]
        public int User_Id { get; set; }

        [JsonProperty(PropertyName = "Per_Dni")]
        public int? Per_Dni { get; set; }

        [Required(ErrorMessage = "Nombre de la persona es requerido.")]
        [JsonProperty(PropertyName = "Per_Nombre")]
        public string Per_Nombre { get; set; }

        [Required(ErrorMessage = "Apellido Paterno es requerido.")]
        [JsonProperty(PropertyName = "Per_ApePaterno")]
        public string Per_ApePaterno { get; set; }

        [Required(ErrorMessage = "Apellido Materno es requerido.")]
        [JsonProperty(PropertyName = "Per_ApeMaterno")]
        public string Per_ApeMaterno { get; set; }

        [JsonProperty(PropertyName = "Per_Sexo")]
        public string Per_Sexo { get; set; }

        [JsonProperty(PropertyName = "Per_Dir")]
        public string Per_Dir { get; set; }

        [JsonProperty(PropertyName = "Per_Cel")]
        public string Per_Cel { get; set; }

        [JsonProperty(PropertyName = "Per_Tel")]
        public string Per_Tel { get; set; }

        [Required(ErrorMessage = "Correo electronico es requerido.")]
        [JsonProperty(PropertyName = "Per_Email")]
        public string Per_Email { get; set; }

        [JsonProperty(PropertyName = "User_Nombre")]
        public string User_Nombre { get; set; }

        [JsonProperty(PropertyName = "TipoUser_Descrip")]
        public string TipoUser_Descrip { get; set; }

        [JsonProperty(PropertyName = "AlumnoItem")]
        public AlumnoItem AlumnoItem { get; set; } 

    }

    public class AlumnoItem
    {

        [JsonProperty(PropertyName = "Alu_Apoderado")]
        public string Alu_Apoderado { get; set; }

        [Required(ErrorMessage = "Fecha de Ingreso es requerido.")]
        [JsonProperty(PropertyName = "Alu_FechaIngreso")]
        public DateTime? Alu_FechaIngreso { get; set; }

        [JsonProperty(PropertyName = "Alu_FechaRegistro")]
        public DateTime? Alu_FechaRegistro { get; set; }

    }  

}