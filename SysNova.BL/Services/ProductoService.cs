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
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Producto?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Producto>> FindAsync(
            Expression<Func<Producto, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Producto> AddAsync(Producto producto)
        {
            return await _repository.AddAsync(producto);
        }

        public async Task UpdateAsync(Producto producto)
        {
            await _repository.UpdateAsync(producto);
        }

        public async Task DeleteAsync(Producto producto)
        {
            await _repository.DeleteAsync(producto);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Producto, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
