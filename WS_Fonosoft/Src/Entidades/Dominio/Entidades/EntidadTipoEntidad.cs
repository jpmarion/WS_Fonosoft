using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Entidades
{
    public class EntidadTipoEntidad : IEntidadTipoEntidad
    {
        public int IdEntidad { get; set; }
        public int IdTipoEntidad { get; set; }
        public string TipoEntidad { get; set; }
    }
}
