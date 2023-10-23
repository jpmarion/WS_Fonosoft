using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Entidades
{
    public class EntidadTipoDocumento : IEntidadTipoDocumento
    {
        public int IdEntidad { get; set; }
        public int IdTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
    }
}
