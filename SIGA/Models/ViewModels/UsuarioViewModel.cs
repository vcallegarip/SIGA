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

        [JsonProperty(PropertyName = "User_Id")]
        public int User_Id { get; set; }

        [JsonProperty(PropertyName = "Per_Dni")]
        public int Per_Dni { get; set; }

        [JsonProperty(PropertyName = "TipoUser_Descrip")]
        public string TipoUser_Descrip { get; set; }

        [JsonProperty(PropertyName = "Per_Nombre")]
        public string Per_Nombre { get; set; }

        [JsonProperty(PropertyName = "Per_ApePaterno")]
        public string Per_ApePaterno { get; set; }

        [JsonProperty(PropertyName = "Per_ApeMaterno")]
        public string Per_ApeMaterno { get; set; }

        [JsonProperty(PropertyName = "Per_Sexo")]
        public string Per_Sexo { get; set; }

        [JsonProperty(PropertyName = "Per_Dir")]
        public string Per_Dir { get; set; }

        [JsonProperty(PropertyName = "Per_Tel")]
        public string Per_Tel { get; set; }

        [JsonProperty(PropertyName = "Per_Cel")]
        public string Per_Cel { get; set; }

        [JsonProperty(PropertyName = "Per_Email")]
        public string Per_Email { get; set; }

        [JsonProperty(PropertyName = "User_Nombre")]
        public string User_Nombre { get; set; }

        [JsonProperty(PropertyName = "User_Inactivo")]
        public Boolean User_Inactivo { get; set; }
    }


}