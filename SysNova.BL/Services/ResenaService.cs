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
    public class ResenaService : IResenaService
    {
        private readonly IResenaRepository _repository;
        private readonly IMapper _mapper;

        public ResenaService(IResenaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResenaDTO>> GetAllAsync()
        {
            var resenas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ResenaDTO>>(resenas);
        }

        public async Task<ResenaDTO?> GetByIdAsync(int id)
        {
            var resena = await _repository.GetByIdAsync(id);
            return _mapper.Map<ResenaDTO?>(resena);
        }

        public async Task<IEnumerable<ResenaDTO>> FindAsync(
            Expression<Func<ResenaDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Resena, bool>>>(predicate);
            var resenas = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<ResenaDTO>>(resenas);
        }

        public async Task<ResenaDTO> AddAsync(ResenaDTO resenaDto)
        {
            var entity = _mapper.Map<Resena>(resenaDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<ResenaDTO>(result);
        }

        public async Task UpdateAsync(ResenaDTO resenaDto)
        {
            var entity = _mapper.Map<Resena>(resenaDto);
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
            Expression<Func<ResenaDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Resena, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}