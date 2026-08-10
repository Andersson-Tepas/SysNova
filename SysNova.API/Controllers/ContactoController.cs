using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

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
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<IEnumerable<ContactoDTO>>> GetAll()
        {
            var contactos = await _service.GetAllAsync();
            return Ok(contactos);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<ContactoDTO>> GetById(int id)
        {
            var contacto = await _service.GetByIdAsync(id);

            if (contacto == null)
                return NotFound();

            return Ok(contacto);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ContactoDTO>> Create(ContactoDTO contactoDto)
        {
            var nuevoContacto = await _service.AddAsync(contactoDto);
            return Ok(nuevoContacto);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(ContactoDTO contactoDto)
        {
            var existente = await _service.GetByIdAsync(contactoDto.ContactoId);

            if (existente == null)
                return NotFound();

            await _service.UpdateAsync(contactoDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var contacto = await _service.GetByIdAsync(id);

            if (contacto == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}