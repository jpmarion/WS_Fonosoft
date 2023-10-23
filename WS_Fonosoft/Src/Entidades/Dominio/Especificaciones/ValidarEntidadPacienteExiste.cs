using WS_Fonosoft.Src.Entidades.Dominio.Abstracta;
using WS_Fonosoft.Src.Entidades.Dominio.Entidades;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;
using WS_Fonosoft.Src.Entidades.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Especificaciones
{
    public class ValidarEntidadPacienteExiste : AValidarEntidad
    {
        private readonly IRepositorioEntidad _repositorioEntidad;

        public ValidarEntidadPacienteExiste(IRepositorioEntidad repositorioEntidad)
        {
            _repositorioEntidad = repositorioEntidad;
        }

        public override void EsValido(IEntidad entidad)
        {
            IEntidad paciente = _repositorioEntidad.BuscarPacienteXId(entidad.Id);
            if (paciente == null)
            {
                Exception exception = new Exception();
                ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.PacienteInexistente);
                exception.Data.Add(error.GetNroError(), error.GetDescription());
                throw exception;
            }

            if (_validador != null)
            {
                _validador.EsValido(entidad);
            }
        }
    }
}
