using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Abstracta;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Especificaciones;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;
using WS_Fonosoft.Src.TipoDocumento.Infraestructura.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Aplicacion
{
    public class AgregarTipoDocumentoCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioTipoDocumento _fonosoftTipoDocumentoRepo;
        private ITipoDocumento _tipoDocumento;
        private readonly AValidarTipoDocumento _validarTipoDocumento = new ValidarTipoDocumentoNombre();
        private readonly AValidarTipoDocumento _validarTipoDocumentoNombreExiste;

        public AgregarTipoDocumentoCU(IResponse<T> response, IRepositorioTipoDocumento fonosoftTipoDocumentoRepo, ITipoDocumento tipoDocumento) : base(response)
        {
            _fonosoftTipoDocumentoRepo = fonosoftTipoDocumentoRepo;
            _tipoDocumento = tipoDocumento;
            _validarTipoDocumentoNombreExiste = new ValidarTipoDocumentoNombreExiste(_fonosoftTipoDocumentoRepo);
            _validarTipoDocumento.ProximaValidacion(_validarTipoDocumentoNombreExiste);
        }

        public override IList<T> Proceso()
        {
            _validarTipoDocumento.EsValido(_tipoDocumento);

            _tipoDocumento = _fonosoftTipoDocumentoRepo.AgregarTipoDocumento(_tipoDocumento);

            IList<ITipoDocumento> lstTipoDocumento = new List<ITipoDocumento>
            {
                _tipoDocumento
            };
            return (IList<T>)lstTipoDocumento;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
