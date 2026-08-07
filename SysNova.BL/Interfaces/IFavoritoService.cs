using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IFavoritoService
    {
        Task<IEnumerable<Favorito>> GetAllAsync();
        Task<Favorito?> GetByIdAsync(object id);
        Task<IEnumerable<Favorito>> FindAsync(Expression<Func<Favorito, bool>> predicate);
        Task<Favorito> AddAsync(Favorito favorito);
        Task UpdateAsync(Favorito favorito);
        Task DeleteAsync(Favorito favorito);
        Task<bool> ExistsAsync(Expression<Func<Favorito, bool>> predicate);
    }
}
