using WS_Fonosoft.Src.ObraSocial.Dominio.Abstracta;
using WS_Fonosoft.Src.ObraSocial.Dominio.Entidades;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

namespace WS_Fonosoft.Src.ObraSocial.Dominio.Especificaciones
{
    public class ValidarObraSocialIdExiste : AValidarObraSocial
    {
        private readonly IMysqlRepositorioObraSocial _mysqlRepositorio;

        public ValidarObraSocialIdExiste(IMysqlRepositorioObraSocial mysqlRepositorio)
        {
            _mysqlRepositorio = mysqlRepositorio;
        }

        public override void EsValido(IObraSocial obraSocial)
        {
            IObraSocial obraSocialExiste = _mysqlRepositorio.BuscarObraSocialXId(obraSocial.Id);
            if (obraSocialExiste !=null)
            {
                Exception exception = new Exception();
                ErroresObraSocial error = new ErroresObraSocial(ErroresObraSocial.ErrorObraSocial.ObraSocialInexistente);
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
