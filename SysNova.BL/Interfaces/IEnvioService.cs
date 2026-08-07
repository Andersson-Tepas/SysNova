using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IEnvioService
    {
        Task<IEnumerable<Envio>> GetAllAsync();
        Task<Envio?> GetByIdAsync(object id);
        Task<IEnumerable<Envio>> FindAsync(Expression<Func<Envio, bool>> predicate);
        Task<Envio> AddAsync(Envio envio);
        Task UpdateAsync(Envio envio);
        Task DeleteAsync(Envio envio);
        Task<bool> ExistsAsync(Expression<Func<Envio, bool>> predicate);
    }

}