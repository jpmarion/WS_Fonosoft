using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.ObraSocial.Dominio.Abstracta;
using WS_Fonosoft.Src.ObraSocial.Dominio.Especificaciones;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Aplicacion
{
    public class CambiarEstadoObraSocialCU<T> : AEjecutarCU<T>
    {
        private readonly IMysqlRepositorioObraSocial _mysqlRepositorio;
        private readonly IObraSocial _obraSocial;
        private readonly AValidarObraSocial _validarObraSocialId = new ValidarObraSocialId();
        private readonly AValidarObraSocial _validarObraSocialNoIdExiste;

        public CambiarEstadoObraSocialCU(IResponse<T> response, IMysqlRepositorioObraSocial mysqlRepositorio, IObraSocial obraSocial) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            _obraSocial = obraSocial;
            _validarObraSocialNoIdExiste = new ValidarObraSocialIdNoExiste(_mysqlRepositorio);
            _validarObraSocialId.ProximaValidacion(_validarObraSocialNoIdExiste);
        }

        public override IList<T> Proceso()
        {
            _validarObraSocialId.EsValido(_obraSocial);

            IObraSocial obraSocial = _mysqlRepositorio.BuscarObraSocialXId(_obraSocial.Id);
            if (obraSocial.Periodo.Estado)
            {
                _mysqlRepositorio.DesHabilitarObraSocialXId(obraSocial);
            }
            else
            {
                _mysqlRepositorio.HabilitarObraSocialXId(obraSocial);
            }

            return null;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
