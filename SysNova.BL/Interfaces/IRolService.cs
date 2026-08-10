using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<RolDTO>> GetAllAsync();

        Task<RolDTO?> GetByIdAsync(int id);

        Task<IEnumerable<RolDTO>> FindAsync(Expression<Func<RolDTO, bool>> predicate);

        Task<RolDTO> AddAsync(RolDTO rolDto);

        Task UpdateAsync(RolDTO rolDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<RolDTO, bool>> predicate);
    }
}