using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MetodoPagoDTO>>> GetAll()
        {
            var metodos = await _service.GetAllAsync();
            return Ok(metodos);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<MetodoPagoDTO>> GetById(int id)
        {
            var metodo = await _service.GetByIdAsync(id);

            if (metodo == null)
                return NotFound();

            return Ok(metodo);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<MetodoPagoDTO>> Create(
            MetodoPagoDTO metodoPagoDto)
        {
            var nuevoMetodo = await _service.AddAsync(metodoPagoDto);
            return Ok(nuevoMetodo);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(
            MetodoPagoDTO metodoPagoDto)
        {
            var existente = await _service.GetByIdAsync(
                metodoPagoDto.MetodoPagoId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(metodoPagoDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var metodo = await _service.GetByIdAsync(id);

            if (metodo == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}