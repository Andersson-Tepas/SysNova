using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

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
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            var pedidos = await _service.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetById(object id)
        {
            var pedido = await _service.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> Create(Pedido pedido)
        {
            var nuevoPedido = await _service.AddAsync(pedido);
            return Ok(nuevoPedido);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Pedido pedido)
        {
            await _service.UpdateAsync(pedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(object id)
        {
            var pedido = await _service.GetByIdAsync(id);

            if (pedido == null)
                return NotFound();

            await _service.DeleteAsync(pedido);

            return NoContent();
        }
    }
}