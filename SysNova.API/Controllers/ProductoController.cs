using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAll()
        {
            var productos = await _service.GetAllAsync();
            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var producto = await _service.GetByIdAsync(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<ProductoDTO>> Create(ProductoDTO productoDto)
        {
            var nuevoProducto = await _service.AddAsync(productoDto);
            return Ok(nuevoProducto);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(ProductoDTO productoDto)
        {
            var existente = await _service.GetByIdAsync(productoDto.ProductoId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(productoDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _service.GetByIdAsync(id);

            if (producto == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}