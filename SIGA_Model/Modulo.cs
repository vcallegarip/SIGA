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
    using System;
    using System.Collections.Generic;
    
    public partial class Modulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modulo()
        {
            this.Curso = new HashSet<Curso>();
        }
    
        public int Mod_Id { get; set; }
        public string Mod_Nombre { get; set; }
        public string Mod_Tipo { get; set; }
        public string Mod_Unidad { get; set; }
        public string Mod_NumHoras { get; set; }
        public string Mod_NumMes { get; set; }
        public string Mod_NumCursos { get; set; }
        public Nullable<int> Mod_Nivel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Curso> Curso { get; set; }
    }
}
