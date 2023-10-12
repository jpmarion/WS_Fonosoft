using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Dominio.Abstracta
{
    public abstract class AValidarTipoDocumento
    {
        public AValidarTipoDocumento _validador;
        public void ProximaValidacion(AValidarTipoDocumento validador)
        {
            _validador = validador;
        }
        public abstract void EsValido(ITipoDocumento tipoDocumento);
    }
}
