using CifradoPE.Infraestructura.Interface;
using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Auth.Dominio.Especificaciones;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Aplicacion
{
    public class RegistrarUsuarioCU<T> : AEjecutarCU<T>
    {
        private readonly IMysqlRepositorioAuth _mysqlRepositorio;
        private readonly IEmailRepo _emailRepo;
        private readonly IAes _aes;
        private readonly IUsuario _usuario;
        private readonly ValidarUsuarioNombreUsuario _validarNombreUsuario;
        private readonly ValidarUsuarioEmail _validarUsuarioEmail = new ValidarUsuarioEmail();
        private readonly ValidarUsuarioPassword _validarUsuarioPassword = new ValidarUsuarioPassword();

        #region USUARIO
        public RegistrarUsuarioCU(IResponse<T> response, IMysqlRepositorioAuth mysqlRepositorio, IEmailRepo emailRepo, IAes aes, IUsuario usuario) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            this._emailRepo = emailRepo;
            _aes = aes;
            _usuario = usuario;
            _validarNombreUsuario = new ValidarUsuarioNombreUsuario(_mysqlRepositorio);
            _validarNombreUsuario.ProximaValidcion(_validarUsuarioEmail);
            _validarUsuarioEmail.ProximaValidcion(_validarUsuarioPassword);

        }
        #endregion
        public override IList<T> Proceso()
        {
            string email = _usuario.Email;
            _usuario.NombreUsuario = _aes.Encriptar(_usuario.NombreUsuario.ToLower());

            _validarNombreUsuario.EsValido(_usuario);

            _usuario.Email = _aes.Encriptar(_usuario.Email);
            _usuario.Password = _aes.Encriptar(_usuario.Password);

            IUsuario usuario = _mysqlRepositorio.RegistrarUsuario(_usuario);
            if (usuario != null)
            {
                ICollection<string> emails = new List<string>{
                    email
                };

                string asunto = "Confirmar Registro";
                string cuerpo = GetBodyConfirmarRegistro("");
                _emailRepo.EnviarEmail(emails, asunto, cuerpo, true);

                IList<IUsuario> list = new List<IUsuario>
                {
                    usuario
                };
                return (IList<T>)list;
            }
            return null;
        }
        private string GetBodyConfirmarRegistro(string urlConfirmarRegistro)
        {
            string htmlString = @"<html>
                      <body>
                      <p>Confirmar registro</p>
                      <p>@urlConfirmarRegistro</br></p>
                      </body>
                      </html>
                     ";
            return htmlString.Replace("@urlConfirmarRegistro", urlConfirmarRegistro);
        }
        public override void BeginTransaction()
        {
            _mysqlRepositorio.BeginTransaction();
        }
        public override void CommitTransaction()
        {
            _mysqlRepositorio.CommitTransaction();
        }
        public override void RollbackTransaction()
        {
            _mysqlRepositorio.RollbackTransaction();
        }
    }
}
