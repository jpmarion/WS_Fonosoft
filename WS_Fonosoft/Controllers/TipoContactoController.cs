using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using CompartidoPE.Modelo;
using Microsoft.AspNetCore.Mvc;
using WS_Fonosoft.Dto.TipoContacto;
using WS_Fonosoft.Src.TipoContacto.Aplicacion;
using WS_Fonosoft.Src.TipoContacto.Dominio.Interface;
using WS_Fonosoft.Src.TipoContacto.Infraestructura.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WS_Fonosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoContactoController : ControllerBase
    {
        private IResponse<ITipoContacto> _responseTipoContacto;
        private readonly IRepositorioTipoContacto _repositorioTipoContacto;

        public TipoContactoController(IResponse<ITipoContacto> responseTipoContacto, IRepositorioTipoContacto repositorioTipoContacto)
        {
            _responseTipoContacto = responseTipoContacto;
            _repositorioTipoContacto = repositorioTipoContacto;
        }

        // GET: api/<TipoContactoController>
        [HttpGet]
        [ProducesResponseType(typeof(List<RspTipoContactoGet>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            AEjecutarCU<ITipoContacto> buscarTiposContactosCU = new BuscarTiposContactosCU<ITipoContacto>(_responseTipoContacto, _repositorioTipoContacto);
            _responseTipoContacto = buscarTiposContactosCU.Ejecutar();

            if (_responseTipoContacto.Error.NroError == string.Empty)
            {
                if (_responseTipoContacto.Data.Count == 0)
                {
                    return NoContent();
                }

                IList<RspTipoContactoGet> lstRspTipoContacto = new List<RspTipoContactoGet>();
                foreach (ITipoContacto item in _responseTipoContacto.Data)
                {
                    RspTipoContactoGet rspTipoContactoGet = new RspTipoContactoGet();
                    rspTipoContactoGet.IdTipoContacto = item.Id;
                    rspTipoContactoGet.TipoContacto = item.Nombre;

                    lstRspTipoContacto.Add(rspTipoContactoGet);
                }
                return Ok(lstRspTipoContacto);
            }
            else
            {
                return StatusCode(400, _responseTipoContacto.Error);
            }
        }

    }
}
