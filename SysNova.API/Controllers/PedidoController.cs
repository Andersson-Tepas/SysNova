using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SysNova.BL.Interfaces;
using SysNova.DTO;

using System.Security.Claims;


namespace SysNova.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;


        // ==========================================
        // CONSTRUCTOR
        // ==========================================

        public PedidoController(
            IPedidoService service)
        {
            _service = service;
        }


        // ==========================================
        // GET ALL - ADMIN
        // ==========================================

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>>
            GetAll()
        {
            try
            {
                var pedidos =
                    await _service.GetAllAsync();


                return Ok(
                    pedidos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error obteniendo pedidos: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al obtener los pedidos."
                    });
            }
        }


        // ==========================================
        // GET BY ID - ADMIN
        // ==========================================

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PedidoDTO>>
            GetById(
                int id)
        {
            try
            {
                var pedido =
                    await _service.GetByIdAsync(
                        id);


                if (pedido == null)
                {
                    return NotFound(
                        new
                        {
                            mensaje =
                                "El pedido no existe."
                        });
                }


                return Ok(
                    pedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error obteniendo pedido {id}: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al obtener el pedido."
                    });
            }
        }


        // ==========================================
        // MIS PEDIDOS - CLIENTE
        // ==========================================

        [HttpGet("mis-pedidos")]
        [Authorize(Roles = "Cliente")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>>
            GetMisPedidos()
        {
            var clienteIdClaim =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);


            if (!int.TryParse(
                    clienteIdClaim,
                    out var clienteId))
            {
                return Unauthorized(
                    new
                    {
                        mensaje =
                            "No se pudo identificar al cliente autenticado."
                    });
            }


            try
            {
                var pedidos =
                    await _service.GetMisPedidosAsync(
                        clienteId);


                return Ok(
                    pedidos);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error obteniendo pedidos del cliente: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al obtener tus pedidos."
                    });
            }
        }


        // ==========================================
        // DETALLE DE MI PEDIDO - CLIENTE
        // ==========================================

        [HttpGet("mis-pedidos/{numeroPedido}")]
        [Authorize(Roles = "Cliente")]
        public async Task<ActionResult<PedidoDetalleClienteDTO>>
            GetDetalleMiPedido(
                string numeroPedido)
        {
            var clienteIdClaim =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);


            if (!int.TryParse(
                    clienteIdClaim,
                    out var clienteId))
            {
                return Unauthorized(
                    new
                    {
                        mensaje =
                            "No se pudo identificar al cliente autenticado."
                    });
            }


            try
            {
                var pedido =
                    await _service
                        .GetDetallePedidoClienteAsync(
                            clienteId,
                            numeroPedido);


                if (pedido == null)
                {
                    return NotFound(
                        new
                        {
                            mensaje =
                                "El pedido no existe o no pertenece al cliente autenticado."
                        });
                }


                return Ok(
                    pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error obteniendo detalle del pedido del cliente: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al obtener el detalle del pedido."
                    });
            }
        }


        // ==========================================
        // DETALLE DE PEDIDO - ADMIN
        // ==========================================

        [HttpGet("admin/{numeroPedido}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PedidoDetalleClienteDTO>>
            GetDetallePedidoAdmin(
                string numeroPedido)
        {
            try
            {
                var pedido =
                    await _service
                        .GetDetallePedidoAdminAsync(
                            numeroPedido);


                if (pedido == null)
                {
                    return NotFound(
                        new
                        {
                            mensaje =
                                "El pedido no existe."
                        });
                }


                return Ok(
                    pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error obteniendo detalle del pedido para administrador: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al obtener el detalle del pedido."
                    });
            }
        }


        // ==========================================
        // CREATE NORMAL - ADMIN
        // ==========================================

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PedidoDTO>>
            Create(
                PedidoDTO pedidoDto)
        {
            try
            {
                var pedido =
                    await _service.AddAsync(
                        pedidoDto);


                return CreatedAtAction(
                    nameof(GetById),
                    new
                    {
                        id =
                            pedido.PedidoId
                    },
                    pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error creando pedido: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al crear el pedido."
                    });
            }
        }


        // ==========================================
        // CHECKOUT - CLIENTE
        // ==========================================

        [HttpPost("checkout")]
        [Authorize(Roles = "Cliente")]
        public async Task<ActionResult<CheckoutPedidoResponseDTO>>
            Checkout(
                CheckoutPedidoDTO checkout)
        {
            var clienteIdClaim =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);


            if (!int.TryParse(
                    clienteIdClaim,
                    out var clienteId))
            {
                return Unauthorized(
                    new
                    {
                        mensaje =
                            "No se pudo identificar al cliente autenticado."
                    });
            }


            try
            {
                var resultado =
                    await _service.CrearCheckoutAsync(
                        clienteId,
                        checkout);


                return Ok(
                    resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error procesando checkout: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al procesar el pedido."
                    });
            }
        }


        // ==========================================
        // ACTUALIZAR ESTADO - ADMIN
        // ==========================================

        [HttpPut("{id:int}/estado")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult>
            ActualizarEstado(
                int id,
                [FromBody] ActualizarEstadoPedidoDTO dto)
        {
            try
            {
                await _service.ActualizarEstadoAsync(
                    id,
                    dto.Estado);


                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error actualizando estado del pedido {id}: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al actualizar el estado del pedido."
                    });
            }
        }


        // ==========================================
        // UPDATE - ADMIN
        // ==========================================

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult>
            Update(
                PedidoDTO pedidoDto)
        {
            try
            {
                if (pedidoDto.PedidoId <= 0)
                {
                    return BadRequest(
                        new
                        {
                            mensaje =
                                "El pedido no es válido."
                        });
                }


                var existe =
                    await _service.GetByIdAsync(
                        pedidoDto.PedidoId);


                if (existe == null)
                {
                    return NotFound(
                        new
                        {
                            mensaje =
                                "El pedido no existe."
                        });
                }


                await _service.UpdateAsync(
                    pedidoDto);


                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                    new
                    {
                        mensaje =
                            ex.Message
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error actualizando pedido: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al actualizar el pedido."
                    });
            }
        }


        // ==========================================
        // DELETE - ADMIN
        // ==========================================

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult>
            Delete(
                int id)
        {
            try
            {
                var pedido =
                    await _service.GetByIdAsync(
                        id);


                if (pedido == null)
                {
                    return NotFound(
                        new
                        {
                            mensaje =
                                "El pedido no existe."
                        });
                }


                await _service.DeleteAsync(
                    id);


                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error eliminando pedido {id}: {ex.Message}");


                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        mensaje =
                            "Ocurrió un error al eliminar el pedido."
                    });
            }
        }
    }
}