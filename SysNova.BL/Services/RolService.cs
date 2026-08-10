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
    public class RolService : IRolService
    {
        private readonly IRolRepository _repository;
        private readonly IMapper _mapper;

        public RolService(IRolRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolDTO>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RolDTO>>(roles);
        }

        public async Task<RolDTO?> GetByIdAsync(int id)
        {
            var rol = await _repository.GetByIdAsync(id);
            return _mapper.Map<RolDTO?>(rol);
        }

        public async Task<IEnumerable<RolDTO>> FindAsync(
            Expression<Func<RolDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Rol, bool>>>(predicate);
            var roles = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<RolDTO>>(roles);
        }

        public async Task<RolDTO> AddAsync(RolDTO rolDto)
        {
            var entity = _mapper.Map<Rol>(rolDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<RolDTO>(result);
        }

        public async Task UpdateAsync(RolDTO rolDto)
        {
            var entity = _mapper.Map<Rol>(rolDto);
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
            Expression<Func<RolDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Rol, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}