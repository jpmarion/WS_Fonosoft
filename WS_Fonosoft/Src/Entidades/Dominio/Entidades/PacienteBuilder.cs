using WS_Fonosoft.Src.Entidades.Dominio.Abstracta;
using WS_Fonosoft.Src.Entidades.Dominio.Agregados;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Entidades
{
    public class PacienteBuilder : EntidadBuilder
    {
        public override void SetDatosPersona()
        {
            _entidad.DatosPersona = new DatosPersona();
        }
        public override void SetObrasSociales()
        {
            _entidad.ObrasSociales = new List<IEntidadObraSocial>();
        }
        public override void SetTipoEntidad()
        {
            _entidad.TiposEntidades = new List<IEntidadTipoEntidad>();
        }
        public override void SetContactos()
        {
            _entidad.Contactos = new List<IEntidadTipoContacto>();
        }
        public override void SetDocumentos()
        {
            _entidad.Documentos = new List<IEntidadTipoDocumento>();
        }
    }
}