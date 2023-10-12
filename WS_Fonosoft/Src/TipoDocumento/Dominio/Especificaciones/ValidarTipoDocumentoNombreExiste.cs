using WS_Fonosoft.Src.TipoDocumento.Dominio.Abstracta;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Entidades;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;
using WS_Fonosoft.Src.TipoDocumento.Infraestructura.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Dominio.Especificaciones
{
    public class ValidarTipoDocumentoNombreExiste : AValidarTipoDocumento
    {
        private readonly IRepositorioTipoDocumento _fonosoftTipoDocumentoRepo;

        public ValidarTipoDocumentoNombreExiste(IRepositorioTipoDocumento fonosoftTipoDocumentoRepo)
        {
            _fonosoftTipoDocumentoRepo = fonosoftTipoDocumentoRepo;
        }

        public override void EsValido(ITipoDocumento tipoDocumento)
        {
            ITipoDocumento tipoDocumentoExiste = _fonosoftTipoDocumentoRepo.BuscarTipoDocumentoXNombre(tipoDocumento.Nombre);
            if (tipoDocumentoExiste != null)
            {
                Exception exception = new Exception();
                ErroresTipoDocumento error = new ErroresTipoDocumento(ErroresTipoDocumento.ErrorTipoDocumento.NombreTipoDocumentoExistente);
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
