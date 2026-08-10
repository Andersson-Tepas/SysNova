using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

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
        public async Task<ActionResult<IEnumerable<CarritoDTO>>> GetAll()
        {
            var carritos = await _service.GetAllAsync();
            return Ok(carritos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarritoDTO>> GetById(int id)
        {
            var carrito = await _service.GetByIdAsync(id);

            if (carrito == null)
                return NotFound();

            return Ok(carrito);
        }

        [HttpPost]
        public async Task<ActionResult<CarritoDTO>> Create(CarritoDTO carritoDto)
        {
            var nuevoCarrito = await _service.AddAsync(carritoDto);
            return Ok(nuevoCarrito);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CarritoDTO carritoDto)
        {
            await _service.UpdateAsync(carritoDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carrito = await _service.GetByIdAsync(id);

            if (carrito == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}