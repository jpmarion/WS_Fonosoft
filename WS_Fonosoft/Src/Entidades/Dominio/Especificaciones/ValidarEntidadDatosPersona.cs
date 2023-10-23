using WS_Fonosoft.Src.Entidades.Dominio.Abstracta;
using WS_Fonosoft.Src.Entidades.Dominio.Entidades;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Especificaciones
{
    public class ValidarEntidadDatosPersona : AValidarEntidad
    {
        public override void EsValido(IEntidad entidad)
        {
            if (string.IsNullOrEmpty(entidad.DatosPersona.Apellido))
            {
                Exception exception = new Exception();
                ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.ApellidoNullOrEmpty);
                exception.Data.Add(error.GetNroError(), error.GetDescription());
                throw exception;
            }
            else if (string.IsNullOrEmpty(entidad.DatosPersona.Nombre))
            {
                Exception exception = new Exception();
                ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.NombreNullOrEmpty);
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
