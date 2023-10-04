using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Aplicacion
{
    public class BuscarObrasSocialesCU<T> : AEjecutarCU<T>
    {
        private readonly IMysqlRepositorioObraSocial _mysqlRepositorio;

        public BuscarObrasSocialesCU(IResponse<T> response, IMysqlRepositorioObraSocial mysqlRepositorio) : base(response)
        {
            this._mysqlRepositorio = mysqlRepositorio;
        }
        public override IList<T> Proceso()
        {
            return (IList<T>)_mysqlRepositorio.BuscarObrasSociales();
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
