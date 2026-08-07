using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.EN.Entities;
using System.Linq.Expressions;

namespace SysNova.BL.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync();

        Task<Producto?> GetByIdAsync(object id);

        Task<IEnumerable<Producto>> FindAsync(
            Expression<Func<Producto, bool>> predicate);

        Task<Producto> AddAsync(Producto producto);

        Task UpdateAsync(Producto producto);

        Task DeleteAsync(Producto producto);

        Task<bool> ExistsAsync(
            Expression<Func<Producto, bool>> predicate);
    }
}
