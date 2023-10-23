using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Entidades
{
    public class Entidad : IEntidad
    {
        public int Id { get; set; }
        public IList<IEntidadTipoEntidad> TiposEntidades { get; set; }
        public IDatosPersona DatosPersona { get; set; }
        public IList<IEntidadObraSocial> ObrasSociales { get; set; }
        public IList<IEntidadTipoContacto> Contactos { get; set; }
        public IList<IEntidadTipoDocumento> Documentos { get; set; }
    }
}
