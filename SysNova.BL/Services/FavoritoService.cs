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
    public class FavoritoService : IFavoritoService
    {
        private readonly IFavoritoRepository _repository;

        public FavoritoService(IFavoritoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Favorito>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Favorito?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Favorito>> FindAsync(
            Expression<Func<Favorito, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Favorito> AddAsync(Favorito favorito)
        {
            return await _repository.AddAsync(favorito);
        }

        public async Task UpdateAsync(Favorito favorito)
        {
            await _repository.UpdateAsync(favorito);
        }

        public async Task DeleteAsync(Favorito favorito)
        {
            await _repository.DeleteAsync(favorito);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Favorito, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
