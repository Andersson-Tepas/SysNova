using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleCarritoController : ControllerBase
    {
        private readonly IDetalleCarritoService _service;

        public DetalleCarritoController(IDetalleCarritoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCarritoDTO>>> GetAll()
        {
            var detalles = await _service.GetAllAsync();
            return Ok(detalles);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DetalleCarritoDTO>> GetById(int id)
        {
            var detalle = await _service.GetByIdAsync(id);

            if (detalle == null)
                return NotFound();

            return Ok(detalle);
        }

        [HttpPost]
        public async Task<ActionResult<DetalleCarritoDTO>> Create(DetalleCarritoDTO detalleDto)
        {
            var nuevoDetalle = await _service.AddAsync(detalleDto);
            return Ok(nuevoDetalle);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DetalleCarritoDTO detalleDto)
        {
            await _service.UpdateAsync(detalleDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var detalle = await _service.GetByIdAsync(id);

            if (detalle == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}