using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;
using WS_Fonosoft.Src.TipoDocumento.Infraestructura.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Aplicacion
{
    public class BuscarTiposDocumentosHabilitadosCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioTipoDocumento _fonosoftTipoDocumentoRepo;

        public BuscarTiposDocumentosHabilitadosCU(IResponse<T> response, IRepositorioTipoDocumento fonosoftTipoDocumentoRepo) : base(response)
        {
            _fonosoftTipoDocumentoRepo = fonosoftTipoDocumentoRepo;
        }

        public override IList<T> Proceso()
        {
            IList<ITipoDocumento> lstTipoDocumento = _fonosoftTipoDocumentoRepo.BuscarTiposDocumentos();

            return (IList<T>)lstTipoDocumento;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
