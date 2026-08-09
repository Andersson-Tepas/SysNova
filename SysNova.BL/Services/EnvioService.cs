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
    public class EnvioService : IEnvioService
    {
        private readonly IEnvioRepository _repository;

        public EnvioService(IEnvioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Envio>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Envio?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Envio>> FindAsync(
            Expression<Func<Envio, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Envio> AddAsync(Envio envio)
        {
            return await _repository.AddAsync(envio);
        }

        public async Task UpdateAsync(Envio envio)
        {
            await _repository.UpdateAsync(envio);
        }

        public async Task DeleteAsync(Envio envio)
        {
            await _repository.DeleteAsync(envio);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Envio, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
