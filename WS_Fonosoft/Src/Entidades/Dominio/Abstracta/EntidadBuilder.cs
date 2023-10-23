using WS_Fonosoft.Src.Entidades.Dominio.Entidades;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Abstracta
{
    public abstract class EntidadBuilder
    {
        protected IEntidad _entidad;
        public void CrearEntidad()
        {
            _entidad = new Entidad();
        }
        public IEntidad GetEntidad()
        {
            return _entidad;
        }
        public abstract void SetTipoEntidad();
        public abstract void SetDatosPersona();
        public abstract void SetObrasSociales();
        public abstract void SetContactos();
        public abstract void SetDocumentos();
    }
}
