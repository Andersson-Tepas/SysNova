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
    public class ResenaService : IResenaService
    {
        private readonly IResenaRepository _repository;

        public ResenaService(IResenaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Resena>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Resena?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Resena>> FindAsync(
            Expression<Func<Resena, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Resena> AddAsync(Resena resena)
        {
            return await _repository.AddAsync(resena);
        }

        public async Task UpdateAsync(Resena resena)
        {
            await _repository.UpdateAsync(resena);
        }

        public async Task DeleteAsync(Resena resena)
        {
            await _repository.DeleteAsync(resena);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Resena, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
