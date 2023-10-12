using AuthPeDDD.Compartido.Abstracta;
using System.ComponentModel;

namespace WS_Fonosoft.Src.TipoDocumento.Dominio.Entidades
{
    public class ErroresTipoDocumento : AErrores
    {
        public enum ErrorTipoDocumento
        {
            [Description("Ingrese el Nombre del tipo de documento")]
            NombreTipoDocumentoNullOrEmpty = 0,
            [Description("Tipo documento existente")]
            NombreTipoDocumentoExistente = 1,
            [Description("Ingrese el Id del tipo de documento")]
            IdTipoDocumentoNullOrEmpty = 2
        }
        public ErroresTipoDocumento(Enum enumValor) : base(enumValor) { }
    }
}
