using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnvioController : ControllerBase
    {
        private readonly IEnvioService _service;

        public EnvioController(IEnvioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<IEnumerable<EnvioDTO>>> GetAll()
        {
            var envios = await _service.GetAllAsync();
            return Ok(envios);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<EnvioDTO>> GetById(int id)
        {
            var envio = await _service.GetByIdAsync(id);

            if (envio == null)
                return NotFound();

            return Ok(envio);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<EnvioDTO>> Create(
            EnvioDTO envioDto)
        {
            var nuevoEnvio = await _service.AddAsync(envioDto);
            return Ok(nuevoEnvio);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(
            EnvioDTO envioDto)
        {
            var existente = await _service.GetByIdAsync(
                envioDto.EnvioId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(envioDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var envio = await _service.GetByIdAsync(id);

            if (envio == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}