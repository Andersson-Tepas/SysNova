using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResenaController : ControllerBase
    {
        private readonly IResenaService _service;

        public ResenaController(IResenaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resena>>> GetAll()
        {
            var resenas = await _service.GetAllAsync();
            return Ok(resenas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Resena>> GetById(int id)
        {
            var resena = await _service.GetByIdAsync(id);

            if (resena == null)
                return NotFound();

            return Ok(resena);
        }

        [HttpPost]
        public async Task<ActionResult<Resena>> Create(Resena resena)
        {
            var nuevaResena = await _service.AddAsync(resena);
            return Ok(nuevaResena);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Resena resena)
        {
            await _service.UpdateAsync(resena);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resena = await _service.GetByIdAsync(id);

            if (resena == null)
                return NotFound();

            await _service.DeleteAsync(resena);

            return NoContent();
        }
    }
}