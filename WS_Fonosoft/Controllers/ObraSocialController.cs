using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using CompartidoPE.Modelo;
using Microsoft.AspNetCore.Mvc;
using WS_Fonosoft.Src.ObraSocial.Aplicacion;
using WS_Fonosoft.Src.ObraSocial.Dominio.Entidades;
using WS_Fonosoft.Src.ObraSocial.Dominio.Interface;
using WS_Fonosoft.Src.ObraSocial.Infraestructura.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WS_Fonosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraSocialController : ControllerBase
    {
        private IResponse<IObraSocial> _responseObraSocial;
        private readonly IMysqlRepositorioObraSocial _mysqlRepositorio;

        public ObraSocialController(IResponse<IObraSocial> responseObraSocial, IMysqlRepositorioObraSocial mysqlRepositorio)
        {
            _responseObraSocial = responseObraSocial;
            _mysqlRepositorio = mysqlRepositorio;
        }

        // GET: api/<ObraSocialController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ObraSocialController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ObraSocialController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] rspObraSocialPost rspObraSocialPut)
        {
            IObraSocial obraSocial = new ObraSocial();
            obraSocial.Nombre = rspObraSocialPut.Nombre;
            obraSocial.Periodo.FechaInicio = rspObraSocialPut.FechaInicio;
            obraSocial.Periodo.FechaFin = rspObraSocialPut.FechaFin;

            AEjecutarCU<IObraSocial> agregarObraSocialCU = new AgregarObraSocialCU<IObraSocial>(_responseObraSocial, _mysqlRepositorio, obraSocial);
            _responseObraSocial = agregarObraSocialCU.Ejecutar();

            if (_responseObraSocial.Error.NroError == string.Empty)
            {
                if (_responseObraSocial.Data.Count == 0)
                {
                    return NoContent();
                }
                return Created("", _responseObraSocial.Data[0].Id.ToString());
            }
            else
            {
                return StatusCode(400, _responseObraSocial.Error);
            }
        }

        // PUT api/<ObraSocialController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ObraSocialController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class rspObraSocialPost
    {
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
