using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;
using WS_Fonosoft.Src.Entidades.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Entidades.Aplicacion
{
    public class BuscarPacientesCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioEntidad _repositorioEntidad;

        public BuscarPacientesCU(IResponse<T> response, IRepositorioEntidad repositorioEntidad) : base(response)
        {
            _repositorioEntidad = repositorioEntidad;
        }

        public override IList<T> Proceso()
        {
            IList<IEntidad> lstPaciente = _repositorioEntidad.BuscarPacientes();

            return (IList<T>)lstPaciente;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
