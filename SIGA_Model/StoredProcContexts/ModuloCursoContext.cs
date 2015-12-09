using System.Data.Entity;
using CodeFirstStoredProcs;

namespace SIGA_Model.StoredProcContexts
{
    public partial class ModuloCursoContext : DbContext
    {
        static ModuloCursoContext()
        {
            Database.SetInitializer<ModuloCursoContext>(null);
        }

        public ModuloCursoContext()
            : base("Name=SIGAConnection")
        {
            this.InitializeStoredProcs();
        }

        [StoredProcAttributes.Name("[ModuloCurso.Delete]")]
        [StoredProcAttributes.Schema("[dbo]")]
        public StoredProc<DeleteModuloCursoInputParams> eliminarModuloCurso { get; set; }

        [StoredProcAttributes.Name("[Modulo.Delete]")]
        [StoredProcAttributes.Schema("[dbo]")]
        public StoredProc<DeleteModuloCursoInputParams> eliminarModulo { get; set; }


    }
}
