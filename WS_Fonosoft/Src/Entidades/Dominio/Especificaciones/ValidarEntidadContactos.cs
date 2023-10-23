using WS_Fonosoft.Src.Entidades.Dominio.Abstracta;
using WS_Fonosoft.Src.Entidades.Dominio.Entidades;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Especificaciones
{
    public class ValidarEntidadContactos : AValidarEntidad
    {
        public override void EsValido(IEntidad entidad)
        {
            foreach (IEntidadTipoContacto item in entidad.Contactos)
            {
                if (string.IsNullOrEmpty(item.IdEntidad.ToString()) || item.IdEntidad == 0)
                {
                    Exception exception = new Exception();
                    ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.IdEntidadNullOrEmpty);
                    exception.Data.Add(error.GetNroError(), error.GetDescription());
                    throw exception;
                }
                else if (string.IsNullOrEmpty(item.IdTipoContacto.ToString()) || item.IdTipoContacto == 0)
                {
                    Exception exception = new Exception();
                    ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.IdEntidadNullOrEmpty);
                    exception.Data.Add(error.GetNroError(), error.GetDescription());
                    throw exception;

                }else if (string.IsNullOrEmpty (item.Contacto))
                {
                    Exception exception = new Exception();
                    ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.ContactoNullOrEmpty);
                    exception.Data.Add(error.GetNroError(), error.GetDescription());
                    throw exception;
                }
            }

            if (_validador != null)
            {
                _validador.EsValido(entidad);
            }
        }
    }
}
