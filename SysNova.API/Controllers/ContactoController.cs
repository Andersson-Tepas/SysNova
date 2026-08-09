using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.EN.Entities;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoService _service;

        public ContactoController(IContactoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetAll()
        {
            var contactos = await _service.GetAllAsync();

            return Ok(contactos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Contacto>> GetById(int id)
        {
            var contacto = await _service.GetByIdAsync(id);

            if (contacto == null)
                return NotFound();

            return Ok(contacto);
        }

        [HttpPost]
        public async Task<ActionResult<Contacto>> Create(
            Contacto contacto)
        {
            var nuevoContacto = await _service.AddAsync(contacto);

            return Ok(nuevoContacto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Contacto contacto)
        {
            await _service.UpdateAsync(contacto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contacto = await _service.GetByIdAsync(id);

            if (contacto == null)
                return NotFound();

            await _service.DeleteAsync(contacto);

            return NoContent();
        }
    }
}