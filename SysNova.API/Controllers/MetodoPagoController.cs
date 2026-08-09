using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodoPagoController : ControllerBase
    {
        private readonly IMetodoPagoService _service;

        public MetodoPagoController(IMetodoPagoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodoPago>>> GetAll()
        {
            var metodos = await _service.GetAllAsync();
            return Ok(metodos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MetodoPago>> GetById(object id)
        {
            var metodo = await _service.GetByIdAsync(id);

            if (metodo == null)
                return NotFound();

            return Ok(metodo);
        }

        [HttpPost]
        public async Task<ActionResult<MetodoPago>> Create(
            MetodoPago metodoPago)
        {
            var nuevoMetodo = await _service.AddAsync(metodoPago);
            return Ok(nuevoMetodo);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MetodoPago metodoPago)
        {
            await _service.UpdateAsync(metodoPago);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(object id)
        {
            var metodo = await _service.GetByIdAsync(id);

            if (metodo == null)
                return NotFound();

            await _service.DeleteAsync(metodo);

            return NoContent();
        }
    }
}