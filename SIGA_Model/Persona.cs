//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGA_Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persona()
        {
            this.Usuario = new HashSet<Usuario>();
        }
    
        public int Per_Id { get; set; }
        public int Per_Dni { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [JsonProperty(PropertyName = "per_nombre")]
        public string Per_Nombre { get; set; }
        public string Per_ApePaterno { get; set; }
        public string Per_ApeMaterno { get; set; }
        public string Per_Sexo { get; set; }
        public string Per_Dir { get; set; }
        public string Per_Cel { get; set; }
        public string Per_Tel { get; set; }
        public string Per_Email { get; set; }
        public Nullable<System.DateTime> Per_FechaNacimiento { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
