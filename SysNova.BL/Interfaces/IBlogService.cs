using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(object id);
        Task<IEnumerable<Blog>> FindAsync(Expression<Func<Blog, bool>> predicate);
        Task<Blog> AddAsync(Blog blog);
        Task UpdateAsync(Blog blog);
        Task DeleteAsync(Blog blog);
        Task<bool> ExistsAsync(Expression<Func<Blog, bool>> predicate);
    }

}