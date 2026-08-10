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
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetAll()
        {
            var pedidos = await _service.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoDTO>> GetById(int id)
        {
            var pedido = await _service.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> Create(PedidoDTO pedidoDto)
        {
            var nuevoPedido = await _service.AddAsync(pedidoDto);
            return Ok(nuevoPedido);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PedidoDTO pedidoDto)
        {
            await _service.UpdateAsync(pedidoDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
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