using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Entidades.Dominio.Especificaciones;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;
using WS_Fonosoft.Src.Entidades.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Entidades.Aplicacion
{
    public class ModificarPacienteCU<T> : AEjecutarCU<T>
    {
        private readonly IRepositorioEntidad _repositorioEntidad;
        private readonly IEntidad _paciente;
        private readonly ValidarEntidadPacienteExiste _validarEntidadPacienteExiste;
        private readonly ValidarEntidadDatosPersona _validarEntidadDatosPersona = new ValidarEntidadDatosPersona() ;
        private readonly ValidarEntidadContactos _validarEntidadContactos = new ValidarEntidadContactos() ;
        private readonly ValidarEntidadObrasSociales _validarEntidadObrasSociales = new ValidarEntidadObrasSociales() ;

        public ModificarPacienteCU(IResponse<T> response, IRepositorioEntidad repositorioEntidad, IEntidad paciente) : base(response)
        {
            _repositorioEntidad = repositorioEntidad;
            _paciente = paciente;
            _validarEntidadPacienteExiste = new ValidarEntidadPacienteExiste(_repositorioEntidad);
            _validarEntidadPacienteExiste.ProximaValidacion(_validarEntidadDatosPersona);
            _validarEntidadDatosPersona.ProximaValidacion(_validarEntidadContactos);
            _validarEntidadContactos.ProximaValidacion(_validarEntidadObrasSociales);
        }

        public override IList<T> Proceso()
        {
            _validarEntidadPacienteExiste.EsValido(_paciente);


            throw new NotImplementedException();
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
