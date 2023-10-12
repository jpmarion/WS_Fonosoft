using WS_Fonosoft.Src.TipoDocumento.Dominio.Abstracta;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Entidades;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Dominio.Especificaciones
{
    public class ValidarTipoDocumentoNombre : AValidarTipoDocumento
    {
        public override void EsValido(ITipoDocumento tipoDocumento)
        {
            if (string.IsNullOrEmpty(tipoDocumento.Nombre))
            {
                Exception exception = new Exception();
                ErroresTipoDocumento error = new ErroresTipoDocumento(ErroresTipoDocumento.ErrorTipoDocumento.NombreTipoDocumentoNullOrEmpty);
                exception.Data.Add(error.GetNroError(), error.GetDescription());
                throw exception;
            }

            if (_validador != null)
            {
                _validador.EsValido(tipoDocumento);
            }
        }
    }
}
