using WS_Fonosoft.Src.Entidades.Dominio.Abstracta;
using WS_Fonosoft.Src.Entidades.Dominio.Agregados;
using WS_Fonosoft.Src.Entidades.Dominio.Entidades;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;

namespace WS_Fonosoft.Src.Entidades.Dominio.Factoria
{
    public class EntidadFactory
    {
        public static IEntidad GetEntidad(TipoEntidad.EnumTipoEntidad tipoEntidad)
        {
            IEntidad entidad = null;
            switch (tipoEntidad)
            {
                case TipoEntidad.EnumTipoEntidad.Paciente:
                    EntidadDirector entidadDirector = new EntidadDirector();
                    EntidadBuilder entidadBuilder = new PacienteBuilder();
                    entidad = entidadDirector.CrearEntidad(entidadBuilder);
                    break;
            }
            return entidad;
        }
    }
}
