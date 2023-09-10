using CifradoPE.Infraestructura.Interface;
using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using WS_Fonosoft.Src.Auth.Dominio.Especificaciones;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Aplicacion
{
    public class ModificarContraseniaCU<T> : AEjecutarCU<T>
    {
        private readonly IMysqlRepositorio _mysqlRepositorio;
        private readonly IAes _aes;
        private readonly IUsuario _usuario;
        private ValidarUsuarioId _validarUsuarioId = new ValidarUsuarioId();
        private ValidarUsuarioPassword _validarUsuarioPassword = new ValidarUsuarioPassword();

        public ModificarContraseniaCU(IResponse<T> response, IMysqlRepositorio mysqlRepositorio, IAes aes, IUsuario usuario) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            _aes = aes;
            _usuario = usuario;
            _validarUsuarioId.ProximaValidcion(_validarUsuarioPassword);
        }
        public override IList<T> Proceso()
        {
            _validarUsuarioId.EsValido(_usuario);

            _usuario.Password=_aes.Encriptar(_usuario.Password);

            _mysqlRepositorio.ModificarContrasenia(_usuario.Id, _usuario.Password);

            return null;
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
