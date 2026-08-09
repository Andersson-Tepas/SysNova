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
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repository;

        public MarcaService(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Marca>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Marca?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Marca>> FindAsync(
            Expression<Func<Marca, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Marca> AddAsync(Marca marca)
        {
            return await _repository.AddAsync(marca);
        }

        public async Task UpdateAsync(Marca marca)
        {
            await _repository.UpdateAsync(marca);
        }

        public async Task DeleteAsync(Marca marca)
        {
            await _repository.DeleteAsync(marca);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Marca, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
