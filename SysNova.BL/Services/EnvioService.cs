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
    public class EnvioService : IEnvioService
    {
        private readonly IEnvioRepository _repository;
        private readonly IMapper _mapper;

        public EnvioService(IEnvioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnvioDTO>> GetAllAsync()
        {
            var envios = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<EnvioDTO>>(envios);
        }

        public async Task<EnvioDTO?> GetByIdAsync(int id)
        {
            var envio = await _repository.GetByIdAsync(id);
            return _mapper.Map<EnvioDTO?>(envio);
        }

        public async Task<IEnumerable<EnvioDTO>> FindAsync(
            Expression<Func<EnvioDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Envio, bool>>>(predicate);
            var envios = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<EnvioDTO>>(envios);
        }

        public async Task<EnvioDTO> AddAsync(EnvioDTO envioDto)
        {
            var entity = _mapper.Map<Envio>(envioDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<EnvioDTO>(result);
        }

        public async Task UpdateAsync(EnvioDTO envioDto)
        {
            var entity = _mapper.Map<Envio>(envioDto);
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
            Expression<Func<EnvioDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Envio, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}