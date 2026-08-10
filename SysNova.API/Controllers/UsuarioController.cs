using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

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
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAll()
        {
            var usuarios = await _service.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await _service.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create(UsuarioDTO usuarioDto)
        {
            var nuevoUsuario = await _service.AddAsync(usuarioDto);
            return Ok(nuevoUsuario);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UsuarioDTO usuarioDto)
        {
            await _service.UpdateAsync(usuarioDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _service.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}