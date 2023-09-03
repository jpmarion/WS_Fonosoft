using System.Text.RegularExpressions;
using WS_Fonosoft.Src.Auth.Dominio.Abstracta;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using WS_Fonosoft.Src.Auth.Dominio.Interface;

namespace WS_Fonosoft.Src.Auth.Dominio.Especificaciones
{
    public class ValidarUsuarioEmail : AValidarUsuario
    {
        public override void EsValido(IUsuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Email))
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.EmailNullOrEmpty);
                exception.Data.Add(erroresAuth.GetNroError(), erroresAuth.GetDescription());
                throw exception;
            }
            else if (!Regex.IsMatch(usuario.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.EmailFormatoIncorrecto);
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
