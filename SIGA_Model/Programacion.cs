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
    
    public partial class Programacion
    {
        public Programacion()
        {
            this.Curso = new HashSet<Curso>();
        }
    
        public int Prog_Id { get; set; }
        public int Aul_Id { get; set; }
        public int Hor_Id { get; set; }
    
        public virtual Aula Aula { get; set; }
        public virtual ICollection<Curso> Curso { get; set; }
        public virtual Horario Horario { get; set; }
    }
}
