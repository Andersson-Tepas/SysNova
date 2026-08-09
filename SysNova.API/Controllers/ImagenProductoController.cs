using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagenProductoController : ControllerBase
    {
        private readonly IImagenProductoService _service;

        public ImagenProductoController(IImagenProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenProducto>>> GetAll()
        {
            var imagenes = await _service.GetAllAsync();
            return Ok(imagenes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImagenProducto>> GetById(object id)
        {
            var imagen = await _service.GetByIdAsync(id);

            if (imagen == null)
                return NotFound();

            return Ok(imagen);
        }

        [HttpPost]
        public async Task<ActionResult<ImagenProducto>> Create(
            ImagenProducto imagen)
        {
            var nuevaImagen = await _service.AddAsync(imagen);
            return Ok(nuevaImagen);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ImagenProducto imagen)
        {
            await _service.UpdateAsync(imagen);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(object id)
        {
            var imagen = await _service.GetByIdAsync(id);

            if (imagen == null)
                return NotFound();

            await _service.DeleteAsync(imagen);

            return NoContent();
        }
    }
}