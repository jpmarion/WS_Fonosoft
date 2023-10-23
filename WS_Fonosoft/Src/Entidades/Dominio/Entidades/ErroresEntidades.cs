using AuthPeDDD.Compartido.Abstracta;
using System.ComponentModel;

namespace WS_Fonosoft.Src.Entidades.Dominio.Entidades
{
    public class ErroresEntidades : AErrores
    {
        public enum ErrorEntidad
        {
            [Description("Ingrese el apellido")]
            ApellidoNullOrEmpty = 0,
            [Description("Ingrese el nombre")]
            NombreNullOrEmpty = 1,
            [Description("Ingrese el IdEntidad")]
            IdEntidadNullOrEmpty = 2,
            [Description("Ingrese el IdObraSocial")]
            IdObraSocialNullOrEmpty = 3,
            [Description("Ingrese el Nro. Obra Social")]
            NroObraSocialNullOrEmpty = 4,
            [Description("Ingrese el IdTipoContacto")]
            IdTipoContactoNullOrEmpty = 5,
            [Description("Ingrese el contacto")]
            ContactoNullOrEmpty = 6,
            [Description("Paciente inexistente")]
            PacienteInexistente = 7
        }
        public ErroresEntidades(Enum enumValor) : base(enumValor) { }
    }
}
