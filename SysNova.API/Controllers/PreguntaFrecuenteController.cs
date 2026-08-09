using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntaFrecuenteController : ControllerBase
    {
        private readonly IPreguntaFrecuenteService _service;

        public PreguntaFrecuenteController(
            IPreguntaFrecuenteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreguntaFrecuente>>> GetAll()
        {
            var preguntas = await _service.GetAllAsync();

            return Ok(preguntas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PreguntaFrecuente>> GetById(object id)
        {
            var pregunta = await _service.GetByIdAsync(id);

            if (pregunta == null)
                return NotFound();

            return Ok(pregunta);
        }

        [HttpPost]
        public async Task<ActionResult<PreguntaFrecuente>> Create(
            PreguntaFrecuente pregunta)
        {
            var nuevaPregunta = await _service.AddAsync(pregunta);

            return Ok(nuevaPregunta);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            PreguntaFrecuente pregunta)
        {
            await _service.UpdateAsync(pregunta);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(object id)
        {
            var pregunta = await _service.GetByIdAsync(id);

            if (pregunta == null)
                return NotFound();

            await _service.DeleteAsync(pregunta);

            return NoContent();
        }
    }
}