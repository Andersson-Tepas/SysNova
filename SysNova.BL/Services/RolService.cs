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
    public class RolService : IRolService
    {
        private readonly IRolRepository _repository;

        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Rol?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Rol>> FindAsync(
            Expression<Func<Rol, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Rol> AddAsync(Rol rol)
        {
            return await _repository.AddAsync(rol);
        }

        public async Task UpdateAsync(Rol rol)
        {
            await _repository.UpdateAsync(rol);
        }

        public async Task DeleteAsync(Rol rol)
        {
            await _repository.DeleteAsync(rol);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Rol, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
