using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _service;

        public MarcaController(IMarcaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaDTO>>> GetAll()
        {
            var marcas = await _service.GetAllAsync();
            return Ok(marcas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MarcaDTO>> GetById(int id)
        {
            var marca = await _service.GetByIdAsync(id);

            if (marca == null)
                return NotFound();

            return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult<MarcaDTO>> Create(MarcaDTO marcaDto)
        {
            var nuevaMarca = await _service.AddAsync(marcaDto);
            return Ok(nuevaMarca);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MarcaDTO marcaDto)
        {
            await _service.UpdateAsync(marcaDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _service.GetByIdAsync(id);

            if (marca == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}