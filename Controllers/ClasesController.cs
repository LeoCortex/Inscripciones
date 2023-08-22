using InscripcionesApi.src.Core;
using InscripcionesApi.src.DTO;
using InscripcionesApi.src.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ClasesApi.Controllers
{
    [Route("api/Clases")]
    [ApiController]
    public class ClasesController : ControllerBase
    {

        private readonly GenericCore<ClaseDTO> _ClasesCore;
        private readonly ILogger<ClasesController> _logger;

        public ClasesController(ICoreFactory<ClaseDTO> ClasesCore, ILogger<ClasesController> logger)
        {
            _logger = logger;
            _ClasesCore = ClasesCore.CreateInstance();
        }

        // GET: api/Clases
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _ClasesCore.GetAll(true,null,null,null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Clases
        [HttpGet]
		[Route("GetParams")]
		public IActionResult GetParams([FromQuery] Dictionary<string, string>? filter, string? orderby, string? dependencias,bool? asc)
        { 
            try
            {
                var result = _ClasesCore.GetAll(asc ?? true, filter, orderby, dependencias);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Clases/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _ClasesCore.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Clases
        [HttpPost]
        public IActionResult Add([FromBody] ClaseDTO Clase)
        {

            try
            {
                var result = _ClasesCore.Add(Clase);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Clases/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Clase = _ClasesCore.GetById(id);
                if (Clase == null)
                {
                    return NotFound();
                }

                var result = _ClasesCore.Delete(Clase);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Clases/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClaseDTO Clase)
        {
            try
            {
                if (id != Clase.Id)
                {
                    return BadRequest("Id in URL does not match Id in body.");
                }

                var result = _ClasesCore.Update(Clase);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}