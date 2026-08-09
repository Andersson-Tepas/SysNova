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
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodoPagoRepository _repository;

        public MetodoPagoService(IMetodoPagoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MetodoPago>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MetodoPago?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MetodoPago>> FindAsync(
            Expression<Func<MetodoPago, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<MetodoPago> AddAsync(MetodoPago metodoPago)
        {
            return await _repository.AddAsync(metodoPago);
        }

        public async Task UpdateAsync(MetodoPago metodoPago)
        {
            await _repository.UpdateAsync(metodoPago);
        }

        public async Task DeleteAsync(MetodoPago metodoPago)
        {
            await _repository.DeleteAsync(metodoPago);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<MetodoPago, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
