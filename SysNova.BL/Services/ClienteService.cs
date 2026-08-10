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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            var clientes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO?> GetByIdAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            return _mapper.Map<ClienteDTO?>(cliente);
        }

        public async Task<IEnumerable<ClienteDTO>> FindAsync(
            Expression<Func<ClienteDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Cliente, bool>>>(predicate);
            var clientes = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> AddAsync(ClienteDTO clienteDto)
        {
            var entity = _mapper.Map<Cliente>(clienteDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<ClienteDTO>(result);
        }

        public async Task UpdateAsync(ClienteDTO clienteDto)
        {
            var entity = _mapper.Map<Cliente>(clienteDto);
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
            Expression<Func<ClienteDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Cliente, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}