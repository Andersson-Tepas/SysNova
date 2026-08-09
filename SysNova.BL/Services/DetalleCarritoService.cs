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
    public class DetalleCarritoService : IDetalleCarritoService
    {
        private readonly IDetalleCarritoRepository _repository;

        public DetalleCarritoService(IDetalleCarritoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DetalleCarrito>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DetalleCarrito?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<DetalleCarrito>> FindAsync(
            Expression<Func<DetalleCarrito, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<DetalleCarrito> AddAsync(DetalleCarrito detalle)
        {
            return await _repository.AddAsync(detalle);
        }

        public async Task UpdateAsync(DetalleCarrito detalle)
        {
            await _repository.UpdateAsync(detalle);
        }

        public async Task DeleteAsync(DetalleCarrito detalle)
        {
            await _repository.DeleteAsync(detalle);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<DetalleCarrito, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}