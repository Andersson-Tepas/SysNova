using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.BL.Interfaces;
using SysNova.EN.Entities;
using SysNova.Repository.Interfaces;
using System.Linq.Expressions;

namespace SysNova.BL.Services
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _repository;

        public BannerService(IBannerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Banner>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Banner?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Banner>> FindAsync(
            Expression<Func<Banner, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Banner> AddAsync(Banner banner)
        {
            return await _repository.AddAsync(banner);
        }

        public async Task UpdateAsync(Banner banner)
        {
            await _repository.UpdateAsync(banner);
        }

        public async Task DeleteAsync(Banner banner)
        {
            await _repository.DeleteAsync(banner);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Banner, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
