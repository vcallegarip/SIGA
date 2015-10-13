using Newtonsoft.Json;
using SIGA_Model;
using SIGA_Model.StoredProcContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGA.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public List<UsuarioInformation> UsuarioInformationItems { get; set; }
        
        [JsonProperty(PropertyName = "user_id")]
        public int User_Id { get; set; }

        [JsonProperty(PropertyName = "per_dni")]
        public int Per_Dni { get; set; }

        [JsonProperty(PropertyName = "tipouser_descrip")]
        public string TipoUser_Descrip { get; set; }

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

        [JsonProperty(PropertyName = "per_tel")]
        public string Per_Tel { get; set; }

        [JsonProperty(PropertyName = "per_cel")]
        public string Per_Cel { get; set; }

        [JsonProperty(PropertyName = "per_email")]
        public string Per_Email { get; set; }

        [JsonProperty(PropertyName = "user_nombre")]
        public string User_Nombre { get; set; }

        //[JsonProperty(PropertyName = "User_Inactivo")]
        //public Boolean User_Inactivo { get; set; }
    }


}