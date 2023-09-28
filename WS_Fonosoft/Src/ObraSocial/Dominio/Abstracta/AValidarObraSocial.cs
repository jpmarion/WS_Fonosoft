using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Dominio.Abstracta
{
    public abstract class AValidarObraSocial
    {
        public AValidarObraSocial _validador;
        public void ProximaValidacion(AValidarObraSocial validador)
        {
            _validador = validador;
        }
        public abstract void EsValido(IObraSocial obraSocial);
    }
}
