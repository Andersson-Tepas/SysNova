using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IDetallePedidoService
    {
        Task<IEnumerable<DetallePedido>> GetAllAsync();
        Task<DetallePedido?> GetByIdAsync(object id);
        Task<IEnumerable<DetallePedido>> FindAsync(Expression<Func<DetallePedido, bool>> predicate);
        Task<DetallePedido> AddAsync(DetallePedido detallePedido);
        Task UpdateAsync(DetallePedido detallePedido);
        Task DeleteAsync(DetallePedido detallePedido);
        Task<bool> ExistsAsync(Expression<Func<DetallePedido, bool>> predicate);
    }

}