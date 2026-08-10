using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IDetallePedidoService
    {
        Task<IEnumerable<DetallePedidoDTO>> GetAllAsync();

        Task<DetallePedidoDTO?> GetByIdAsync(int id);

        Task<IEnumerable<DetallePedidoDTO>> FindAsync(Expression<Func<DetallePedidoDTO, bool>> predicate);

        Task<DetallePedidoDTO> AddAsync(DetallePedidoDTO detallePedidoDto);

        Task UpdateAsync(DetallePedidoDTO detallePedidoDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<DetallePedidoDTO, bool>> predicate);
    }
}