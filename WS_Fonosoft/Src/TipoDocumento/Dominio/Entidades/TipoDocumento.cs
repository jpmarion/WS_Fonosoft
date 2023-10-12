using WS_Fonosoft.Src.TipoDocumento.Dominio.Agregados;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Dominio.Entidades
{
    public class TipoDocumento : ITipoDocumento
    {
        public TipoDocumento()
        {
            Periodo = new Periodo();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Periodo Periodo { get; set; }
    }
}
