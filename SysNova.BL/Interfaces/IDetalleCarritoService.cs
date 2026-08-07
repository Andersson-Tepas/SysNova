using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IDetalleCarritoService
    {
        Task<IEnumerable<DetalleCarrito>> GetAllAsync();
        Task<DetalleCarrito?> GetByIdAsync(object id);
        Task<IEnumerable<DetalleCarrito>> FindAsync(Expression<Func<DetalleCarrito, bool>> predicate);
        Task<DetalleCarrito> AddAsync(DetalleCarrito detalleCarrito);
        Task UpdateAsync(DetalleCarrito detalleCarrito);
        Task DeleteAsync(DetalleCarrito detalleCarrito);
        Task<bool> ExistsAsync(Expression<Func<DetalleCarrito, bool>> predicate);
    }

}