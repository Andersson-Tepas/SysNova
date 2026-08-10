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
    public class DetalleCarritoService : IDetalleCarritoService
    {
        private readonly IDetalleCarritoRepository _repository;
        private readonly IMapper _mapper;

        public DetalleCarritoService(IDetalleCarritoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetalleCarritoDTO>> GetAllAsync()
        {
            var detalles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DetalleCarritoDTO>>(detalles);
        }

        public async Task<DetalleCarritoDTO?> GetByIdAsync(int id)
        {
            var detalle = await _repository.GetByIdAsync(id);
            return _mapper.Map<DetalleCarritoDTO?>(detalle);
        }

        public async Task<IEnumerable<DetalleCarritoDTO>> FindAsync(
            Expression<Func<DetalleCarritoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<DetalleCarrito, bool>>>(predicate);
            var detalles = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<DetalleCarritoDTO>>(detalles);
        }

        public async Task<DetalleCarritoDTO> AddAsync(DetalleCarritoDTO detalleDto)
        {
            var entity = _mapper.Map<DetalleCarrito>(detalleDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<DetalleCarritoDTO>(result);
        }

        public async Task UpdateAsync(DetalleCarritoDTO detalleDto)
        {
            var entity = _mapper.Map<DetalleCarrito>(detalleDto);
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
            Expression<Func<DetalleCarritoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<DetalleCarrito, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}