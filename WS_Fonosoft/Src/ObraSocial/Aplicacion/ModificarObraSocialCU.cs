using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.ObraSocial.Dominio.Abstracta;
using WS_Fonosoft.Src.ObraSocial.Dominio.Especificaciones;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Aplicacion
{
    public class ModificarObraSocialCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioObraSocial _mysqlRepositorio;
        private readonly IObraSocial _obraSocial;
        private readonly AValidarObraSocial _validarObraSocialId = new ValidarObraSocialId();
        private readonly AValidarObraSocial _validarObraSocialNombre = new ValidarObraSocialNombre();
        private readonly AValidarObraSocial _validarObraSocialNoIdExiste;

        public ModificarObraSocialCU(IResponse<T> response, IRepositorioObraSocial mysqlRepositorio, IObraSocial obraSocial) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            _obraSocial = obraSocial;
            _validarObraSocialNoIdExiste = new ValidarObraSocialIdNoExiste(_mysqlRepositorio);
            _validarObraSocialId.ProximaValidacion(_validarObraSocialNombre);
            _validarObraSocialNombre.ProximaValidacion(_validarObraSocialNoIdExiste);
        }
        public override IList<T> Proceso()
        {
            _validarObraSocialId.EsValido(_obraSocial);

            _mysqlRepositorio.ModificarObraSocialXId(_obraSocial);
            
            return null;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
