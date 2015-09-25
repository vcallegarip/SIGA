using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstStoredProcs;

namespace SIGA_Model.StoredProcContexts
{
    //class UsuarioSearch
    //{
    //    public UsuarioSearchCollection Execute(UsuarioSearchInputParams inputParams)
    //    {
    //        using (UsuarioContext db = new UsuarioContext())
    //        {
    //            CodeFirstStoredProcs.ResultsList resultSets = db.getUsuarios.CallStoredProc(inputParams);
    //            UsuarioSearchCollection resultSet = new UsuarioSearchCollection();
    //            try
    //            {
    //                resultSet.UsuarioSearchResults = resultSets.ToList<SIGA_Model.StoredProcContexts.UsuarioInformation>();
    //            }
    //            catch (Exception ex)
    //            {
    //                //TODO:: keep error in logger
    //            }
    //            return resultSet;
    //        }
    //    }
    //}

    //public class UsuarioSearchInputParams
    //{

    //    [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
    //    [StoredProcAttributes.Size(50)]
    //    public String PrimerNombre { get; set; }

    //    [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
    //    [StoredProcAttributes.Size(50)]
    //    public String ApellidoPaterno { get; set; }

    //    [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
    //    [StoredProcAttributes.Size(100)]
    //    public String Email { get; set; }


    //    [StoredProcAttributes.ParameterType(System.Data.SqlDbType.VarChar)]
    //    [StoredProcAttributes.Size(50)]
    //    public String TipoUsuario { get; set; }

        
    //}

    //public class UsuarioSearchCollection
    //{
    //    public List<UsuarioInformation> UsuarioSearchResults { get; set; }
    //}
}
