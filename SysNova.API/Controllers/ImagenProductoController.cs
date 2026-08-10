using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

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
        public async Task<ActionResult<IEnumerable<ImagenProductoDTO>>> GetAll()
        {
            var imagenes = await _service.GetAllAsync();
            return Ok(imagenes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ImagenProductoDTO>> GetById(int id)
        {
            var imagen = await _service.GetByIdAsync(id);

            if (imagen == null)
                return NotFound();

            return Ok(imagen);
        }

        [HttpPost]
        public async Task<ActionResult<ImagenProductoDTO>> Create(ImagenProductoDTO imagenDto)
        {
            var nuevaImagen = await _service.AddAsync(imagenDto);
            return Ok(nuevaImagen);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ImagenProductoDTO imagenDto)
        {
            await _service.UpdateAsync(imagenDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var imagen = await _service.GetByIdAsync(id);

            if (imagen == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}