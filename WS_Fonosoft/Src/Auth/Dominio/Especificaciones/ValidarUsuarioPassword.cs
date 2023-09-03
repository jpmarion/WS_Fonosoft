using System.Text.RegularExpressions;
using WS_Fonosoft.Src.Auth.Dominio.Abstracta;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using WS_Fonosoft.Src.Auth.Dominio.Interface;

namespace WS_Fonosoft.Src.Auth.Dominio.Especificaciones
{
    public class ValidarUsuarioPassword : AValidarUsuario
    {
        public override void EsValido(IUsuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Password))
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.PasswordNullOrEmpty);
                exception.Data.Add(erroresAuth.GetNroError(), erroresAuth.GetDescription());
                throw exception;
            }
            //          The regular expression below cheks that a password:
            //          Has minimum 8 characters in length.Adjust it by modifying { 8,}
            //          At least one uppercase English letter. You can remove this condition by removing(?=.*?[A - Z])
            //          At least one lowercase English letter.  You can remove this condition by removing(?=.*?[a - z])
            //          At least one digit. You can remove this condition by removing(?=.*?[0 - 9])
            //          At least one special character,  You can remove this condition by removing(?=.*?[#?!@$%^&*-])
            else if (!Regex.IsMatch(usuario.Password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
            {
                Exception exception = new Exception();
                ErroresAuth erroresAuth = new ErroresAuth(ErroresAuth.ErrorAut.PasswordFormatoIncorrecto);
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
