using WS_Fonosoft.Src.Auth.Dominio.Abstracta;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Dominio.Especificaciones
{
    public class ValidarUsuarioNombrePassword : AValidarUsuario
    {
        private readonly IMysqlRepositorio _mysqlRepositorio;

        public ValidarUsuarioNombrePassword(IMysqlRepositorio mysqlRepositorio)
        {
            _mysqlRepositorio = mysqlRepositorio;
        }

        public override void EsValido(IUsuario usuario)
        {
            IUsuario usuarioRepo = _mysqlRepositorio.BuscarUsuarioXNombreUsuario(usuario.NombreUsuario);
            if (usuarioRepo== null)
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.UsuarioPassworIncorrecto);
                exception.Data.Add(erroresAuth.GetNroError(), erroresAuth.GetDescription());
                throw exception;
            }
            else if (usuarioRepo.Password != usuario.Password)
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.UsuarioPassworIncorrecto);
                exception.Data.Add(erroresAuth.GetNroError(), erroresAuth.GetDescription());
                throw exception;
            }

            if (_validador != null)
            {
                _validador.EsValido(usuario);
            }
        }
    }
}
