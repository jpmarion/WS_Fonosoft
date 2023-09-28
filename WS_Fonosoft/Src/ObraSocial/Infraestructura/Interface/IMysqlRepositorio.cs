using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface
{
    public interface IMysqlRepositorio
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        IObraSocial AgregarObraSocial(IObraSocial obraSocial);
        IObraSocial BuscarObraSocialXNombre(string nombre);
    }
}
