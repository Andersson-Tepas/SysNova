using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTO>> GetAllAsync();

        Task<PedidoDTO?> GetByIdAsync(int id);

        Task<IEnumerable<PedidoDTO>> FindAsync(Expression<Func<PedidoDTO, bool>> predicate);

        Task<PedidoDTO> AddAsync(PedidoDTO pedidoDto);

        Task UpdateAsync(PedidoDTO pedidoDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<PedidoDTO, bool>> predicate);
    }
}