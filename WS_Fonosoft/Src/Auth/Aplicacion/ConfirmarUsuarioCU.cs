using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Auth.Dominio.Especificaciones;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Aplicacion
{
    public class ConfirmarUsuarioCU<T> : AEjecutarCU<T>
    {
        private readonly IMysqlRepositorio _mysqlRepositorio;
        private readonly IUsuario _usuario;
        private ValidarUsuarioId _validarUsuarioId = new ValidarUsuarioId();

        public ConfirmarUsuarioCU(IResponse<T> response, IMysqlRepositorio mysqlRepositorio, IUsuario usuario) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            _usuario = usuario;
        }
        public override IList<T> Proceso()
        {
            _validarUsuarioId.EsValido(_usuario);
            _mysqlRepositorio.ConfirmarUsuario(_usuario.Id);

            return null;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
