using AuthPeDDD.Compartido.Abstracta;
using WS_Fonosoft.Src.ObraSocial.Dominio.Abstracta;
using WS_Fonosoft.Src.ObraSocial.Dominio.Entidades;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Dominio.Especificaciones
{
    public class ValidarObraSocialId : AValidarObraSocial
    {
        public override void EsValido(IObraSocial obraSocial)
        {
            if (string.IsNullOrEmpty(obraSocial.Id.ToString()) || obraSocial.Id == 0)
            {
                Exception exception = new Exception();
                AErrores error = new ErroresObraSocial(ErroresObraSocial.ErrorObraSocial.IdObraSocialNullOrEmpty);
                exception.Data.Add(error.GetNroError(), error.GetDescription());
                throw exception;
            }

            if (_validador != null)
            {
                _validador.EsValido(obraSocial);
            }
        }
    }
}
