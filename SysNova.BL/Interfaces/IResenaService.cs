using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IResenaService
    {
        Task<IEnumerable<Resena>> GetAllAsync();
        Task<Resena?> GetByIdAsync(object id);
        Task<IEnumerable<Resena>> FindAsync(Expression<Func<Resena, bool>> predicate);
        Task<Resena> AddAsync(Resena resena);
        Task UpdateAsync(Resena resena);
        Task DeleteAsync(Resena resena);
        Task<bool> ExistsAsync(Expression<Func<Resena, bool>> predicate);
    }
}
