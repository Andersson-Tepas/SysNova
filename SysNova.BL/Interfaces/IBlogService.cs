using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDTO>> GetAllAsync();

        Task<BlogDTO?> GetByIdAsync(int id);

        Task<IEnumerable<BlogDTO>> FindAsync(Expression<Func<BlogDTO, bool>> predicate);

        Task<BlogDTO> AddAsync(BlogDTO blogDto);

        Task UpdateAsync(BlogDTO blogDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<BlogDTO, bool>> predicate);
    }
}