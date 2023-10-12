using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Especificaciones;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;
using WS_Fonosoft.Src.TipoDocumento.Infraestructura.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Aplicacion
{
    public class BuscarTipoDocumentoXIdCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioTipoDocumento _fonosoftTipoDocumentoRepo;
        private readonly ITipoDocumento _tipoDocumento;
        private ValidarTipoDocumentoId _validarTipoDocumentoId = new ValidarTipoDocumentoId();

        public BuscarTipoDocumentoXIdCU(IResponse<T> response, IRepositorioTipoDocumento fonosoftTipoDocumentoRepo, ITipoDocumento tipoDocumento) : base(response)
        {
            _fonosoftTipoDocumentoRepo = fonosoftTipoDocumentoRepo;
            _tipoDocumento = tipoDocumento;
        }


        public override IList<T> Proceso()
        {
            _validarTipoDocumentoId.EsValido(_tipoDocumento);

            ITipoDocumento tipoDocumento = _fonosoftTipoDocumentoRepo.BuscarTipoDocumentoXId(_tipoDocumento.Id);

            IList<ITipoDocumento> lstTipoDocumento = new List<ITipoDocumento>
            {
                tipoDocumento
            };

            return (IList<T>)lstTipoDocumento;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
