using WS_Fonosoft.Src.TipoContacto.Dominio.Interface;

namespace WS_Fonosoft.Src.TipoContacto.Infraestructura.Interface
{
    public interface IRepositorioTipoContacto
    {
        void BeginTransaction();
        IList<ITipoContacto> BuscarTipoContactos();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
