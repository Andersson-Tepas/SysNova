using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface ICarritoService
    {
        Task<IEnumerable<Carrito>> GetAllAsync();
        Task<Carrito?> GetByIdAsync(object id);
        Task<IEnumerable<Carrito>> FindAsync(Expression<Func<Carrito, bool>> predicate);
        Task<Carrito> AddAsync(Carrito carrito);
        Task UpdateAsync(Carrito carrito);
        Task DeleteAsync(Carrito carrito);
        Task<bool> ExistsAsync(Expression<Func<Carrito, bool>> predicate);
    }

}