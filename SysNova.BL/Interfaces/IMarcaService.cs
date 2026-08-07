using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<Marca>> GetAllAsync();
        Task<Marca?> GetByIdAsync(object id);
        Task<IEnumerable<Marca>> FindAsync(Expression<Func<Marca, bool>> predicate);
        Task<Marca> AddAsync(Marca marca);
        Task UpdateAsync(Marca marca);
        Task DeleteAsync(Marca marca);
        Task<bool> ExistsAsync(Expression<Func<Marca, bool>> predicate);
    }

}