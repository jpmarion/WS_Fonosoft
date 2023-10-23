using CompartidoPE.Abstracta;
using CompartidoPE.Interface;
using CompartidoPE.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using WS_Fonosoft.Dto.Paciente;
using WS_Fonosoft.Src.Entidades.Aplicacion;
using WS_Fonosoft.Src.Entidades.Dominio.Agregados;
using WS_Fonosoft.Src.Entidades.Dominio.Entidades;
using WS_Fonosoft.Src.Entidades.Dominio.Factoria;
using WS_Fonosoft.Src.Entidades.Dominio.Interface;
using WS_Fonosoft.Src.Entidades.Infraestructura.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WS_Fonosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private IResponse<IEntidad> _responseEntidad;
        private readonly IRepositorioEntidad _repositorioEntidad;

        public PacienteController(IResponse<IEntidad> responseEntidad, IRepositorioEntidad repositorioEntidad)
        {
            _responseEntidad = responseEntidad;
            _repositorioEntidad = repositorioEntidad;
        }

        // GET: api/<PacienteController>
        [HttpGet]
        [ProducesResponseType(typeof(List<RspPacienteGet>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            AEjecutarCU<IEntidad> buscarPacientesCU = new BuscarPacientesCU<IEntidad>(_responseEntidad, _repositorioEntidad);
            _responseEntidad = buscarPacientesCU.Ejecutar();

            if (_responseEntidad.Error.NroError == string.Empty)
            {
                if (_responseEntidad.Data == null)
                {
                    return NoContent();
                }

                IList<RspPacienteGet> lstRspPacienteGet = new List<RspPacienteGet>();
                foreach (IEntidad item in _responseEntidad.Data)
                {
                    RspPacienteGet rspPacienteGet = new RspPacienteGet();
                    rspPacienteGet.IdPaciente = item.Id;
                    rspPacienteGet.Apellido = item.DatosPersona.Apellido;
                    rspPacienteGet.Nombre = item.DatosPersona.Nombre;
                    rspPacienteGet.TipoDocumento = item.Documentos[0].TipoDocumento;
                    rspPacienteGet.NroDocumento = item.Documentos[0].NroDocumento;

                    lstRspPacienteGet.Add(rspPacienteGet);
                }
                return Ok(lstRspPacienteGet);

            }
            else
            {
                return StatusCode(400, _responseEntidad.Error);
            }
        }

        // GET api/<PacienteController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<RspPacienteGet>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Get(int id)
        {
            IEntidad paciente = EntidadFactory.GetEntidad(TipoEntidad.EnumTipoEntidad.Paciente);
            paciente.Id = id;

            AEjecutarCU<IEntidad> buscarPacienteXIdCU = new BuscarPacienteXIdCU<IEntidad>(_responseEntidad, _repositorioEntidad, paciente);
            _responseEntidad = buscarPacienteXIdCU.Ejecutar();

            if (_responseEntidad.Error.NroError == string.Empty)
            {
                if (_responseEntidad.Data == null)
                {
                    return NoContent();
                }

                paciente = _responseEntidad.Data[0];
                RspPacienteXId rspPacienteXId = new RspPacienteXId();
                rspPacienteXId.IdPaciente = paciente.Id;
                rspPacienteXId.Apellido = paciente.DatosPersona.Apellido;
                rspPacienteXId.Nombre = paciente.DatosPersona.Nombre;

                if (paciente.TiposEntidades != null)
                {
                    foreach (IEntidadTipoEntidad item in paciente.TiposEntidades)
                    {
                        EntidadGet entidadGet = new EntidadGet();
                        entidadGet.IdTipoEntidad = item.IdTipoEntidad;
                        entidadGet.Entidad = item.TipoEntidad;
                        rspPacienteXId.Entidades.Add(entidadGet);
                    }
                }

                if (paciente.Contactos != null)
                {
                    foreach (IEntidadTipoContacto item in paciente.Contactos)
                    {
                        ContactoGet contactoGet = new ContactoGet();
                        contactoGet.IdTipoContacto = item.IdTipoContacto;
                        contactoGet.TipoContacto = item.TipoContacto;
                        contactoGet.Descripcion = item.Contacto;
                        rspPacienteXId.Contactos.Add(contactoGet);
                    }
                }

                if (paciente.ObrasSociales != null)
                {
                    foreach (IEntidadObraSocial item in paciente.ObrasSociales)
                    {
                        ObraSocialGet obraSocialGet = new ObraSocialGet();
                        obraSocialGet.IdObraSocial = item.IdObraSocial;
                        obraSocialGet.ObraSocial = item.ObraSocial;
                        obraSocialGet.NroObraSocial = item.NroObraSocial;
                        rspPacienteXId.ObraSociales.Add(obraSocialGet);
                    }
                }

                if (paciente.Documentos != null)
                {
                    foreach (IEntidadTipoDocumento item in paciente.Documentos)
                    {
                        DocumentoGet documentoGet = new DocumentoGet();
                        documentoGet.IdTipoDocumento = item.IdTipoDocumento;
                        documentoGet.Documento = item.TipoDocumento;
                        documentoGet.NroDocumento = item.NroDocumento;
                        rspPacienteXId.Documentos.Add(documentoGet);
                    }
                }

                return Ok(rspPacienteXId);

            }
            else
            {
                return StatusCode(400, _responseEntidad.Error);
            }
        }

        // POST api/<PacienteController>
        [HttpPost]
        [ProducesResponseType(typeof(RqsPacientePost), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] RqsPacientePost paciente)
        {
            IEntidad pacienteEntidad = EntidadFactory.GetEntidad(TipoEntidad.EnumTipoEntidad.Paciente);
            pacienteEntidad.DatosPersona.Apellido = paciente.Apellido;
            pacienteEntidad.DatosPersona.Nombre = paciente.Nombre;
            foreach (Documento itemDocumento in paciente.Documentos)
            {
                IEntidadTipoDocumento entidadTipoDocumento = new EntidadTipoDocumento();
                entidadTipoDocumento.IdTipoDocumento = itemDocumento.IdTipoDocumento;
                entidadTipoDocumento.NroDocumento = itemDocumento.NroDocumento;
                pacienteEntidad.Documentos.Add(entidadTipoDocumento);
            }
            foreach (ObraSocial item in paciente.ObrasSociales)
            {
                IEntidadObraSocial entidadObraSocial = new EntidadObraSocial();
                entidadObraSocial.IdObraSocial = item.IdObraSocial;
                entidadObraSocial.NroObraSocial = item.NroObraSocial;
                pacienteEntidad.ObrasSociales.Add(entidadObraSocial);
            }
            foreach (Contacto item in paciente.Contactos)
            {
                IEntidadTipoContacto entidadTipoContacto = new EntidadTipoContacto();
                entidadTipoContacto.IdTipoContacto = item.IdTipoContacto;
                entidadTipoContacto.Contacto = item.Descripcion;
                pacienteEntidad.Contactos.Add(entidadTipoContacto);
            }

            AEjecutarCU<IEntidad> agregarPacienteCU = new AgregarPacienteCU<IEntidad>(_responseEntidad, _repositorioEntidad, pacienteEntidad);
            _responseEntidad = agregarPacienteCU.Ejecutar();

            if (_responseEntidad.Error.NroError == string.Empty)
            {
                if (_responseEntidad.Data.Count == 0)
                {
                    return NoContent();
                }
                RspPacientePost rspPacientePost = new RspPacientePost();
                rspPacientePost.IdEntidad = _responseEntidad.Data[0].Id;
                return Created("", rspPacientePost);
            }
            else
            {
                return StatusCode(400, _responseEntidad.Error);
            }
        }

        // PUT api/<PacienteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PacienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
