using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

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
        public async Task<ActionResult<IEnumerable<ResenaDTO>>> GetAll()
        {
            var resenas = await _service.GetAllAsync();
            return Ok(resenas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResenaDTO>> GetById(int id)
        {
            var resena = await _service.GetByIdAsync(id);

            if (resena == null)
                return NotFound();

            return Ok(resena);
        }

        [HttpPost]
        public async Task<ActionResult<ResenaDTO>> Create(ResenaDTO resenaDto)
        {
            var nuevaResena = await _service.AddAsync(resenaDto);
            return Ok(nuevaResena);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ResenaDTO resenaDto)
        {
            await _service.UpdateAsync(resenaDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resena = await _service.GetByIdAsync(id);

            if (resena == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}