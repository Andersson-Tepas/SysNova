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
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repository;
        private readonly IMapper _mapper;

        public MarcaService(IMarcaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MarcaDTO>> GetAllAsync()
        {
            var marcas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MarcaDTO>>(marcas);
        }

        public async Task<MarcaDTO?> GetByIdAsync(int id)
        {
            var marca = await _repository.GetByIdAsync(id);
            return _mapper.Map<MarcaDTO?>(marca);
        }

        public async Task<IEnumerable<MarcaDTO>> FindAsync(
            Expression<Func<MarcaDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Marca, bool>>>(predicate);
            var marcas = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<MarcaDTO>>(marcas);
        }

        public async Task<MarcaDTO> AddAsync(MarcaDTO marcaDto)
        {
            var entity = _mapper.Map<Marca>(marcaDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<MarcaDTO>(result);
        }

        public async Task UpdateAsync(MarcaDTO marcaDto)
        {
            var entity = _mapper.Map<Marca>(marcaDto);
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
            Expression<Func<MarcaDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Marca, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}