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
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodoPagoRepository _repository;
        private readonly IMapper _mapper;

        public MetodoPagoService(IMetodoPagoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MetodoPagoDTO>> GetAllAsync()
        {
            var metodos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MetodoPagoDTO>>(metodos);
        }

        public async Task<MetodoPagoDTO?> GetByIdAsync(int id)
        {
            var metodo = await _repository.GetByIdAsync(id);
            return _mapper.Map<MetodoPagoDTO?>(metodo);
        }

        public async Task<IEnumerable<MetodoPagoDTO>> FindAsync(
            Expression<Func<MetodoPagoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<MetodoPago, bool>>>(predicate);
            var metodos = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<MetodoPagoDTO>>(metodos);
        }

        public async Task<MetodoPagoDTO> AddAsync(MetodoPagoDTO metodoPagoDto)
        {
            var entity = _mapper.Map<MetodoPago>(metodoPagoDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<MetodoPagoDTO>(result);
        }

        public async Task UpdateAsync(MetodoPagoDTO metodoPagoDto)
        {
            var entity = _mapper.Map<MetodoPago>(metodoPagoDto);
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
            Expression<Func<MetodoPagoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<MetodoPago, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}