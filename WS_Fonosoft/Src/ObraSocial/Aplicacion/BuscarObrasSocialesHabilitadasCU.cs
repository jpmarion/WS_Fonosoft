using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Aplicacion
{
    public class BuscarObrasSocialesHabilitadasCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioObraSocial _mysqlRepositorio;

        public BuscarObrasSocialesHabilitadasCU(IResponse<T> response, IRepositorioObraSocial mysqlRepositorio) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
        }

        public override IList<T> Proceso()
        {
            return (IList<T>)_mysqlRepositorio.BuscarObrasSocialesHabilitadas();
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
