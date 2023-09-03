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
        private readonly IMysqlRepositorio _repoFonoAuth;
        private readonly IAes _aes;

        public UsuarioController(IConfiguration configuration, IResponse<IUsuario> responseUsuario, IMysqlRepositorio repoFonoAuth, IAes aes)
        {
            _configuration = configuration;
            _responseUsuario = responseUsuario;
            _repoFonoAuth = repoFonoAuth;
            _aes = aes;
        }

        [HttpPost]
        [AllowAnonymous]
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
                if (_responseUsuario.Data.Count==0)
                {
                    return NoContent();
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                string key = @"{@BCe-DWtXGWZu7`k7W^&t];<9'vB>r=" + _configuration.GetValue<string>("Jwt:key");
                var tokenKey = Encoding.ASCII.GetBytes(key);
                SecurityTokenDescriptor securityToken = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,usuario.NombreUsuario)
                    }),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(securityToken);
                RspLogin responseLogin = new RspLogin();
                responseLogin.Id = _responseUsuario.Data[0].Id;
                responseLogin.Token = tokenHandler.WriteToken(token);

                return Ok(responseLogin);
            }
            else
            {
                return StatusCode(400, _responseUsuario.Error);
            }
        }
    }
    public class RqsRegistrar
    {
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
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
}
