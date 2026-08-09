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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Usuario?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Usuario>> FindAsync(
            Expression<Func<Usuario, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            return await _repository.AddAsync(usuario);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await _repository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            await _repository.DeleteAsync(usuario);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Usuario, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
