using WS_Fonosoft.Src.ObraSocial.Dominio.Agregados;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Dominio.Entidades
{
    

    public class ObraSocial : IObraSocial
    {
        public ObraSocial()
        {
            Periodo = new Periodo();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public Periodo Periodo { get; set; }
    }
}
