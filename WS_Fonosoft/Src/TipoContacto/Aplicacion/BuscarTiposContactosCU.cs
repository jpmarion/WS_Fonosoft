using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.TipoContacto.Dominio.Interface;
using WS_Fonosoft.Src.TipoContacto.Infraestructura.Interface;

namespace WS_Fonosoft.Src.TipoContacto.Aplicacion
{
    public class BuscarTiposContactosCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioTipoContacto _repositorio;

        public BuscarTiposContactosCU(IResponse<T> response, IRepositorioTipoContacto repositorio) : base(response)
        {
            _repositorio = repositorio;
        }
        public override IList<T> Proceso()
        {
            IList<ITipoContacto> lstTipoContacto = _repositorio.BuscarTipoContactos();

            return (IList<T>)lstTipoContacto;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
