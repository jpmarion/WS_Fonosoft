using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Agregados
{
    public class DatosPersona : IDatosPersona
    {
        public string Apellido { get; set; }
        public string Nombre { get; set; }
    }
}
