using InscripcionesApi.src.Core;
using InscripcionesApi.src.DTO;
using InscripcionesApi.src.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InscripcionesApi.Controllers
{
    [Route("api/Estudiantes")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {

        private readonly EstudiantesCore<EstudianteDTO> _EstudiantesCore;
        private readonly ILogger<EstudiantesController> _logger;

        public EstudiantesController(ICoreFactory<EstudianteDTO> EstudiantesCore, ILogger<EstudiantesController> logger)
        {
            _logger = logger;
            _EstudiantesCore = (EstudiantesCore<EstudianteDTO>)EstudiantesCore.CreateInstance();
        }

        // GET: api/Estudiantes
        [HttpGet]
        [Route("GetCompañeros")]
        public IActionResult GetCompañeros(int idEstudiante)
        {
            try
            {
                var result = _EstudiantesCore.GetCompañeros(idEstudiante);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Estudiantes
        [HttpGet]
		[Route("GetParams")]
		public IActionResult GetParams([FromQuery] Dictionary<string, string>? filter, string? orderby, string? dependencias,bool? asc)
        { 
            try
            {
                var result = _EstudiantesCore.GetAll(asc ?? true, filter, orderby, dependencias);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _EstudiantesCore.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Estudiantes
        [HttpPost]
        public IActionResult Add([FromBody] EstudianteDTO Estudiante)
        {

          


            try
            {
                var result = _EstudiantesCore.Add(Estudiante);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Estudiante = _EstudiantesCore.GetById(id);
                if (Estudiante == null)
                {
                    return NotFound();
                }

                var result = _EstudiantesCore.Delete(Estudiante);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Estudiantes/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EstudianteDTO Estudiante)
        {
            try
            {
                if (id != Estudiante.Id)
                {
                    return BadRequest("Id in URL does not match Id in body.");
                }

                var result = _EstudiantesCore.Update(Estudiante);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}