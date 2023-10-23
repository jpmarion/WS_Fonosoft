using WS_Fonosoft.Src.Entidades.Dominio.Abstracta;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Factoria
{
    public class EntidadDirector
    {
        public IEntidad CrearEntidad(EntidadBuilder entidadBuilder)
        {
            entidadBuilder.CrearEntidad();
            entidadBuilder.SetTipoEntidad();
            entidadBuilder.SetDatosPersona();
            entidadBuilder.SetObrasSociales();
            entidadBuilder.SetContactos();
            entidadBuilder.SetDocumentos();
            return entidadBuilder.GetEntidad();
        }
    }
}
