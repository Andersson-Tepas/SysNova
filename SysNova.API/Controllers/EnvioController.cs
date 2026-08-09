using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

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
        public async Task<ActionResult<IEnumerable<Envio>>> GetAll()
        {
            var envios = await _service.GetAllAsync();
            return Ok(envios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Envio>> GetById(int id)
        {
            var envio = await _service.GetByIdAsync(id);

            if (envio == null)
                return NotFound();

            return Ok(envio);
        }

        [HttpPost]
        public async Task<ActionResult<Envio>> Create(Envio envio)
        {
            var nuevoEnvio = await _service.AddAsync(envio);
            return Ok(nuevoEnvio);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Envio envio)
        {
            await _service.UpdateAsync(envio);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var envio = await _service.GetByIdAsync(id);

            if (envio == null)
                return NotFound();

            await _service.DeleteAsync(envio);

            return NoContent();
        }
    }
}