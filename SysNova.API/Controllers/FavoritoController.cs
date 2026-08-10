using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador,Cliente")]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoService _service;

        public FavoritoController(IFavoritoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoritoDTO>>> GetAll()
        {
            var favoritos = await _service.GetAllAsync();
            return Ok(favoritos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FavoritoDTO>> GetById(int id)
        {
            var favorito = await _service.GetByIdAsync(id);

            if (favorito == null)
                return NotFound();

            return Ok(favorito);
        }

        [HttpPost]
        public async Task<ActionResult<FavoritoDTO>> Create(
            FavoritoDTO favoritoDto)
        {
            var nuevoFavorito = await _service.AddAsync(favoritoDto);
            return Ok(nuevoFavorito);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            FavoritoDTO favoritoDto)
        {
            var existente = await _service.GetByIdAsync(
                favoritoDto.FavoritoId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(favoritoDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var favorito = await _service.GetByIdAsync(id);

            if (favorito == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}