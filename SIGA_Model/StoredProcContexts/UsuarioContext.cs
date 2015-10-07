using System.Data.Entity;
using CodeFirstStoredProcs;

namespace SIGA_Model.StoredProcContexts
{
    public partial class UsuarioContext : DbContext
    {
        static UsuarioContext()
        {
            Database.SetInitializer<UsuarioContext>(null);
        }

        public UsuarioContext()
            : base("Name=SIGAConnection")
        {
            this.InitializeStoredProcs();
        }

        [StoredProcAttributes.Name("[Usuario.Get]")]
        [StoredProcAttributes.Schema("[dbo]")]
        [StoredProcAttributes.ReturnTypes(typeof(UsuarioInformation))]
        public StoredProc<UsuarioInfoInputParams> getUsuarios { get; set; }


        [StoredProcAttributes.Name("[Usuario.Create]")]
        [StoredProcAttributes.Schema("[dbo]")]
        public StoredProc<UsuarioCreateInputParams> createUsuario { get; set; }

        //[StoredProcAttributes.Name("[EmployeeInfo]")]
        //[StoredProcAttributes.Schema("[webapp]")]
        //[StoredProcAttributes.ReturnTypes(typeof(UsuarioInformation))]
        //public StoredProc<UsuarioInfoInputParams> webapp_EmployeeInfo { get; set; }

        
    }
}
