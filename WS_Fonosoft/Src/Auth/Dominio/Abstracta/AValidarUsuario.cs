using WS_Fonosoft.Src.Auth.Dominio.Interface;

namespace WS_Fonosoft.Src.Auth.Dominio.Abstracta
{
    public abstract class AValidarUsuario
    {
        public AValidarUsuario _validador;
        public void ProximaValidcion(AValidarUsuario validador)
        {
            _validador = validador;
        }
        public abstract void EsValido(IUsuario usuario);
    }
}
