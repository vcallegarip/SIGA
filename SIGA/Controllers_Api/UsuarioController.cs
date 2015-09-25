using SIGA_Model.StoredProcContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGA.Controllers_Api
{
    public class UsuarioController
    {
        public UsuarioInfoCollection GetUsuarios(string PrimerNombre, string ApellidoPaterno, string Email, string TipoUsuario)
        {

            UsuarioInfoInputParams usuarioInfoInputParams = new UsuarioInfoInputParams()
            {
                PrimerNombre = PrimerNombre,
                ApellidoPaterno = ApellidoPaterno,
                Email = Email,
                TipoUsuario = TipoUsuario
            };

            UsuarioInfoCollection results = new UsuarioInfo().Execute(usuarioInfoInputParams);

            return results;

        }
    }
}