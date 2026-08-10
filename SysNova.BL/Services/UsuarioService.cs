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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            return _mapper.Map<UsuarioDTO?>(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> FindAsync(
            Expression<Func<UsuarioDTO, bool>> predicate)
        {
            // Traduce el predicado del DTO a la Entidad para la consulta
            var entityPredicate = _mapper.Map<Expression<Func<Usuario, bool>>>(predicate);
            var usuarios = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> AddAsync(UsuarioDTO usuarioDto)
        {
            var entity = _mapper.Map<Usuario>(usuarioDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<UsuarioDTO>(result);
        }

        public async Task UpdateAsync(UsuarioDTO usuarioDto)
        {
            var entity = _mapper.Map<Usuario>(usuarioDto);
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
            Expression<Func<UsuarioDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Usuario, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}