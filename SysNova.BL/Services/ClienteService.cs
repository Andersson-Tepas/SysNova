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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Cliente?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Cliente>> FindAsync(
            Expression<Func<Cliente, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            return await _repository.AddAsync(cliente);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            await _repository.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(Cliente cliente)
        {
            await _repository.DeleteAsync(cliente);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Cliente, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
