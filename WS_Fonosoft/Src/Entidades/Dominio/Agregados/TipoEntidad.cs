using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Agregados
{

    public class TipoEntidad : ITipoEntidad
    {
        public enum EnumTipoEntidad
        {
            Paciente = 1
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
