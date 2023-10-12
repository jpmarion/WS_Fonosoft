using WS_Fonosoft.Src.TipoDocumento.Dominio.Abstracta;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Entidades;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Dominio.Especificaciones
{
    public class ValidarTipoDocumentoId : AValidarTipoDocumento
    {
        public override void EsValido(ITipoDocumento tipoDocumento)
        {
            if (string.IsNullOrEmpty(tipoDocumento.Id.ToString()) || tipoDocumento.Id == 0)
            {
                Exception exception = new Exception();
                ErroresTipoDocumento error = new ErroresTipoDocumento(ErroresTipoDocumento.ErrorTipoDocumento.IdTipoDocumentoNullOrEmpty);
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
