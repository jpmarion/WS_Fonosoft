using WS_Fonosoft.Src.Auth.Dominio.Interface;

namespace WS_Fonosoft.Src.Auth.Infraestructura.Interface
{
    public interface IRepositorioAuth
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        #region USUARIO
        IUsuario RegistrarUsuario(IUsuario usuario);
        IUsuario BuscarUsuarioXNombreUsuario(string nombreUsuario);
        void ConfirmarUsuario(int id);
        IUsuario BuscarUsuarioXNombreUsuarioXPassword(string NombreUsuario, string Password);
        void ResetPasswordUsuario(string NombreUsuario, string Password);
        void ModificarContrasenia(int IdUsuario, string Password);
        #endregion
    }
}
