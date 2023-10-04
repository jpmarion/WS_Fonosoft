using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface
{
    public interface IMysqlRepositorioObraSocial
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        IObraSocial AgregarObraSocial(IObraSocial obraSocial);
        IList<IObraSocial> BuscarObrasSociales();
        IObraSocial BuscarObraSocialXId(int Id);
        IObraSocial BuscarObraSocialXNombre(string nombre);
        void ModificarObraSocialXId(IObraSocial obraSocial);
        void HabilitarObraSocialXId(IObraSocial obraSocial);
        void DesHabilitarObraSocialXId(IObraSocial obraSocial);
    }
}
