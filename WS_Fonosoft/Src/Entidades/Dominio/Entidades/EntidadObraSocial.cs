using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Entidades
{
    public class EntidadObraSocial : IEntidadObraSocial
    {
        public int IdEntidad { get; set; }
        public int IdObraSocial { get; set; }
        public string ObraSocial { get; set; }
        public string NroObraSocial { get; set; }
    }
}
