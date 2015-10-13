using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstStoredProcs;

namespace SIGA_Model.StoredProcContexts
{
    public class UsuarioInfo
    {
        public UsuarioInfoCollection Execute(UsuarioInfoInputParams inputParams)
        {
            using (UsuarioContext db = new UsuarioContext())
            {
                ResultsList resultSets = db.getUsuarios.CallStoredProc(inputParams);
                UsuarioInfoCollection resultSet = new UsuarioInfoCollection();
                try
                {
                    resultSet.UsuarioInformationItems = resultSets.ToList<UsuarioInformation>();
                }
                catch (Exception ex)
                {
                    //TODO:: keep error in logger
                }
                return resultSet;
            }
        }
    }

    public class UsuarioInfoInputParams
    {
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        public int UserID { get; set; }

        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        [StoredProcAttributes.Size(50)]
        public String PrimerNombre { get; set; }

        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        [StoredProcAttributes.Size(50)]
        public String ApellidoPaterno { get; set; }

        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        [StoredProcAttributes.Size(100)]
        public String Email { get; set; }


        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
        [StoredProcAttributes.Size(50)]
        public String TipoUsuario { get; set; }
    }

    public class UsuarioInfoCollection
    {
        public List<UsuarioInformation> UsuarioInformationItems { get; set; }
    }

    public class UsuarioInformation
        {
            [StoredProcAttributes.ParameterType(System.Data.SqlDbType.Int)]
            [StoredProcAttributes.Name("User_Id")]
            public int User_Id { get; set; }

            [StoredProcAttributes.Name("Per_Dni")]
            public int Per_Dni { get; set; }

            [StoredProcAttributes.Name("TipoUser_Descrip")]
            public string TipoUser_Descrip { get; set; }

            [StoredProcAttributes.Name("Per_Nombre")]
            public string Per_Nombre { get; set; }

            [StoredProcAttributes.Name("Per_ApePaterno")]
            public string Per_ApePaterno { get; set; }

            [StoredProcAttributes.Name("Per_ApeMaterno")]
            public string Per_ApeMaterno { get; set; }

            [StoredProcAttributes.Name("Per_Sexo")]
            public string Per_Sexo { get; set; }

            [StoredProcAttributes.Name("Per_Dir")]
            public string Per_Dir { get; set; }

            [StoredProcAttributes.Name("Per_Tel")]
            public string Per_Tel { get; set; }

            [StoredProcAttributes.Name("Per_Cel")]
            public string Per_Cel { get; set; }

            [StoredProcAttributes.Name("Per_Email")]
            public string Per_Email { get; set; }

            [StoredProcAttributes.Name("User_Nombre")]
            public string User_Nombre { get; set; }

            [StoredProcAttributes.Name("User_Inactivo")]
            public Boolean User_Inactivo { get; set; }

        }
    
}
