using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGA_WebApplication.Models
{
    public class Profesor2
    {
        public int Prof_Id { get; set; }
        public int User_Id { get; set; }
        public int Cur_Id { get; set; }
        public string Prof_Especialidad { get; set; }

        public string Curso { get; set; }
        public string Usuario { get; set; }
    }
}