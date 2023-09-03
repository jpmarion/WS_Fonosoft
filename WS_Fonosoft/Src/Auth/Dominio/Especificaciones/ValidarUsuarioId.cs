using WS_Fonosoft.Src.Auth.Dominio.Abstracta;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using WS_Fonosoft.Src.Auth.Dominio.Interface;

namespace WS_Fonosoft.Src.Auth.Dominio.Especificaciones
{
    public class ValidarUsuarioId : AValidarUsuario
    {
        public override void EsValido(IUsuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Id.ToString()))
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.IdUsuarioNullOrEmpty);
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
