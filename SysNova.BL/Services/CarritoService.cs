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
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _repository;

        public CarritoService(ICarritoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Carrito>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Carrito?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Carrito>> FindAsync(
            Expression<Func<Carrito, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Carrito> AddAsync(Carrito carrito)
        {
            return await _repository.AddAsync(carrito);
        }

        public async Task UpdateAsync(Carrito carrito)
        {
            await _repository.UpdateAsync(carrito);
        }

        public async Task DeleteAsync(Carrito carrito)
        {
            await _repository.DeleteAsync(carrito);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Carrito, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
