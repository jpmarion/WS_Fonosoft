using CifradoPE.Infraestructura.Interface;
using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using CompartidoPE.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WS_Fonosoft.Src.Auth.Aplicacion;
using WS_Fonosoft.Src.Auth.Dominio.Entidades;
using WS_Fonosoft.Src.Auth.Dominio.Interface;
using WS_Fonosoft.Src.Auth.Infraestructura.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WS_Fonosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IResponse<IUsuario> _responseUsuario;
        private IResponse<IError> _responseError;
        private readonly IMysqlRepositorio _repoFonoAuth;
        private readonly IAes _aes;

        public UsuarioController(IConfiguration configuration, IResponse<IUsuario> responseUsuario, IResponse<IError> responseError, IMysqlRepositorio repoFonoAuth, IAes aes)
        {
            _configuration = configuration;
            _responseUsuario = responseUsuario;
            _responseError = responseError;
            _repoFonoAuth = repoFonoAuth;
            _aes = aes;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Registrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Registrar([FromServices] IEmailRepo emailRepo,
                               RqsRegistrar rqsRegistrar)
        {
            IUsuario usuario = new Usuario();
            usuario.NombreUsuario = rqsRegistrar.NombreUsuario;
            usuario.Email = rqsRegistrar.Email;
            usuario.Password = rqsRegistrar.Password;

            AEjecutarCU<IUsuario> registrarUsuarioCU = new RegistrarUsuarioCU<IUsuario>(_responseUsuario, _repoFonoAuth, emailRepo, _aes, usuario);
            _responseUsuario = registrarUsuarioCU.Ejecutar();

            if (_responseUsuario.Error.NroError == string.Empty)
            {
                if (_responseUsuario.Data.Count == 0)
                {
                    return NoContent();
                }
                return Created("", null);
            }
            else
            {
                return StatusCode(400, _responseUsuario.Error);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Confirmar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Confirmar(int id)
        {
            IUsuario usuario = new Usuario();
            usuario.Id = id;

            AEjecutarCU<IUsuario> confirmarUsuarioCU = new ConfirmarUsuarioCU<IUsuario>(_responseUsuario, _repoFonoAuth, usuario);
            _responseUsuario = confirmarUsuarioCU.Ejecutar();

            if (_responseUsuario.Error.NroError == string.Empty)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, _responseUsuario.Error);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        [ProducesResponseType(typeof(RqsLogin), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Login(RqsLogin rqsLogin)
        {
            IUsuario usuario = new Usuario();
            usuario.NombreUsuario = rqsLogin.NombreUsuario;
            usuario.Password = rqsLogin.Password;

            AEjecutarCU<IUsuario> loginUsuarioCU = new LoginUsuarioCU<IUsuario>(_responseUsuario, _repoFonoAuth, _aes, usuario);
            _responseUsuario = loginUsuarioCU.Ejecutar();

            if (_responseUsuario.Error.NroError == string.Empty)
            {
                if (_responseUsuario.Data.Count == 0)
                {
                    return NoContent();
                }
                var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString() )
                };

                usuario.Id = _responseUsuario.Data[0].Id;

                RspLogin rspLogin = new RspLogin();
                rspLogin.Id = usuario.Id;
                rspLogin.Token = GenerateToken(authClaims);


                return Ok(rspLogin);
            }
            else
            {
                return StatusCode(400, _responseUsuario.Error);
            }
        }

        [HttpPost]
        [Route("ResetContrasenia")]
        [ProducesResponseType(typeof(RqsLogin), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [Authorize]
        public IActionResult ResetContrasenia([FromServices] IEmailRepo emailRepo, string NombreUsuario)
        {
            IUsuario usuario = new Usuario();
            usuario.NombreUsuario = NombreUsuario;

            AEjecutarCU<IUsuario> resetContraseniaCU = new ResetContraseniaCU<IUsuario>(_responseUsuario, _repoFonoAuth, emailRepo, _aes, usuario);
            _responseUsuario = resetContraseniaCU.Ejecutar();

            if (_responseUsuario.Error.NroError == string.Empty)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, _responseUsuario.Error);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ModificarContrasenia")]
        [ProducesResponseType(typeof(RqsModificarContrasenia), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult ModificarContrasenia(RqsModificarContrasenia rqpsModificarContrasenia)
        {
            IUsuario usuario = new Usuario();
            usuario.Id = rqpsModificarContrasenia.IdUsuario;
            usuario.Password = rqpsModificarContrasenia.Password;

            AEjecutarCU<IUsuario> modificarContraseniaCU = new ModificarContraseniaCU<IUsuario>(_responseUsuario, _repoFonoAuth, _aes, usuario);
            _responseUsuario = modificarContraseniaCU.Ejecutar();

            if (_responseUsuario.Error.NroError == string.Empty)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, _responseUsuario.Error);
            }
        }

        [HttpGet]
        [Route("ListarErrores")]
        [ProducesResponseType(typeof(RspListarErrores), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult ListarErrores()
        {
            AEjecutarCU<IError> listarErroresCU = new ListarErroresCU<IError>(_responseError);
            _responseError = listarErroresCU.Ejecutar();

            if (_responseError.Error.NroError == string.Empty)
            {
                if (_responseError.Data.Count == 0)
                {
                    return NoContent();
                }

                IList<RspListarErrores> lstErrores = new List<RspListarErrores>();
                foreach (IError item in _responseError.Data)
                {
                    RspListarErrores rspListarErrores = new RspListarErrores();
                    rspListarErrores.NroError = item.NroError;
                    rspListarErrores.MsgError = item.MsgError;
                    lstErrores.Add(rspListarErrores);
                }

                return new OkObjectResult(lstErrores);
            }
            else
            {
                return StatusCode(400, _responseError.Error);
            }
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                //Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    public class RqsRegistrar
    {
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RqsModificarContrasenia
    {
        public int IdUsuario { get; set; }
        public string Password { get; set; }

    }
    public class RqsLogin
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }
    public class RspLogin
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
    public class RspListarErrores
    {
        public string NroError { get; set; }
        public string MsgError { get; set; }
    }
}
