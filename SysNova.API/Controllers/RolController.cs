using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

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
        public async Task<ActionResult<IEnumerable<Rol>>> GetAll()
        {
            var roles = await _service.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Rol>> GetById(int id)
        {
            var rol = await _service.GetByIdAsync(id);

            if (rol == null)
                return NotFound();

            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> Create(Rol rol)
        {
            var nuevoRol = await _service.AddAsync(rol);
            return Ok(nuevoRol);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Rol rol)
        {
            await _service.UpdateAsync(rol);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _service.GetByIdAsync(id);

            if (rol == null)
                return NotFound();

            await _service.DeleteAsync(rol);

            return NoContent();
        }
    }
}