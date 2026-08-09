using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _service;

        public CarritoController(ICarritoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetAll()
        {
            var carritos = await _service.GetAllAsync();
            return Ok(carritos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrito>> GetById(object id)
        {
            var carrito = await _service.GetByIdAsync(id);

            if (carrito == null)
                return NotFound();

            return Ok(carrito);
        }

        [HttpPost]
        public async Task<ActionResult<Carrito>> Create(Carrito carrito)
        {
            var nuevoCarrito = await _service.AddAsync(carrito);
            return Ok(nuevoCarrito);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Carrito carrito)
        {
            await _service.UpdateAsync(carrito);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(object id)
        {
            var carrito = await _service.GetByIdAsync(id);

            if (carrito == null)
                return NotFound();

            await _service.DeleteAsync(carrito);

            return NoContent();
        }
    }
}