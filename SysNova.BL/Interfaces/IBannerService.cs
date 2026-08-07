using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IBannerService
    {
        Task<IEnumerable<Banner>> GetAllAsync();
        Task<Banner?> GetByIdAsync(object id);
        Task<IEnumerable<Banner>> FindAsync(Expression<Func<Banner, bool>> predicate);
        Task<Banner> AddAsync(Banner banner);
        Task UpdateAsync(Banner banner);
        Task DeleteAsync(Banner banner);
        Task<bool> ExistsAsync(Expression<Func<Banner, bool>> predicate);
    }
}
