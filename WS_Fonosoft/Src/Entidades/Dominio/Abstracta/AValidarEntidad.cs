using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Abstracta
{
    public abstract class AValidarEntidad
    {
        public AValidarEntidad _validador;

        public void ProximaValidacion( AValidarEntidad validador )
        {
            _validador = validador;
        }
        public abstract void EsValido(IEntidad entidad);
    }
}
