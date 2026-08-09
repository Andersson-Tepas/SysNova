using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

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
        public async Task<ActionResult<IEnumerable<Marca>>> GetAll()
        {
            var marcas = await _service.GetAllAsync();
            return Ok(marcas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetById(object id)
        {
            var marca = await _service.GetByIdAsync(id);

            if (marca == null)
                return NotFound();

            return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult<Marca>> Create(Marca marca)
        {
            var nuevaMarca = await _service.AddAsync(marca);
            return Ok(nuevaMarca);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Marca marca)
        {
            await _service.UpdateAsync(marca);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(object id)
        {
            var marca = await _service.GetByIdAsync(id);

            if (marca == null)
                return NotFound();

            await _service.DeleteAsync(marca);

            return NoContent();
        }
    }
}