using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.ObraSocial.Dominio.Especificaciones;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Aplicacion
{
    public class BuscarObraSocialXIdCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioObraSocial _mysqlRepositorio;
        private IObraSocial _obraSocial;
        private readonly ValidarObraSocialId _validarObraSocialId = new ValidarObraSocialId();

        public BuscarObraSocialXIdCU(IResponse<T> response, IRepositorioObraSocial mysqlRepositorio, IObraSocial obraSocial) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            _obraSocial = obraSocial;
        }

        public override IList<T> Proceso()
        {
            _validarObraSocialId.EsValido(_obraSocial);

            IObraSocial obraSocial = _mysqlRepositorio.BuscarObraSocialXId(_obraSocial.Id);
            IList<IObraSocial> lstObraSocial = new List<IObraSocial>();
            lstObraSocial.Add(obraSocial);

            return (IList<T>)lstObraSocial;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
