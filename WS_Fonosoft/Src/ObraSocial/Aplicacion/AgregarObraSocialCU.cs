using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.ObraSocial.Dominio.Especificaciones;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Aplicacion
{
    public class AgregarObraSocialCU<T> : AEjecutarCU<T>
    {
        private readonly IMysqlRepositorioObraSocial _mysqlRepositorio;
        private IObraSocial _obraSocial;
        private readonly ValidarObraSocialNombre _validarObraSocialNombre = new ValidarObraSocialNombre();
        private readonly ValidarObrasocialNombreExiste _validarObrasocialNombreExiste;
        private readonly ValidarObraSocialPeriodo _validarObraSocialPeriodo = new ValidarObraSocialPeriodo();
        public AgregarObraSocialCU(IResponse<T> response, IMysqlRepositorioObraSocial mysqlRepositorio, IObraSocial obraSocial) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            _obraSocial = obraSocial;
            _validarObrasocialNombreExiste = new ValidarObrasocialNombreExiste(_mysqlRepositorio);
            _validarObraSocialNombre.ProximaValidacion(_validarObrasocialNombreExiste);
            _validarObrasocialNombreExiste.ProximaValidacion(_validarObraSocialPeriodo);
        }
        public override IList<T> Proceso()
        {
            _validarObraSocialNombre.EsValido(_obraSocial);

            _obraSocial = _mysqlRepositorio.AgregarObraSocial(_obraSocial);
            if (_obraSocial == null)
            {
                return null;
            }

            IList<IObraSocial> lstObraSocial = new List<IObraSocial>
            {
                _obraSocial
            };

            return (IList<T>)lstObraSocial;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
