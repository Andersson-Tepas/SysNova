using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

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
        public async Task<ActionResult<IEnumerable<DetalleCarrito>>> GetAll()
        {
            var detalles = await _service.GetAllAsync();
            return Ok(detalles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleCarrito>> GetById(object id)
        {
            var detalle = await _service.GetByIdAsync(id);

            if (detalle == null)
                return NotFound();

            return Ok(detalle);
        }

        [HttpPost]
        public async Task<ActionResult<DetalleCarrito>> Create(
            DetalleCarrito detalle)
        {
            var nuevoDetalle = await _service.AddAsync(detalle);
            return Ok(nuevoDetalle);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DetalleCarrito detalle)
        {
            await _service.UpdateAsync(detalle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(object id)
        {
            var detalle = await _service.GetByIdAsync(id);

            if (detalle == null)
                return NotFound();

            await _service.DeleteAsync(detalle);

            return NoContent();
        }
    }
}