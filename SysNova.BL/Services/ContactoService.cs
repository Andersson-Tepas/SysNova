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
    public class ContactoService : IContactoService
    {
        private readonly IContactoRepository _repository;

        public ContactoService(IContactoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Contacto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Contacto?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Contacto>> FindAsync(
            Expression<Func<Contacto, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Contacto> AddAsync(Contacto contacto)
        {
            return await _repository.AddAsync(contacto);
        }

        public async Task UpdateAsync(Contacto contacto)
        {
            await _repository.UpdateAsync(contacto);
        }

        public async Task DeleteAsync(Contacto contacto)
        {
            await _repository.DeleteAsync(contacto);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Contacto, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
