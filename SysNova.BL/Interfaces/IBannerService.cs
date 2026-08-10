using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IBannerService
    {
        Task<IEnumerable<BannerDTO>> GetAllAsync();

        Task<BannerDTO?> GetByIdAsync(int id);

        Task<IEnumerable<BannerDTO>> FindAsync(Expression<Func<BannerDTO, bool>> predicate);

        Task<BannerDTO> AddAsync(BannerDTO bannerDto);

        Task UpdateAsync(BannerDTO bannerDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<BannerDTO, bool>> predicate);
    }
}