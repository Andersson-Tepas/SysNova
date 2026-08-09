using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoService _service;

        public FavoritoController(IFavoritoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorito>>> GetAll()
        {
            var favoritos = await _service.GetAllAsync();
            return Ok(favoritos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Favorito>> GetById(int id)
        {
            var favorito = await _service.GetByIdAsync(id);

            if (favorito == null)
                return NotFound();

            return Ok(favorito);
        }

        [HttpPost]
        public async Task<ActionResult<Favorito>> Create(Favorito favorito)
        {
            var nuevoFavorito = await _service.AddAsync(favorito);
            return Ok(nuevoFavorito);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Favorito favorito)
        {
            await _service.UpdateAsync(favorito);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var favorito = await _service.GetByIdAsync(id);

            if (favorito == null)
                return NotFound();

            await _service.DeleteAsync(favorito);

            return NoContent();
        }
    }
}