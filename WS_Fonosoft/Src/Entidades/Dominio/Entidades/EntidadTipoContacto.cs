using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Entidades
{
    public class EntidadTipoContacto : IEntidadTipoContacto
    {
        public int IdEntidad { get; set; }
        public int IdTipoContacto { get; set; }
        public string Contacto { get; set; }
        public string TipoContacto { get ; set ; }
    }
}
