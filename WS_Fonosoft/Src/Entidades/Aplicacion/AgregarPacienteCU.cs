using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;
using WS_Fonosoft.Src.Entidades.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Entidades.Aplicacion
{
    public class AgregarPacienteCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioEntidad _repositorioEntidad;
        private IEntidad _paciente;

        public AgregarPacienteCU(IResponse<T> response, IRepositorioEntidad repositorioEntidad, IEntidad paciente) : base(response)
        {
            _repositorioEntidad = repositorioEntidad;
            _paciente = paciente;
        }

        public override IList<T> Proceso()
        {

            _paciente = _repositorioEntidad.AgregarPaciente(_paciente);

            IList<IEntidad> lstPaciente = new List<IEntidad>
            {
                _paciente
            };

            return (IList<T>)lstPaciente;

        }
        public override void BeginTransaction()
        {
            _repositorioEntidad.BeginTransaction();
        }
        public override void CommitTransaction()
        {
            _repositorioEntidad.CommitTransaction();
        }
        public override void RollbackTransaction()
        {
            _repositorioEntidad.RollbackTransaction();
        }
    }
}
