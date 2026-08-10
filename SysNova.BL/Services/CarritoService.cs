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
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _repository;
        private readonly IMapper _mapper;

        public CarritoService(ICarritoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarritoDTO>> GetAllAsync()
        {
            var carritos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CarritoDTO>>(carritos);
        }

        public async Task<CarritoDTO?> GetByIdAsync(int id)
        {
            var carrito = await _repository.GetByIdAsync(id);
            return _mapper.Map<CarritoDTO?>(carrito);
        }

        public async Task<IEnumerable<CarritoDTO>> FindAsync(
            Expression<Func<CarritoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Carrito, bool>>>(predicate);
            var carritos = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<CarritoDTO>>(carritos);
        }

        public async Task<CarritoDTO> AddAsync(CarritoDTO carritoDto)
        {
            var entity = _mapper.Map<Carrito>(carritoDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<CarritoDTO>(result);
        }

        public async Task UpdateAsync(CarritoDTO carritoDto)
        {
            var entity = _mapper.Map<Carrito>(carritoDto);
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
            Expression<Func<CarritoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Carrito, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}