using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallePedidoService _service;

        public DetallePedidoController(IDetallePedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<IEnumerable<DetallePedidoDTO>>> GetAll()
        {
            var detalles = await _service.GetAllAsync();
            return Ok(detalles);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<DetallePedidoDTO>> GetById(int id)
        {
            var detalle = await _service.GetByIdAsync(id);

            if (detalle == null)
                return NotFound();

            return Ok(detalle);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<DetallePedidoDTO>> Create(
            DetallePedidoDTO detalleDto)
        {
            var nuevoDetalle = await _service.AddAsync(detalleDto);
            return Ok(nuevoDetalle);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(
            DetallePedidoDTO detalleDto)
        {
            var existente = await _service.GetByIdAsync(
                detalleDto.DetallePedidoId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(detalleDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
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