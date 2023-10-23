using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface
{
    public interface IRepositorioObraSocial
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        IObraSocial AgregarObraSocial(IObraSocial obraSocial);
        IList<IObraSocial> BuscarObrasSociales();
        IList<IObraSocial> BuscarObrasSocialesHabilitadas();
        IObraSocial BuscarObraSocialXId(int Id);
        IObraSocial BuscarObraSocialXNombre(string nombre);
        void ModificarObraSocialXId(IObraSocial obraSocial);
        void HabilitarObraSocialXId(IObraSocial obraSocial);
        void DesHabilitarObraSocialXId(IObraSocial obraSocial);
    }
}
