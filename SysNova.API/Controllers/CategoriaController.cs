using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAll()
        {
            var categorias = await _service.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoriaDTO>> GetById(int id)
        {
            var categoria = await _service.GetByIdAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CategoriaDTO>> Create(CategoriaDTO categoriaDto)
        {
            var nuevaCategoria = await _service.AddAsync(categoriaDto);
            return Ok(nuevaCategoria);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(CategoriaDTO categoriaDto)
        {
            var existente = await _service.GetByIdAsync(categoriaDto.CategoriaId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(categoriaDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
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