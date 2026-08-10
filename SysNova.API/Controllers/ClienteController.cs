using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;

namespace SysNova.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
        {
            var clientes = await _service.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> GetById(int id)
        {
            var cliente = await _service.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Create(ClienteDTO clienteDto)
        {
            var nuevoCliente = await _service.AddAsync(clienteDto);
            return Ok(nuevoCliente);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClienteDTO clienteDto)
        {
            await _service.UpdateAsync(clienteDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _service.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}