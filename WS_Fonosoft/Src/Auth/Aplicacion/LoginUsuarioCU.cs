using CifradoPE.Infraestructura.Interface;
using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Auth.Dominio.Especificaciones;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Aplicacion
{
    public class LoginUsuarioCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioAuth _mysqlRepositorio;
        private readonly IAes _aes;
        private readonly IUsuario _usuario;
        private readonly ValidarUsuarioNombreUsuario _validarUsuarioNombreUsuario;
        private readonly ValidarUsuarioPassword _validarUsuarioPassword = new ValidarUsuarioPassword();
        private readonly ValidarUsuarioNombrePassword _validarUsuarioNombrePassword;

        public LoginUsuarioCU(IResponse<T> response, IRepositorioAuth mysqlRepositorio, IAes aes, IUsuario usuario) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            _aes = aes;
            _usuario = usuario;
            _validarUsuarioNombreUsuario = new ValidarUsuarioNombreUsuario(_mysqlRepositorio);
            _validarUsuarioNombreUsuario.ProximaValidcion(_validarUsuarioPassword);
            _validarUsuarioNombrePassword = new ValidarUsuarioNombrePassword(_mysqlRepositorio);
        }
        public override IList<T> Proceso()
        {
            _validarUsuarioNombreUsuario.EsValido(_usuario);
            _usuario.Password = _aes.Encriptar(_usuario.Password);
            _usuario.NombreUsuario = _aes.Encriptar(_usuario.NombreUsuario.ToLower());
            _validarUsuarioNombrePassword.EsValido(_usuario);

            IUsuario usuario = _mysqlRepositorio.BuscarUsuarioXNombreUsuario(_usuario.NombreUsuario);

            if (usuario != null)
            {
                IList<IUsuario> lstUsuario = new List<IUsuario>
                {
                    usuario
                };
                return (IList<T>)lstUsuario;
            }
            return null;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
