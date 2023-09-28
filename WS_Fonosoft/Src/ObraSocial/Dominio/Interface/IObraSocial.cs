using WS_Fonosoft.Src.ObraSocial.Dominio.Agregados;

namespace WS_Fonosoft.Src.ObraSocial.Dominio.Interface
{
    public interface IObraSocial
    {
        int Id { get; set; }
        string Nombre { get; set; }
        Periodo Periodo { get; set; }
    }
}
