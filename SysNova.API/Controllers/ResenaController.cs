using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ResenaDTO>>> GetAll()
        {
            var resenas = await _service.GetAllAsync();
            return Ok(resenas);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResenaDTO>> GetById(int id)
        {
            var resena = await _service.GetByIdAsync(id);

            if (resena == null)
                return NotFound();

            return Ok(resena);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<ResenaDTO>> Create(ResenaDTO resenaDto)
        {
            var nuevaResena = await _service.AddAsync(resenaDto);
            return Ok(nuevaResena);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<IActionResult> Update(ResenaDTO resenaDto)
        {
            var existente = await _service.GetByIdAsync(resenaDto.ResenaId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(resenaDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
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