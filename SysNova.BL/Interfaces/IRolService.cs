using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetAllAsync();
        Task<Rol?> GetByIdAsync(object id);
        Task<IEnumerable<Rol>> FindAsync(Expression<Func<Rol, bool>> predicate);
        Task<Rol> AddAsync(Rol rol);
        Task UpdateAsync(Rol rol);
        Task DeleteAsync(Rol rol);
        Task<bool> ExistsAsync(Expression<Func<Rol, bool>> predicate);
    }
}
