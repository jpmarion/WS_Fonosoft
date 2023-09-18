using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using CompartidoPE.Modelo;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;

namespace WS_Fonosoft.Src.Auth.Aplicacion
{
    public class ListarErroresCU<T> : AEjecutarCU<T>
    {
        public ListarErroresCU(IResponse<T> response) : base(response) { }
        public override IList<T> Proceso()
        {
            IList<IError> errors = new List<IError>();
            foreach (Enum item in Enum.GetValues(typeof(ErroresAuth.ErrorAut)))
            {
                ErroresAuth erroresAuth = new ErroresAuth(item);
                IError error = new Error();
                error.NroError = erroresAuth.GetNroError();
                error.MsgError = erroresAuth.GetDescription();
                errors.Add(error);  
            }
            return (IList<T>)errors;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
