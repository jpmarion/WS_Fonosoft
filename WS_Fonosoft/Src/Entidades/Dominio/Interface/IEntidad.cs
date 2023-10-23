namespace WS_Fonosoft.Src.Entidades.Dominio.Interface
{
    public interface IEntidad
    {
        int Id { get; set; }
        IList<IEntidadTipoEntidad> TiposEntidades { get; set; }
        IDatosPersona DatosPersona { get; set; }
        IList<IEntidadObraSocial> ObrasSociales { get; set; }
        IList<IEntidadTipoContacto> Contactos { get; set; }
        IList<IEntidadTipoDocumento> Documentos { get; set; }
    }
}
