using AuthPeDDD.Compartido.Abstracta;
using System.ComponentModel;

namespace WS_Fonosoft.Src.ObraSocial.Dominio.Entidades
{
    public class ErroresObraSocial : AErrores
    {
        public enum ErrorObraSocial
        {
            [Description("Ingrese el nombre de la obra social")]
            NombreObraSocialNullOrEmpty = 0,
            [Description("Ingrese la fecha de inicio")]
            FechaInicioNullOrEmpty = 1,
            [Description("Ingrese la fecha de fin")]
            FechaFinNullOrEmpty = 2,
            [Description("La fecha de ingresa inicio no puede ser superior a la fecha de fin")]
            FechaInicioSuperior = 3,
            [Description("Obra social existente")]
            NombreObraSocialExistente = 4,
            [Description("Ingrese el nombre de la obra social")]
            IdObraSocialNullOrEmpty = 5,
            [Description("Obra social inexistente")]
            ObraSocialInexistente = 6
        }
        public ErroresObraSocial(Enum enumValor) : base(enumValor) { }
    }
}
