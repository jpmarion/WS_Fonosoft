using CifradoPE.Infraestructura.Interface;
using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using WS_Fonosoft.Src.Auth.Dominio.Especificaciones;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

namespace WS_Fonosoft.Src.Auth.Aplicacion
{
    public class ResetContraseniaCU<T> : AEjecutarCU<T>
    {
        private readonly IMysqlRepositorioAuth _mysqlRepositorio;
        private readonly IEmailRepo _emailRepo;
        private readonly IAes _aes;
        private IUsuario _usuario;
        private readonly ValidarUsuarioNombreUsuario _validarUsuarioNombreUsuario;
        private readonly ValidarUsuarioPassword _validarUsuarioPassword = new ValidarUsuarioPassword();

        public ResetContraseniaCU(IResponse<T> response, IMysqlRepositorioAuth mysqlRepositorio, IEmailRepo emailRepo, IAes aes, IUsuario usuario) : base(response)
        {
            _mysqlRepositorio = mysqlRepositorio;
            this._emailRepo = emailRepo;
            _aes = aes;
            _usuario = usuario;
            _validarUsuarioNombreUsuario = new ValidarUsuarioNombreUsuario(_mysqlRepositorio);
        }
        public override IList<T> Proceso()
        {
            _validarUsuarioNombreUsuario.EsValido(_usuario);

            _usuario = _mysqlRepositorio.BuscarUsuarioXNombreUsuario(_aes.Encriptar(_usuario.NombreUsuario));
            string email = _aes.Desencriptar(_usuario.Email);

            string nuevoPassword = GetRandomPassword(8);
            _usuario.Password = _aes.Encriptar(nuevoPassword);

            _mysqlRepositorio.ResetPasswordUsuario(_usuario.NombreUsuario, _usuario.Password);

            ICollection<string> emails = new List<string>{
                    email
                };
            string asunto = "Confirmar Registro";
            string cuerpo = GetBodyResetContraseña(nuevoPassword);
            _emailRepo.EnviarEmail(emails, asunto, cuerpo, true);

            IList<IUsuario> lstUsuario = new List<IUsuario>
            {
                _usuario
            };

            return (IList<T>)lstUsuario;
        }
        private string GetBodyResetContraseña(string NuevoPassword)
        {
            string htmlString = @"<html>
                      <body>
                      <p>Confirmar registro</p>
                      <p>@nuevoPassword</br></p>
                      </body>
                      </html>
                     ";
            return htmlString.Replace("@nuevoPassword", NuevoPassword);
        }
        private static string GetRandomPassword(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$%&/?¡¿";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            do
            {
                sb.Clear();
                for (int i = 0; i < length; i++)
                {   
                    int index = rnd.Next(chars.Length);
                    sb.Append(chars[index]);
                }
            } while (!Regex.IsMatch(sb.ToString(), @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"));

            return sb.ToString();
        }
        public override void BeginTransaction() { }
        public override void CommitTransaction() { }
        public override void RollbackTransaction() { }
    }
}
