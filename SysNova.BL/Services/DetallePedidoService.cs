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
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly IDetallePedidoRepository _repository;
        private readonly IMapper _mapper;

        public DetallePedidoService(IDetallePedidoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetallePedidoDTO>> GetAllAsync()
        {
            var detalles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DetallePedidoDTO>>(detalles);
        }

        public async Task<DetallePedidoDTO?> GetByIdAsync(int id)
        {
            var detalle = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetallePedidoDTO?>(detalle);
        }

        public async Task<IEnumerable<DetallePedidoDTO>> FindAsync(
            Expression<Func<DetallePedidoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<DetallePedido, bool>>>(predicate);
            var detalles = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<DetallePedidoDTO>>(detalles);
        }

        public async Task<DetallePedidoDTO> AddAsync(DetallePedidoDTO detalleDto)
        {
            var entity = _mapper.Map<DetallePedido>(detalleDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<DetallePedidoDTO>(result);
        }

        public async Task UpdateAsync(DetallePedidoDTO detalleDto)
        {
            var entity = _mapper.Map<DetallePedido>(detalleDto);
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
            Expression<Func<DetallePedidoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<DetallePedido, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}