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
    public class PreguntaFrecuenteService : IPreguntaFrecuenteService
    {
        private readonly IPreguntaFrecuenteRepository _repository;

        public PreguntaFrecuenteService(IPreguntaFrecuenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PreguntaFrecuente>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PreguntaFrecuente?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<PreguntaFrecuente>> FindAsync(
            Expression<Func<PreguntaFrecuente, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<PreguntaFrecuente> AddAsync(
            PreguntaFrecuente pregunta)
        {
            return await _repository.AddAsync(pregunta);
        }

        public async Task UpdateAsync(PreguntaFrecuente pregunta)
        {
            await _repository.UpdateAsync(pregunta);
        }

        public async Task DeleteAsync(PreguntaFrecuente pregunta)
        {
            await _repository.DeleteAsync(pregunta);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<PreguntaFrecuente, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
