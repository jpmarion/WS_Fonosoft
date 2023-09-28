using WS_Fonosoft.Src.ObraSocial.Dominio.Abstracta;
using WS_Fonosoft.Src.ObraSocial.Dominio.Entidades;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Dominio.Especificaciones
{
    public class ValidarObrasocialNombreExiste : AValidarObraSocial
    {
        private readonly IMysqlRepositorio _mysqlRepositorio;

        public ValidarObrasocialNombreExiste(IMysqlRepositorio mysqlRepositorio)
        {
            _mysqlRepositorio = mysqlRepositorio;
        }

        public override void EsValido(IObraSocial obraSocial)
        {
            IObraSocial obraSocialExiste = _mysqlRepositorio.BuscarObraSocialXNombre(obraSocial.Nombre);
            if (obraSocialExiste != null)
            {
                Exception exception = new Exception();
                ErroresObraSocial error = new ErroresObraSocial(ErroresObraSocial.ErrorObraSocial.NombreObraSocialExistente);
                exception.Data.Add(error.GetNroError(), error.GetDescription());
                throw exception;
            }

            if (_validador != null)
            {
                _validador.EsValido(obraSocial);
            }
        }
    }
}
