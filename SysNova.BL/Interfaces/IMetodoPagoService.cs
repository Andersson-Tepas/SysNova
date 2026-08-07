using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IMetodoPagoService
    {
        Task<IEnumerable<MetodoPago>> GetAllAsync();
        Task<MetodoPago?> GetByIdAsync(object id);
        Task<IEnumerable<MetodoPago>> FindAsync(Expression<Func<MetodoPago, bool>> predicate);
        Task<MetodoPago> AddAsync(MetodoPago metodoPago);
        Task UpdateAsync(MetodoPago metodoPago);
        Task DeleteAsync(MetodoPago metodoPago);
        Task<bool> ExistsAsync(Expression<Func<MetodoPago, bool>> predicate);
    }

}