using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using CompartidoPE.Modelo;
using Microsoft.AspNetCore.Mvc;
using WS_Fonosoft.Src.TipoDocumento.Aplicacion;
using WS_Fonosoft.Src.TipoDocumento.Dominio.Interface;
using WS_Fonosoft.Src.TipoDocumento.Infraestructura;
using WS_Fonosoft.Src.TipoDocumento.Infraestructura.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WS_Fonosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly IRepositorioTipoDocumento _repositorioTipoDocumento;
        private IResponse<ITipoDocumento> _responseTipoDocumento;

        public TipoDocumentoController(IRepositorioTipoDocumento repositorioTipoDocumento, IResponse<ITipoDocumento> responseTipoDocumento)
        {
            _repositorioTipoDocumento = repositorioTipoDocumento;
            _responseTipoDocumento = responseTipoDocumento;
        }

        // GET: api/<TipoDocumentoController>
        [HttpGet]
        [ProducesResponseType(typeof(List<rspTipoDocumentoGet>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            AEjecutarCU<ITipoDocumento> buscarTiposDocumentosCU = new BuscarTiposDocumentosCU<ITipoDocumento>(_responseTipoDocumento, _repositorioTipoDocumento);
            _responseTipoDocumento = buscarTiposDocumentosCU.Ejecutar();

            if (_responseTipoDocumento.Error.NroError == string.Empty)
            {
                if (_responseTipoDocumento.Data.Count == 0)
                {
                    return NoContent();
                }

                List<rspTipoDocumentoGet> lstRspTipoDocumentoGet = new List<rspTipoDocumentoGet>();
                foreach (ITipoDocumento item in _responseTipoDocumento.Data)
                {
                    rspTipoDocumentoGet rspTipoDocumentoGet = new rspTipoDocumentoGet();
                    rspTipoDocumentoGet.IdTipoDocumento = item.Id;
                    rspTipoDocumentoGet.NombreTipoDocumento = item.Nombre;
                    rspTipoDocumentoGet.FechaInicio = item.Periodo.FechaInicio;
                    rspTipoDocumentoGet.FechaFin = item.Periodo.FechaFin;
                    rspTipoDocumentoGet.Estado = item.Periodo.Estado;

                    lstRspTipoDocumentoGet.Add(rspTipoDocumentoGet);
                }

                return Ok(lstRspTipoDocumentoGet);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, _responseTipoDocumento.Error);
            }
        }

        [HttpGet("GetHabilitados")]
        [ProducesResponseType(typeof(List<rspTipoDocumentoGet>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult GetHabilitados()
        {
            AEjecutarCU<ITipoDocumento> buscarTiposDocumentosHabilitadosCU = new BuscarTiposDocumentosHabilitadosCU<ITipoDocumento>(_responseTipoDocumento, _repositorioTipoDocumento);
            _responseTipoDocumento = buscarTiposDocumentosHabilitadosCU.Ejecutar();

            if (_responseTipoDocumento.Error.NroError == string.Empty)
            {
                if (_responseTipoDocumento.Data.Count == 0)
                {
                    return NoContent();
                }

                List<rspTipoDocumentoGet> lstRspTipoDocumentoGet = new List<rspTipoDocumentoGet>();
                foreach (ITipoDocumento item in _responseTipoDocumento.Data)
                {
                    rspTipoDocumentoGet rspTipoDocumentoGet = new rspTipoDocumentoGet();
                    rspTipoDocumentoGet.IdTipoDocumento = item.Id;
                    rspTipoDocumentoGet.NombreTipoDocumento = item.Nombre;
                    rspTipoDocumentoGet.FechaInicio = item.Periodo.FechaInicio;
                    rspTipoDocumentoGet.FechaFin = item.Periodo.FechaFin;
                    rspTipoDocumentoGet.Estado = item.Periodo.Estado;

                    lstRspTipoDocumentoGet.Add(rspTipoDocumentoGet);
                }

                return Ok(lstRspTipoDocumentoGet);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, _responseTipoDocumento.Error);
            }
        }

        // GET api/<TipoDocumentoController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(rspTipoDocumentoGet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Get([FromServices] ITipoDocumento tipoDocumento, int id)
        {
            tipoDocumento.Id = id;

            AEjecutarCU<ITipoDocumento> buscarTipoDocumentoXIdCU = new BuscarTipoDocumentoXIdCU<ITipoDocumento>(_responseTipoDocumento, _repositorioTipoDocumento, tipoDocumento);
            _responseTipoDocumento = buscarTipoDocumentoXIdCU.Ejecutar();

            if (_responseTipoDocumento.Error.NroError == string.Empty)
            {
                if (_responseTipoDocumento.Data.Count == 0)
                {
                    return NoContent();
                }

                ITipoDocumento tipoDocumentoData = _responseTipoDocumento.Data[0];

                rspTipoDocumentoGet rspTipoDocumentoGet = new rspTipoDocumentoGet();
                rspTipoDocumentoGet.IdTipoDocumento = tipoDocumentoData.Id;
                rspTipoDocumentoGet.NombreTipoDocumento = tipoDocumentoData.Nombre;
                rspTipoDocumentoGet.FechaInicio = tipoDocumentoData.Periodo.FechaInicio;
                rspTipoDocumentoGet.FechaFin = tipoDocumentoData.Periodo.FechaFin;
                rspTipoDocumentoGet.Estado = tipoDocumentoData.Periodo.Estado;

                return Ok(rspTipoDocumentoGet);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, _responseTipoDocumento.Error);
            }
        }

        // POST api/<TipoDocumentoController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromServices] ITipoDocumento tipoDocumento, [FromBody] rspTipoDocumentoPost rspTipoDocumentoPost)
        {
            tipoDocumento.Nombre = rspTipoDocumentoPost.NombreTipoDocumento;

            AEjecutarCU<ITipoDocumento> agregarTipoDocumentoCU = new AgregarTipoDocumentoCU<ITipoDocumento>(_responseTipoDocumento, _repositorioTipoDocumento, tipoDocumento);
            _responseTipoDocumento = agregarTipoDocumentoCU.Ejecutar();

            if (_responseTipoDocumento.Error.NroError == string.Empty)
            {
                if (_responseTipoDocumento.Data.Count == 0)
                {
                    return NoContent();
                }
                return Created("", _responseTipoDocumento.Data[0].Id.ToString());
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, _responseTipoDocumento.Error);
            }
        }

        // PUT api/<TipoDocumentoController>/5
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromServices] ITipoDocumento tipoDocumento, [FromBody] rspTipoDocumentoPut rspTipoDocumentoPut)
        {
            tipoDocumento.Id = rspTipoDocumentoPut.IdTipoDocumento;
            tipoDocumento.Nombre = rspTipoDocumentoPut.NombreTipoDocumento;

            AEjecutarCU<ITipoDocumento> modificarTipoDocumentoCU = new ModificarTipoDocumentoCU<ITipoDocumento>(_responseTipoDocumento, _repositorioTipoDocumento, tipoDocumento);
            _responseTipoDocumento = modificarTipoDocumentoCU.Ejecutar();

            if (_responseTipoDocumento.Error.NroError == string.Empty)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, _responseTipoDocumento.Error);
            }
        }

        [HttpPut("CambiarEstado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult CambiarEstado([FromServices] ITipoDocumento tipoDocumento, int IdTipoDocumento)
        {
            tipoDocumento.Id = IdTipoDocumento;

            AEjecutarCU<ITipoDocumento> cambiarEstadoTipoDocumentoCU = new CambiarEstadoTipoDocumentoCU<ITipoDocumento>(_responseTipoDocumento, _repositorioTipoDocumento, tipoDocumento);
            _responseTipoDocumento = cambiarEstadoTipoDocumentoCU.Ejecutar();

            if (_responseTipoDocumento.Error.NroError == string.Empty)
            {
                return Ok();
            }
            else
            {
                return StatusCode(400, _responseTipoDocumento.Error);
            }
        }
    }

    public class rspTipoDocumentoGet
    {
        public int IdTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
    }
    public class rspTipoDocumentoPost
    {
        public string NombreTipoDocumento { get; set; }
    }
    public class rspTipoDocumentoPut
    {
        public int IdTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; }
    }
}
