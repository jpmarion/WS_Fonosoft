using AuthPeDDD.Compartido.Abstracta;
using System.ComponentModel;

namespace WS_Fonosoft.Src.Auth.Dominio.Entidades
{
    public class ErroresAuth : AErrores
    {
        public enum ErrorAut
        {
            [Description("Ingrese nombre de usuario")]
            NombreUsuarioNullOrEmpty = 0,
            [Description("Nombre de usuario existente")]
            NombreUsuarioExistente = 1,
            [Description("Ingrese el email")]
            EmailNullOrEmpty = 2,
            [Description("Formato email incorrecto")]
            EmailFormatoIncorrecto = 3,
            [Description("Ingrese la contraseña")]
            PasswordNullOrEmpty = 4,
            [Description("Formato incorrecto de contraseña")]
            PasswordFormatoIncorrecto = 5,
            [Description("Ingrese el Id del usuario")]
            IdUsuarioNullOrEmpty = 6,
            [Description("Usuario o password incorrecto")]
            UsuarioPassworIncorrecto = 7
        }
        public ErroresAuth(Enum enumValor) : base(enumValor) { }
    }
}
