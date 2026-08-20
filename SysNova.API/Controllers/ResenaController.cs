using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysNova.BL.Interfaces;
using SysNova.DTO;
using System.Security.Claims;

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


        // =========================================================
        // OBTENER TODAS
        // =========================================================
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ResenaDTO>>> GetAll()
        {
            var resenas = await _service.GetAllAsync();

            return Ok(resenas);
        }


        // =========================================================
        // OBTENER POR ID
        // =========================================================
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResenaDTO>> GetById(int id)
        {
            var resena = await _service.GetByIdAsync(id);

            if (resena == null)
                return NotFound();

            return Ok(resena);
        }


        // =========================================================
        // CREAR RESEÑA
        // ADMIN O CLIENTE
        // =========================================================
        [HttpPost]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<ResenaDTO>> Create(
            ResenaDTO resenaDto)
        {
            // Si es cliente, NO confiamos en el ClienteId
            // que venga desde el frontend.
            if (User.IsInRole("Cliente"))
            {
                var clienteId = ObtenerClienteIdAutenticado();

                if (clienteId <= 0)
                    return Unauthorized();

                resenaDto.ClienteId = clienteId;
            }

            resenaDto.ResenaId = 0;

            resenaDto.Fecha = DateTime.Now;
            resenaDto.FechaCreacion = DateTime.Now;
            resenaDto.Activo = true;

            var nuevaResena =
                await _service.AddAsync(resenaDto);

            return Ok(nuevaResena);
        }


        // =========================================================
        // EDITAR RESEÑA
        // CLIENTE: SOLO LA SUYA
        // ADMIN: CUALQUIERA
        // =========================================================
        [HttpPut]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<IActionResult> Update(
            ResenaDTO resenaDto)
        {
            var existente =
                await _service.GetByIdAsync(
                    resenaDto.ResenaId);

            if (existente == null)
                return NotFound();


            // =====================================================
            // CLIENTE
            // =====================================================
            if (User.IsInRole("Cliente"))
            {
                var clienteId =
                    ObtenerClienteIdAutenticado();

                if (clienteId <= 0)
                    return Unauthorized();


                // Esta reseña NO pertenece al cliente autenticado.
                if (existente.ClienteId != clienteId)
                    return Forbid();


                // Evitamos que el cliente manipule estos datos.
                resenaDto.ClienteId =
                    existente.ClienteId;

                resenaDto.ProductoId =
                    existente.ProductoId;

                resenaDto.Activo =
                    existente.Activo;

                resenaDto.Fecha =
                    existente.Fecha;

                resenaDto.FechaCreacion =
                    existente.FechaCreacion;
            }


            resenaDto.FechaModificacion =
                DateTime.Now;


            await _service.UpdateAsync(
                resenaDto);

            return NoContent();
        }


        // =========================================================
        // ELIMINAR RESEÑA
        // CLIENTE: SOLO LA SUYA
        // ADMIN: CUALQUIERA
        // =========================================================
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<IActionResult> Delete(int id)
        {
            var resena =
                await _service.GetByIdAsync(id);

            if (resena == null)
                return NotFound();


            // =====================================================
            // CLIENTE
            // =====================================================
            if (User.IsInRole("Cliente"))
            {
                var clienteId =
                    ObtenerClienteIdAutenticado();

                if (clienteId <= 0)
                    return Unauthorized();


                // No puede borrar una reseña ajena.
                if (resena.ClienteId != clienteId)
                    return Forbid();
            }


            await _service.DeleteAsync(id);

            return NoContent();
        }


        // =========================================================
        // ID DEL CLIENTE DESDE JWT
        // =========================================================
        private int ObtenerClienteIdAutenticado()
        {
            var valor =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(valor))
                return 0;

            return int.TryParse(
                valor,
                out var clienteId)
                    ? clienteId
                    : 0;
        }
    }
}