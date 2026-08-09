using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _service.GetAllAsync();
            return Ok(usuarios);
        }

        // 1. Cambiado 'object id' por 'int id' y restringido en la ruta '{id:int}'
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _service.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario usuario)
        {
            var nuevoUsuario = await _service.AddAsync(usuario);
            return Ok(nuevoUsuario);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Usuario usuario)
        {
            await _service.UpdateAsync(usuario);
            return NoContent();
        }

        // 2. Cambiado 'object id' por 'int id' y restringido en la ruta '{id:int}'
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _service.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            await _service.DeleteAsync(usuario);

            return NoContent();
        }
    }
}