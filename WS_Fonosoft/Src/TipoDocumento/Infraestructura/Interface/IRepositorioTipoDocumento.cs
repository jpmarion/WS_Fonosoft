using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;

namespace WS_Fonosoft.Src.TipoDocumento.Infraestructura.Interface
{
    public interface IRepositorioTipoDocumento
    {
        ITipoDocumento AgregarTipoDocumento(ITipoDocumento tipoDocumento);
        IList<ITipoDocumento> BuscarTiposDocumentos();
        ITipoDocumento BuscarTipoDocumentoXId(int id);
        ITipoDocumento BuscarTipoDocumentoXNombre(string nombre);
        void ModificarTipoDocumentoXId(ITipoDocumento tipoDocumento);
        void CambiarEstadoTipoDocumentoXId(int IdTipoDocumento);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
