using System.Linq.Expressions;

using SysNova.DTO;
using SysNova.EN.Enums;

namespace SysNova.BL.Interfaces
{
    public interface IPedidoService
    {
        // ==========================================
        // CRUD
        // ==========================================

        Task<IEnumerable<PedidoDTO>> GetAllAsync();

        Task<PedidoDTO?> GetByIdAsync(
            int id);

        Task<IEnumerable<PedidoDTO>> FindAsync(
            Expression<Func<PedidoDTO, bool>> predicate);

        Task<PedidoDTO> AddAsync(
            PedidoDTO pedidoDto);

        Task UpdateAsync(
            PedidoDTO pedidoDto);

        Task DeleteAsync(
            int id);

        Task<bool> ExistsAsync(
            Expression<Func<PedidoDTO, bool>> predicate);


        // ==========================================
        // CHECKOUT
        // ==========================================

        Task<CheckoutPedidoResponseDTO> CrearCheckoutAsync(
            int clienteId,
            CheckoutPedidoDTO checkout);


        // ==========================================
        // PEDIDOS DEL CLIENTE
        // ==========================================

        Task<IEnumerable<PedidoDTO>> GetMisPedidosAsync(
            int clienteId);


        // ==========================================
        // DETALLE - CLIENTE
        // ==========================================

        Task<PedidoDetalleClienteDTO?>
            GetDetallePedidoClienteAsync(
                int clienteId,
                string numeroPedido);


        // ==========================================
        // DETALLE - ADMIN
        // ==========================================

        Task<PedidoDetalleClienteDTO?>
            GetDetallePedidoAdminAsync(
                string numeroPedido);


        // ==========================================
        // ACTUALIZAR ESTADO - ADMIN
        // ==========================================

        Task ActualizarEstadoAsync(
            int pedidoId,
            EstadoPedido estado);
    }
}