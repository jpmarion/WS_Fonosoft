using WS_Fonosoft.Src.TipoDocumento.Dominio.Agregados;

namespace WS_Fonosoft.Src.TipoDocumento.Dominio.Interface
{
    public interface ITipoDocumento
    {
        int Id { get; set; }
        string Nombre { get; set; }
        Periodo Periodo { get; set; }
    }
}
