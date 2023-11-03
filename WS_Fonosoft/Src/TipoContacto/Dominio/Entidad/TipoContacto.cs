using WS_Fonosoft.Src.TipoContacto.Dominio.Interface;

namespace WS_Fonosoft.Src.TipoContacto.Dominio.Entidad
{
    public class TipoContacto : ITipoContacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
