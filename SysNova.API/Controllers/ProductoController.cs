using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

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

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetAll()
        {
            var productos = await _service.GetAllAsync();

            return Ok(productos);
        }

        // GET: api/Producto/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var producto = await _service.GetByIdAsync(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<Producto>> Create(Producto producto)
        {
            var nuevoProducto = await _service.AddAsync(producto);

            return Ok(nuevoProducto);
        }

        // PUT: api/Producto
        [HttpPut]
        public async Task<IActionResult> Update(Producto producto)
        {
            var existente = await _service.GetByIdAsync(producto.ProductoId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(producto);

            return NoContent();
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _service.GetByIdAsync(id);

            if (producto == null)
                return NotFound();

            await _service.DeleteAsync(producto);

            return NoContent();
        }
    }
}