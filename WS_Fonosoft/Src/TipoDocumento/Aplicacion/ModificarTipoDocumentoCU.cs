using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Abstracta;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Especificaciones;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;
using WS_Fonosoft.Src.TipoDocumento.Infraestructura.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Aplicacion
{
    public class ModificarTipoDocumentoCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioTipoDocumento _fonosoftTipoDocumentoRepo;
        private readonly ITipoDocumento _tipoDocumento;
        private readonly AValidarTipoDocumento _validarTipoDocumentoId = new ValidarTipoDocumentoId();
        private readonly AValidarTipoDocumento _validarTipoDocumentoNombreExiste;

        public ModificarTipoDocumentoCU(IResponse<T> response, IRepositorioTipoDocumento fonosoftTipoDocumentoRepo, ITipoDocumento tipoDocumento) : base(response)
        {
            _fonosoftTipoDocumentoRepo = fonosoftTipoDocumentoRepo;
            _tipoDocumento = tipoDocumento;
            _validarTipoDocumentoNombreExiste = new ValidarTipoDocumentoNombreExiste(_fonosoftTipoDocumentoRepo);
            _validarTipoDocumentoId.ProximaValidacion(_validarTipoDocumentoNombreExiste);
        }

        public override IList<T> Proceso()
        {
            _validarTipoDocumentoId.EsValido(_tipoDocumento);

            _fonosoftTipoDocumentoRepo.ModificarTipoDocumentoXId(_tipoDocumento);

            return null;
        }
        public override void BeginTransaction() { }

        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
