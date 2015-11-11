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
        private int _user_Id;
        public int User_Id
        {
            get
            {
                if (_user_Id == null) _user_Id = 0;
                return _user_Id;
            }
            set
            {
                _user_Id = value;
            }

        }


        [StoredProcAttributes.Name("Per_Dni")]
        private int _per_Dni;
        public int Per_Dni
        {
            get
            {
                if (_per_Dni == null) _per_Dni = 0;
                return _per_Dni;
            }
            set
            {
                _per_Dni = value;
            }

        }


        [StoredProcAttributes.Name("Per_Nombre")]
        private string _per_Nombre;
        public string Per_Nombre
        {
            get
            {
                if (_per_Nombre == null) _per_Nombre = "";
                return _per_Nombre;
            }
            set
            {
                _per_Nombre = value;
            }

        }


        [StoredProcAttributes.Name("Per_ApePaterno")]
        private string _per_ApePaterno;
        public string Per_ApePaterno
        {
            get
            {
                if (_per_ApePaterno == null) _per_ApePaterno = "";
                return _per_ApePaterno;
            }
            set
            {
                _per_ApePaterno = value;
            }

        }


        [StoredProcAttributes.Name("Per_ApeMaterno")]
        private string _per_ApeMaterno;
        public string Per_ApeMaterno
        {
            get
            {
                if (_per_ApeMaterno == null) _per_ApeMaterno = "";
                return _per_ApeMaterno;
            }
            set
            {
                _per_ApeMaterno = value;
            }
        }


        [StoredProcAttributes.Name("Per_Sexo")]
        private string _per_Sexo;
        public string Per_Sexo
        {
            get
            {
                if (_per_Sexo == null) _per_Sexo = "";
                return _per_Sexo;
            }
            set
            {
                _per_Sexo = value;
            }
        }


        [StoredProcAttributes.Name("Per_Dir")]
        private string _per_Dir;
        public string Per_Dir
        {
            get
            {
                if (_per_Dir == null) _per_Dir = "";
                return _per_Dir;
            }
            set
            {
                _per_Dir = value;
            }
        }


        [StoredProcAttributes.Name("Per_Cel")]
        private string _per_Cel;
        public string Per_Cel
        {
            get
            {
                if (_per_Cel == null) _per_Cel = "";
                return _per_Cel;
            }
            set
            {
                _per_Cel = value;
            }
        }


        [StoredProcAttributes.Name("Per_Tel")]
        private string _per_Tel;
        public string Per_Tel
        {
            get
            {
                if (_per_Tel == null) _per_Tel = "";
                return _per_Tel;
            }
            set
            {
                _per_Tel = value;
            }
        }


        [StoredProcAttributes.Name("Per_Email")]
        private string _per_Email;
        public string Per_Email
        {
            get
            {
                if (_per_Email == null) _per_Email = "";
                return _per_Email;
            }
            set
            {
                _per_Email = value;
            }
        }


        [StoredProcAttributes.Name("User_Nombre")]
        private string _user_Nombre;
        public string User_Nombre
        {
            get
            {
                if (_user_Nombre == null) _user_Nombre = "";
                return _user_Nombre;
            }
            set
            {
                _user_Nombre = value;
            }
        }


        [StoredProcAttributes.Name("TipoUser_Descrip")]
        private string _tipoUser_Descrip;
        public string TipoUser_Descrip
        {
            get
            {
                if (_tipoUser_Descrip == null) _tipoUser_Descrip = "";
                return _tipoUser_Descrip;
            }
            set
            {
                _tipoUser_Descrip = value;
            }
        }


        [StoredProcAttributes.Name("User_Inactivo")]
        private bool _user_Inactivo;
        public bool User_Inactivo
        {
            get
            {
                if (_user_Inactivo == null) _user_Inactivo = false;
                return _user_Inactivo;
            }
            set
            {
                _user_Inactivo = value;
            }
        }


        [StoredProcAttributes.Name("Alu_Apoderado")]
        private string _alu_Apoderado;
        public string Alu_Apoderado
        {
            get
            {
                if (_alu_Apoderado == null) _alu_Apoderado = "";
                return _alu_Apoderado;
            }
            set
            {
                _alu_Apoderado = value;
            }
        }


        [StoredProcAttributes.Name("Alu_FechaIngreso")]
        private DateTime _alu_FechaIngreso;
        public DateTime Alu_FechaIngreso
        {
            get
            {
                if (_alu_FechaIngreso == null) _alu_FechaIngreso = DateTime.Now;
                return _alu_FechaIngreso;
            }
            set
            {
                _alu_FechaIngreso = value;
            }
        }


        [StoredProcAttributes.Name("Alu_FechaRegistro")]
        private DateTime _alu_FechaRegistro;
        public DateTime Alu_FechaRegistro
        {
            get
            {
                if (_alu_FechaRegistro == null) _alu_FechaRegistro = DateTime.Now;
                return _alu_FechaRegistro;
            }
            set
            {
                _alu_FechaRegistro = value;
            }
        }

    }
    
}
