using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAll()
        {
            var categorias = await _service.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> GetById(int id)
        {
            var categoria = await _service.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Create(CategoriaDTO categoriaDto)
        {
            var nuevaCategoria = await _service.AddAsync(categoriaDto);
            return Ok(nuevaCategoria);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoriaDTO categoriaDto)
        {
            await _service.UpdateAsync(categoriaDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _service.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}