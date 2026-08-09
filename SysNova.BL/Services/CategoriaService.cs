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
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Categoria?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Categoria>> FindAsync(
            Expression<Func<Categoria, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Categoria> AddAsync(Categoria categoria)
        {
            return await _repository.AddAsync(categoria);
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            await _repository.UpdateAsync(categoria);
        }

        public async Task DeleteAsync(Categoria categoria)
        {
            await _repository.DeleteAsync(categoria);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Categoria, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
