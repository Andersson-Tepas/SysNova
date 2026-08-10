using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using SysNova.BL.Interfaces;
using SysNova.DTO;
using SysNova.EN.Entities;
using SysNova.Repository.Interfaces;

namespace SysNova.BL.Services
{
    public class PreguntaFrecuenteService : IPreguntaFrecuenteService
    {
        private readonly IPreguntaFrecuenteRepository _repository;
        private readonly IMapper _mapper;

        public PreguntaFrecuenteService(IPreguntaFrecuenteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PreguntaFrecuenteDTO>> GetAllAsync()
        {
            var preguntas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PreguntaFrecuenteDTO>>(preguntas);
        }

        public async Task<PreguntaFrecuenteDTO?> GetByIdAsync(int id)
        {
            var pregunta = await _repository.GetByIdAsync(id);
            return _mapper.Map<PreguntaFrecuenteDTO?>(pregunta);
        }

        public async Task<IEnumerable<PreguntaFrecuenteDTO>> FindAsync(
            Expression<Func<PreguntaFrecuenteDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<PreguntaFrecuente, bool>>>(predicate);
            var preguntas = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<PreguntaFrecuenteDTO>>(preguntas);
        }

        public async Task<PreguntaFrecuenteDTO> AddAsync(PreguntaFrecuenteDTO preguntaDto)
        {
            var entity = _mapper.Map<PreguntaFrecuente>(preguntaDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<PreguntaFrecuenteDTO>(result);
        }

        public async Task UpdateAsync(PreguntaFrecuenteDTO preguntaDto)
        {
            var entity = _mapper.Map<PreguntaFrecuente>(preguntaDto);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
            }
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<PreguntaFrecuenteDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<PreguntaFrecuente, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}