using InscripcionesApi.src.Core;
using InscripcionesApi.src.DTO;
using InscripcionesApi.src.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InscripcionesApi.Controllers
{
    [Route("api/Inscripciones")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {

        private readonly GenericCore<InscripcioneDTO> _InscripcionesCore;
        private readonly ILogger<InscripcionesController> _logger;

        public InscripcionesController(ICoreFactory<InscripcioneDTO> FactoryCore, ILogger<InscripcionesController> logger)
        {
            _logger = logger;
            _InscripcionesCore = FactoryCore.CreateInstance();
        }

        // GET: api/Inscripciones
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _InscripcionesCore.GetAll(true,null,null,null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Inscripciones
        [HttpGet]
		[Route("GetParams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetParams([FromQuery] Dictionary<string, string>? filter, string? orderby, string? dependencias,bool? asc)
        { 
            try
            {
                var result = _InscripcionesCore.GetAll(asc ?? true, filter, orderby, dependencias);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Inscripciones/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _InscripcionesCore.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Inscripciones
        [HttpPost]
        public IActionResult Add([FromBody] InscripcioneDTO Inscripcione)
        {

            //TODO
            /*
            validacion del modelo cuando llega por metodo post
            Modelstate.isvalid
            
            modelstate personalizado
            if validacion:
                modelstate.Addmodelerror("Numero de identificacion repetido","El numero de identificacion ya existe");
                return badrecuest(modelstate)

            */


            try
            {
                var result = _InscripcionesCore.Add(Inscripcione);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Inscripciones/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Inscripcione = _InscripcionesCore.GetById(id);
                if (Inscripcione == null)
                {
                    return NotFound();
                }

                var result = _InscripcionesCore.Delete(Inscripcione);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Inscripciones/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] InscripcioneDTO Inscripcione)
        {
            try
            {
                if (id != Inscripcione.Id)
                {
                    return BadRequest("Id in URL does not match Id in body.");
                }

                var result = _InscripcionesCore.Update(Inscripcione);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}