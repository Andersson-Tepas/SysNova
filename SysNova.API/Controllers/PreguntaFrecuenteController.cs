using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntaFrecuenteController : ControllerBase
    {
        private readonly IPreguntaFrecuenteService _service;

        public PreguntaFrecuenteController(IPreguntaFrecuenteService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PreguntaFrecuenteDTO>>> GetAll()
        {
            var preguntas = await _service.GetAllAsync();
            return Ok(preguntas);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<PreguntaFrecuenteDTO>> GetById(int id)
        {
            var pregunta = await _service.GetByIdAsync(id);

            if (pregunta == null)
                return NotFound();

            return Ok(pregunta);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PreguntaFrecuenteDTO>> Create(
            PreguntaFrecuenteDTO preguntaDto)
        {
            var nuevaPregunta = await _service.AddAsync(preguntaDto);
            return Ok(nuevaPregunta);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(
            PreguntaFrecuenteDTO preguntaDto)
        {
            var existente = await _service.GetByIdAsync(
                preguntaDto.PreguntaFrecuenteId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(preguntaDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var pregunta = await _service.GetByIdAsync(id);

            if (pregunta == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}