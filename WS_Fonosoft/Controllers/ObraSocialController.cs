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
        private readonly IRepositorioObraSocial _mysqlRepositorio;

        public ObraSocialController(IResponse<IObraSocial> responseObraSocial, IRepositorioObraSocial mysqlRepositorio)
        {
            _responseObraSocial = responseObraSocial;
            _mysqlRepositorio = mysqlRepositorio;
        }

        // GET: api/<ObraSocialController>
        [HttpGet]
        [ProducesResponseType(typeof(List<rspObraSocialGet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            AEjecutarCU<IObraSocial> buscarObrasSocialesCU = new BuscarObrasSocialesCU<IObraSocial>(_responseObraSocial, _mysqlRepositorio);
            _responseObraSocial = buscarObrasSocialesCU.Ejecutar();

            if (_responseObraSocial.Error.NroError == string.Empty)
            {
                if (_responseObraSocial.Data.Count == 0)
                {
                    return NoContent();
                }

                IList<rspObraSocialGet> lstRspObraSocialGet = new List<rspObraSocialGet>();
                foreach (IObraSocial item in _responseObraSocial.Data)
                {
                    rspObraSocialGet rspObraSocialGet = new rspObraSocialGet();
                    rspObraSocialGet.Id = item.Id;
                    rspObraSocialGet.Nombre = item.Nombre;
                    rspObraSocialGet.Estado = item.Periodo.Estado;

                    lstRspObraSocialGet.Add(rspObraSocialGet);
                }
                return Ok(lstRspObraSocialGet);
            }
            else
            {
                return StatusCode(400, _responseObraSocial.Error);
            }
        }

        // GET api/<ObraSocialController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(rspObraSocialGet), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Get(int id)
        {
            IObraSocial obraSocial = new ObraSocial();
            obraSocial.Id = id;

            AEjecutarCU<IObraSocial> buscarObraSocialXIdCU = new BuscarObraSocialXIdCU<IObraSocial>(_responseObraSocial, _mysqlRepositorio, obraSocial);
            _responseObraSocial = buscarObraSocialXIdCU.Ejecutar();

            if (_responseObraSocial.Error.NroError == string.Empty)
            {
                if (_responseObraSocial.Data.Count == 0)
                {
                    return NoContent();
                }

                IObraSocial item = _responseObraSocial.Data[0];

                rspObraSocialGet rspObraSocialGet = new rspObraSocialGet();
                rspObraSocialGet.Id = item.Id;
                rspObraSocialGet.Nombre = item.Nombre;
                rspObraSocialGet.Estado = item.Periodo.Estado;

                return Ok(rspObraSocialGet);
            }
            else
            {
                return StatusCode(400, _responseObraSocial.Error);
            }
        }

        // POST api/<ObraSocialController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] rspObraSocialPost rspObraSocialPut)
        {
            IObraSocial obraSocial = new ObraSocial();
            obraSocial.Nombre = rspObraSocialPut.Nombre;

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
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Put(rqtObraSocialPut rqtObraSocialPut)
        {
            IObraSocial obraSocial = new ObraSocial();
            obraSocial.Id = rqtObraSocialPut.IdObraSocial;
            obraSocial.Nombre = rqtObraSocialPut.NombreObraSocial;

            AEjecutarCU<IObraSocial> modificarObraSocialCU = new ModificarObraSocialCU<IObraSocial>(_responseObraSocial, _mysqlRepositorio, obraSocial);
            _responseObraSocial = modificarObraSocialCU.Ejecutar();

            if (_responseObraSocial.Error.NroError == string.Empty)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, _responseObraSocial.Error);
            }
        }

        [HttpPut("CambiarEstado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult CambiarEstado(int IdObraSocial)
        {
            IObraSocial obraSocial = new ObraSocial();
            obraSocial.Id = IdObraSocial;

            AEjecutarCU<IObraSocial> cambiarEstadoObraSocialCU = new CambiarEstadoObraSocialCU<IObraSocial>(_responseObraSocial, _mysqlRepositorio, obraSocial);
            _responseObraSocial = cambiarEstadoObraSocialCU.Ejecutar();

            if (_responseObraSocial.Error.NroError == string.Empty)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, _responseObraSocial.Error);
            }
        }
    }

    public class rspObraSocialGet
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }

    public class rspObraSocialPost
    {
        public string Nombre { get; set; }
    }

    public class rqtObraSocialPut
    {
        public int IdObraSocial { get; set; }
        public string NombreObraSocial { get; set; }

    }
}
