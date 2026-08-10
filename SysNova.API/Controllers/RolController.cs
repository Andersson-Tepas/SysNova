using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _service;

        public RolController(IRolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDTO>>> GetAll()
        {
            var roles = await _service.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RolDTO>> GetById(int id)
        {
            var rol = await _service.GetByIdAsync(id);

            if (rol == null)
                return NotFound();

            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<RolDTO>> Create(RolDTO rolDto)
        {
            var nuevoRol = await _service.AddAsync(rolDto);
            return Ok(nuevoRol);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RolDTO rolDto)
        {
            await _service.UpdateAsync(rolDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _service.GetByIdAsync(id);

            if (rol == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}