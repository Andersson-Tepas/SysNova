using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetAll()
        {
            var pedidos = await _service.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<PedidoDTO>> GetById(int id)
        {
            var pedido = await _service.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<PedidoDTO>> Create(PedidoDTO pedidoDto)
        {
            var nuevoPedido = await _service.AddAsync(pedidoDto);
            return Ok(nuevoPedido);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(PedidoDTO pedidoDto)
        {
            var existente = await _service.GetByIdAsync(pedidoDto.PedidoId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(pedidoDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _service.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}