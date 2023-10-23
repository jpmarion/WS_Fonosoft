using WS_Fonosoft.Src.Entidades.Dominio.Abstracta;
using WS_Fonosoft.Src.Entidades.Dominio.Entidades;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Especificaciones
{
    public class ValidarEntidadObrasSociales : AValidarEntidad
    {
        public override void EsValido(IEntidad entidad)
        {
            foreach (IEntidadObraSocial item in entidad.ObrasSociales)
            {
                if (string.IsNullOrEmpty(item.IdEntidad.ToString()) || item.IdEntidad == 0)
                {
                    Exception exception = new Exception();
                    ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.IdEntidadNullOrEmpty);
                    exception.Data.Add(error.GetNroError(), error.GetDescription());
                    throw exception;
                }
                else if (string.IsNullOrEmpty(item.IdObraSocial.ToString()) || item.IdObraSocial == 0)
                {
                    Exception exception = new Exception();
                    ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.IdObraSocialNullOrEmpty);
                    exception.Data.Add(error.GetNroError(), error.GetDescription());
                    throw exception;
                }
                else if (string.IsNullOrEmpty(item.NroObraSocial))
                {
                    Exception exception = new Exception();
                    ErroresEntidades error = new ErroresEntidades(ErroresEntidades.ErrorEntidad.NroObraSocialNullOrEmpty);
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
