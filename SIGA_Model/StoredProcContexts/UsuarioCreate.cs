using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstStoredProcs;

namespace SIGA_Model.StoredProcContexts
{
    class UsuarioCreate
    {
        public void Execute(UsuarioCreateInputParams inputParams)
        {
            using (UsuarioContext db = new UsuarioContext())
            {
                try
                {
                    db.createUsuario.CallStoredProc(inputParams);
                }
                catch (Exception ex)
                {
                    //TODO:: keep error in logger
                }
            }
        }
    }

    public class UsuarioCreateInputParams
    {

        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.Int)]
        [StoredProcAttributes.Size(50)]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Output)]
        public String User_Id { get; set; }

        [StoredProcAttributes.Name("UsuarioXML")]
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.Text)]
        [StoredProcAttributes.Direction(System.Data.ParameterDirection.Input)]
        public string TeamsAndPlanXML { get; set; }


    }

}
