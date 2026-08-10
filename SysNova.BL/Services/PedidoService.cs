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
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoDTO>> GetAllAsync()
        {
            var pedidos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
        }

        public async Task<PedidoDTO?> GetByIdAsync(int id)
        {
            var pedido = await _repository.GetByIdAsync(id);
            return _mapper.Map<PedidoDTO?>(pedido);
        }

        public async Task<IEnumerable<PedidoDTO>> FindAsync(
            Expression<Func<PedidoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Pedido, bool>>>(predicate);
            var pedidos = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
        }

        public async Task<PedidoDTO> AddAsync(PedidoDTO pedidoDto)
        {
            var entity = _mapper.Map<Pedido>(pedidoDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<PedidoDTO>(result);
        }

        public async Task UpdateAsync(PedidoDTO pedidoDto)
        {
            var entity = _mapper.Map<Pedido>(pedidoDto);
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
            Expression<Func<PedidoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Pedido, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}