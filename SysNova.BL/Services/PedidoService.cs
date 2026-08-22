using System.Data;
using System.Linq.Expressions;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using SysNova.BL.Interfaces;
using SysNova.DAL.Context;
using SysNova.DTO;
using SysNova.EN.Entities;
using SysNova.EN.Enums;
using SysNova.Repository.Interfaces;

namespace SysNova.BL.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IMapper _mapper;
        private readonly SysNovaDbContext _context;


        // ==========================================
        // CONSTRUCTOR
        // ==========================================

        public PedidoService(
            IPedidoRepository repository,
            IMapper mapper,
            SysNovaDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }


        // ==========================================
        // GET ALL
        // ==========================================

        public async Task<IEnumerable<PedidoDTO>>
            GetAllAsync()
        {
            var pedidos =
                await _repository.GetAllAsync();


            return _mapper.Map<IEnumerable<PedidoDTO>>(
                pedidos);
        }


        // ==========================================
        // GET BY ID
        // ==========================================

        public async Task<PedidoDTO?>
            GetByIdAsync(
                int id)
        {
            var pedido =
                await _repository.GetByIdAsync(
                    id);


            return _mapper.Map<PedidoDTO?>(
                pedido);
        }


        // ==========================================
        // FIND
        // ==========================================

        public async Task<IEnumerable<PedidoDTO>>
            FindAsync(
                Expression<Func<PedidoDTO, bool>> predicate)
        {
            var entityPredicate =
                _mapper.Map<
                    Expression<Func<Pedido, bool>>>(
                    predicate);


            var pedidos =
                await _repository.FindAsync(
                    entityPredicate);


            return _mapper.Map<IEnumerable<PedidoDTO>>(
                pedidos);
        }


        // ==========================================
        // ADD
        // ==========================================

        public async Task<PedidoDTO>
            AddAsync(
                PedidoDTO pedidoDto)
        {
            var entity =
                _mapper.Map<Pedido>(
                    pedidoDto);


            var result =
                await _repository.AddAsync(
                    entity);


            return _mapper.Map<PedidoDTO>(
                result);
        }


        // ==========================================
        // UPDATE GENERAL
        // ==========================================

        public async Task UpdateAsync(
            PedidoDTO pedidoDto)
        {
            var entity =
                _mapper.Map<Pedido>(
                    pedidoDto);


            await _repository.UpdateAsync(
                entity);
        }


        // ==========================================
        // DELETE
        // ==========================================

        public async Task DeleteAsync(
            int id)
        {
            var entity =
                await _repository.GetByIdAsync(
                    id);


            if (entity == null)
            {
                return;
            }


            await _repository.DeleteAsync(
                entity);
        }


        // ==========================================
        // EXISTS
        // ==========================================

        public async Task<bool>
            ExistsAsync(
                Expression<Func<PedidoDTO, bool>> predicate)
        {
            var entityPredicate =
                _mapper.Map<
                    Expression<Func<Pedido, bool>>>(
                    predicate);


            return await _repository.ExistsAsync(
                entityPredicate);
        }


        // ==========================================
        // MIS PEDIDOS - CLIENTE
        // ==========================================

        public async Task<IEnumerable<PedidoDTO>>
            GetMisPedidosAsync(
                int clienteId)
        {
            if (clienteId <= 0)
            {
                throw new InvalidOperationException(
                    "El cliente no es válido.");
            }


            var pedidos =
                await _context.Pedidos
                    .AsNoTracking()
                    .Where(
                        p =>
                            p.ClienteId == clienteId
                            &&
                            p.Activo)
                    .OrderByDescending(
                        p =>
                            p.FechaPedido)
                    .ToListAsync();


            return _mapper.Map<IEnumerable<PedidoDTO>>(
                pedidos);
        }


        // ==========================================
        // DETALLE PEDIDO - CLIENTE
        // ==========================================

        public async Task<PedidoDetalleClienteDTO?>
            GetDetallePedidoClienteAsync(
                int clienteId,
                string numeroPedido)
        {
            if (clienteId <= 0)
            {
                throw new InvalidOperationException(
                    "El cliente no es válido.");
            }


            var numeroNormalizado =
                numeroPedido?
                    .Trim()
                    .ToUpperInvariant()
                ?? string.Empty;


            if (string.IsNullOrWhiteSpace(
                    numeroNormalizado))
            {
                throw new InvalidOperationException(
                    "El número de pedido no es válido.");
            }


            var pedido =
                await _context.Pedidos
                    .AsNoTracking()

                    .Include(
                        p =>
                            p.MetodoPago)

                    .Include(
                        p =>
                            p.Detalles)
                        .ThenInclude(
                            d =>
                                d.Producto)

                    .FirstOrDefaultAsync(
                        p =>
                            p.ClienteId == clienteId
                            &&
                            p.NumeroPedido ==
                                numeroNormalizado
                            &&
                            p.Activo);


            if (pedido == null)
            {
                return null;
            }


            return CrearDetalleDTO(
                pedido);
        }


        // ==========================================
        // DETALLE PEDIDO - ADMIN
        // ==========================================

        public async Task<PedidoDetalleClienteDTO?>
            GetDetallePedidoAdminAsync(
                string numeroPedido)
        {
            var numeroNormalizado =
                numeroPedido?
                    .Trim()
                    .ToUpperInvariant()
                ?? string.Empty;


            if (string.IsNullOrWhiteSpace(
                    numeroNormalizado))
            {
                throw new InvalidOperationException(
                    "El número de pedido no es válido.");
            }


            var pedido =
                await _context.Pedidos
                    .AsNoTracking()

                    .Include(
                        p =>
                            p.MetodoPago)

                    .Include(
                        p =>
                            p.Detalles)
                        .ThenInclude(
                            d =>
                                d.Producto)

                    .FirstOrDefaultAsync(
                        p =>
                            p.NumeroPedido ==
                                numeroNormalizado
                            &&
                            p.Activo);


            if (pedido == null)
            {
                return null;
            }


            return CrearDetalleDTO(
                pedido);
        }


        // ==========================================
        // ACTUALIZAR ESTADO - ADMIN
        // ==========================================
        //
        // IMPORTANTE:
        //
        // Aquí NO usamos AutoMapper.
        // Aquí NO mandamos todo el PedidoDTO.
        //
        // EF carga la entidad real y solamente
        // modificamos:
        //
        // Estado
        // FechaModificacion
        //
        // ==========================================

        public async Task ActualizarEstadoAsync(
            int pedidoId,
            EstadoPedido estado)
        {
            // ======================================
            // VALIDAR ID
            // ======================================

            if (pedidoId <= 0)
            {
                throw new InvalidOperationException(
                    "El pedido no es válido.");
            }


            // ======================================
            // VALIDAR ENUM
            // ======================================

            if (!Enum.IsDefined(
                    typeof(EstadoPedido),
                    estado))
            {
                throw new InvalidOperationException(
                    "El estado del pedido no es válido.");
            }


            // ======================================
            // BUSCAR ENTIDAD REAL
            // ======================================

            var pedido =
                await _context.Pedidos
                    .FirstOrDefaultAsync(
                        p =>
                            p.PedidoId == pedidoId
                            &&
                            p.Activo);


            if (pedido == null)
            {
                throw new KeyNotFoundException(
                    "El pedido no existe.");
            }


            // ======================================
            // MODIFICAR SOLO ESTADO
            // ======================================

            pedido.Estado =
                estado;


            pedido.FechaModificacion =
                DateTime.Now;


            // ======================================
            // GUARDAR
            // ======================================

            await _context.SaveChangesAsync();
        }


        // ==========================================
        // ARMAR DTO DETALLE
        // ==========================================

        private static PedidoDetalleClienteDTO
            CrearDetalleDTO(
                Pedido pedido)
        {
            var productos =
                pedido.Detalles?
                    .Where(
                        d =>
                            d.Activo)
                    .OrderBy(
                        d =>
                            d.DetallePedidoId)
                    .Select(
                        d =>
                            new PedidoDetalleProductoDTO
                            {
                                ProductoId =
                                    d.ProductoId,

                                Nombre =
                                    d.Producto?.Nombre
                                    ?? "Producto",

                                ImagenPrincipal =
                                    d.Producto?.ImagenPrincipal,

                                Cantidad =
                                    d.Cantidad,

                                PrecioUnitario =
                                    d.PrecioUnitario,

                                SubTotal =
                                    d.SubTotal
                            })
                    .ToList()

                ?? new List<
                    PedidoDetalleProductoDTO>();


            return new PedidoDetalleClienteDTO
            {
                PedidoId =
                    pedido.PedidoId,

                NumeroPedido =
                    pedido.NumeroPedido,

                FechaPedido =
                    pedido.FechaPedido,

                Estado =
                    pedido.Estado,

                SubTotal =
                    pedido.SubTotal,

                IVA =
                    pedido.IVA,

                Descuento =
                    pedido.Descuento,

                Total =
                    pedido.Total,

                MetodoPago =
                    pedido.MetodoPago?.Nombre
                    ?? "No especificado",

                Productos =
                    productos
            };
        }


        // =================================================
        // CHECKOUT
        // =================================================

        public async Task<CheckoutPedidoResponseDTO>
            CrearCheckoutAsync(
                int clienteId,
                CheckoutPedidoDTO checkout)
        {
            // =============================================
            // VALIDAR CLIENTE ID
            // =============================================

            if (clienteId <= 0)
            {
                throw new InvalidOperationException(
                    "El cliente no es válido.");
            }


            // =============================================
            // VALIDAR CARRITO
            // =============================================

            if (checkout == null ||
                checkout.Items == null ||
                checkout.Items.Count == 0)
            {
                throw new InvalidOperationException(
                    "El carrito está vacío.");
            }


            // =============================================
            // VALIDAR MÉTODO
            // =============================================

            var metodo =
                checkout.Metodo?
                    .Trim()
                    .ToLowerInvariant()
                ?? string.Empty;


            if (metodo != "cash")
            {
                throw new InvalidOperationException(
                    "Por ahora únicamente está habilitado el pago en efectivo.");
            }


            // =============================================
            // VALIDAR CLIENTE
            // =============================================

            var cliente =
                await _context.Clientes
                    .FirstOrDefaultAsync(
                        c =>
                            c.ClienteId == clienteId
                            &&
                            c.Activo);


            if (cliente == null)
            {
                throw new InvalidOperationException(
                    "No se encontró el cliente autenticado.");
            }


            // =============================================
            // AGRUPAR PRODUCTOS
            // =============================================

            var itemsAgrupados =
                checkout.Items
                    .Where(
                        x =>
                            x.ProductoId > 0
                            &&
                            x.Cantidad > 0)
                    .GroupBy(
                        x =>
                            x.ProductoId)
                    .Select(
                        grupo =>
                            new CheckoutPedidoItemDTO
                            {
                                ProductoId =
                                    grupo.Key,

                                Cantidad =
                                    grupo.Sum(
                                        x =>
                                            x.Cantidad)
                            })
                    .ToList();


            if (itemsAgrupados.Count == 0)
            {
                throw new InvalidOperationException(
                    "No hay productos válidos para procesar.");
            }


            var productoIds =
                itemsAgrupados
                    .Select(
                        x =>
                            x.ProductoId)
                    .ToList();


            // =============================================
            // TRANSACCIÓN
            // =============================================

            await using var transaccion =
                await _context.Database
                    .BeginTransactionAsync(
                        IsolationLevel.Serializable);


            try
            {
                // =========================================
                // PRODUCTOS
                // =========================================

                var productos =
                    await _context.Productos
                        .Where(
                            producto =>
                                productoIds.Contains(
                                    producto.ProductoId)
                                &&
                                producto.Activo)
                        .ToListAsync();


                if (productos.Count !=
                    productoIds.Count)
                {
                    throw new InvalidOperationException(
                        "Uno o más productos ya no están disponibles.");
                }


                // =========================================
                // MÉTODO DE PAGO
                // =========================================

                var metodoPago =
                    await _context.MetodosPago
                        .FirstOrDefaultAsync(
                            m =>
                                m.Activo
                                &&
                                m.Nombre.ToLower() ==
                                    "efectivo");


                if (metodoPago == null)
                {
                    metodoPago =
                        new MetodoPago
                        {
                            Nombre =
                                "Efectivo",

                            Descripcion =
                                "Pago contra entrega en el domicilio.",

                            Activo =
                                true,

                            FechaCreacion =
                                DateTime.Now
                        };


                    _context.MetodosPago.Add(
                        metodoPago);


                    await _context.SaveChangesAsync();
                }


                // =========================================
                // DETALLES
                // =========================================

                var detalles =
                    new List<DetallePedido>();


                decimal subTotal =
                    0m;


                foreach (
                    var item
                    in itemsAgrupados)
                {
                    var producto =
                        productos.First(
                            p =>
                                p.ProductoId ==
                                item.ProductoId);


                    if (producto.Stock <
                        item.Cantidad)
                    {
                        throw new InvalidOperationException(
                            $"Stock insuficiente para {producto.Nombre}. " +
                            $"Disponible: {producto.Stock}.");
                    }


                    var precioUnitario =
                        producto.Precio;


                    var subtotalDetalle =
                        precioUnitario *
                        item.Cantidad;


                    subTotal +=
                        subtotalDetalle;


                    detalles.Add(
                        new DetallePedido
                        {
                            ProductoId =
                                producto.ProductoId,

                            Cantidad =
                                item.Cantidad,

                            PrecioUnitario =
                                precioUnitario,

                            SubTotal =
                                subtotalDetalle,

                            Activo =
                                true,

                            FechaCreacion =
                                DateTime.Now
                        });


                    producto.Stock -=
                        item.Cantidad;


                    producto.FechaModificacion =
                        DateTime.Now;
                }


                // =========================================
                // TOTALES
                // =========================================

                decimal iva =
                    0m;


                decimal descuento =
                    0m;


                decimal total =
                    subTotal
                    +
                    iva
                    -
                    descuento;


                // =========================================
                // PEDIDO
                // =========================================

                var pedido =
                    new Pedido
                    {
                        ClienteId =
                            clienteId,

                        MetodoPagoId =
                            metodoPago.MetodoPagoId,

                        FechaPedido =
                            DateTime.Now,

                        SubTotal =
                            subTotal,

                        IVA =
                            iva,

                        Descuento =
                            descuento,

                        Total =
                            total,

                        Estado =
                            EstadoPedido.Pendiente,

                        Activo =
                            true,

                        FechaCreacion =
                            DateTime.Now,

                        Detalles =
                            detalles
                    };


                _context.Pedidos.Add(
                    pedido);


                // =========================================
                // GUARDAR
                // =========================================

                await _context.SaveChangesAsync();


                // =========================================
                // COMMIT
                // =========================================

                await transaccion.CommitAsync();


                // =========================================
                // RESPUESTA
                // =========================================

                return new CheckoutPedidoResponseDTO
                {
                    Mensaje =
                        "Pedido creado correctamente.",

                    PedidoId =
                        pedido.PedidoId,

                    NumeroPedido =
                        pedido.NumeroPedido,

                    Total =
                        pedido.Total,

                    Estado =
                        pedido.Estado
                };
            }
            catch
            {
                await transaccion.RollbackAsync();

                throw;
            }
        }
    }
}