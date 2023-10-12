using WS_Fonosoft.Src.Auth.Dominio.Abstracta;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Dominio.Especificaciones
{
    public class ValidarUsuarioNombreUsuario : AValidarUsuario
    {
        private readonly IRepositorioAuth _mysqlRepositorio;

        public ValidarUsuarioNombreUsuario(IRepositorioAuth mysqlRepositorio)
        {
            _mysqlRepositorio = mysqlRepositorio;
        }

        public override void EsValido(IUsuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.NombreUsuario))
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.NombreUsuarioNullOrEmpty);
                exception.Data.Add(erroresAuth.GetNroError(), erroresAuth.GetDescription());
                throw exception;
            }
            else if (_mysqlRepositorio.BuscarUsuarioXNombreUsuario(usuario.NombreUsuario) != null)
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.NombreUsuarioExistente);
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
