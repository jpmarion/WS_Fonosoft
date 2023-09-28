using WS_Fonosoft.Src.ObraSocial.Dominio.Abstracta;
using WS_Fonosoft.Src.ObraSocial.Dominio.Entidades;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Dominio.Especificaciones
{
    public class ValidarObraSocialPeriodo : AValidarObraSocial
    {
        public override void EsValido(IObraSocial obraSocial)
        {
            if (string.IsNullOrEmpty(obraSocial.Periodo.FechaInicio.ToString()))
            {
                Exception exception = new Exception();
                ErroresObraSocial error = new ErroresObraSocial(ErroresObraSocial.ErrorObraSocial.FechaInicioNullOrEmpty);
                exception.Data.Add(error.GetNroError(), error.GetDescription());
                throw exception;
            }
            else if (string.IsNullOrEmpty(obraSocial.Periodo.FechaFin.ToString()))
            {
                Exception exception = new Exception();
                ErroresObraSocial error = new ErroresObraSocial(ErroresObraSocial.ErrorObraSocial.FechaFinNullOrEmpty);
                exception.Data.Add(error.GetNroError(), error.GetDescription());
                throw exception;
            }
            else if (DateTime.Compare(obraSocial.Periodo.FechaInicio, obraSocial.Periodo.FechaFin) < 0)
            {
                Exception exception = new Exception();
                ErroresObraSocial error = new ErroresObraSocial(ErroresObraSocial.ErrorObraSocial.FechaInicioSuperior);
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
