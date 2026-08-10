using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IFavoritoService
    {
        Task<IEnumerable<FavoritoDTO>> GetAllAsync();

        Task<FavoritoDTO?> GetByIdAsync(int id);

        Task<IEnumerable<FavoritoDTO>> FindAsync(Expression<Func<FavoritoDTO, bool>> predicate);

        Task<FavoritoDTO> AddAsync(FavoritoDTO favoritoDto);

        Task UpdateAsync(FavoritoDTO favoritoDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<FavoritoDTO, bool>> predicate);
    }
}