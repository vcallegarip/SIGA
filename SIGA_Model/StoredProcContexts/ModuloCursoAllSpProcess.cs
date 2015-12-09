using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstStoredProcs;

namespace SIGA_Model.StoredProcContexts
{
    public class ModuloCursoAllSpProcess
    {
        public void EliminarModuloCurso(DeleteModuloCursoInputParams inputParams)
        {
            using (ModuloCursoContext db = new ModuloCursoContext())
            {
                try
                {
                    db.eliminarModuloCurso.CallStoredProc(inputParams);
                }
                catch (Exception ex)
                {
                    //TODO:: keep error in logger
                }
            }
        }

        public void EliminarModulo(DeleteModuloCursoInputParams inputParams)
        {
            using (ModuloCursoContext db = new ModuloCursoContext())
            {
                try
                {
                    db.eliminarModuloCurso.CallStoredProc(inputParams);
                    db.eliminarModulo.CallStoredProc(inputParams);
                }
                catch (Exception ex)
                {
                    //TODO:: keep error in logger
                }
            }
        }
    }

    public class DeleteModuloCursoInputParams
    {
        [StoredProcAttributes.ParameterType(System.Data.SqlDbType.Int)]
        public int ModId { get; set; }



    }

    
}
