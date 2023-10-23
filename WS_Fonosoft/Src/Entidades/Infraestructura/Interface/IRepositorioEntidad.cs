using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Infraestructura.Interface
{
    public interface IRepositorioEntidad
    {
        IEntidad AgregarPaciente(IEntidad paciente);
        void AgregarEntidadObraSocial(IEntidadObraSocial entidadObraSocial);
        void AgregarEntidadTipoContacto(IEntidadTipoContacto entidadTipoContacto);
        void AgregarEntidadTipoEntidad(IEntidadTipoEntidad entidadTipoEntidad);
        IList<IEntidad> BuscarPacientes();
        IEntidad BuscarPacienteXId(int IdPaciente);
        IList<IEntidadObraSocial> BuscarObrasSocialesXIdEntidad(int IdEntidad);
        IList<IEntidadTipoContacto> BuscarContactosXIdEntidad(int IdEntidad);
        IList<IEntidadTipoEntidad> BuscarTiposEntidadesXIdEntidad(int IdEntidad);
        IList<IEntidadTipoDocumento> BuscarDocumentosXIdEntidad(int IdEntidad);
        
        
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
