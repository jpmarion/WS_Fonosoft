using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;
using WS_Fonosoft.Src.Entidades.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Entidades.Aplicacion
{
    public class BuscarPacienteXIdCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioEntidad _repositorioEntidad;
        private IEntidad _paciente;

        public BuscarPacienteXIdCU(IResponse<T> response, IRepositorioEntidad repositorioEntidad, IEntidad paciente) : base(response)
        {
            _repositorioEntidad = repositorioEntidad;
            _paciente = paciente;
        }

        public override IList<T> Proceso()
        {
            _paciente = _repositorioEntidad.BuscarPacienteXId(_paciente.Id);

            if (_paciente == null)
            {
                return null;
            }

            IList<IEntidad> lstPaciente = new List<IEntidad>
            {
                _paciente
            };
            
            return (IList<T>)lstPaciente;
        }

        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
