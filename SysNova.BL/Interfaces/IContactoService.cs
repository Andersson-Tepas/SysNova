using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IContactoService
    {
        Task<IEnumerable<Contacto>> GetAllAsync();
        Task<Contacto?> GetByIdAsync(object id);
        Task<IEnumerable<Contacto>> FindAsync(Expression<Func<Contacto, bool>> predicate);
        Task<Contacto> AddAsync(Contacto contacto);
        Task UpdateAsync(Contacto contacto);
        Task DeleteAsync(Contacto contacto);
        Task<bool> ExistsAsync(Expression<Func<Contacto, bool>> predicate);
    }

}