using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido?> GetByIdAsync(object id);
        Task<IEnumerable<Pedido>> FindAsync(Expression<Func<Pedido, bool>> predicate);
        Task<Pedido> AddAsync(Pedido pedido);
        Task UpdateAsync(Pedido pedido);
        Task DeleteAsync(Pedido pedido);
        Task<bool> ExistsAsync(Expression<Func<Pedido, bool>> predicate);
    }
}
