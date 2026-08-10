using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IResenaService
    {
        Task<IEnumerable<ResenaDTO>> GetAllAsync();

        Task<ResenaDTO?> GetByIdAsync(int id);

        Task<IEnumerable<ResenaDTO>> FindAsync(Expression<Func<ResenaDTO, bool>> predicate);

        Task<ResenaDTO> AddAsync(ResenaDTO resenaDto);

        Task UpdateAsync(ResenaDTO resenaDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<ResenaDTO, bool>> predicate);
    }
}