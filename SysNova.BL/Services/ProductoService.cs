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
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        private readonly IMapper _mapper;

        public ProductoService(IProductoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductoDTO>>(productos);
        }

        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductoDTO?>(producto);
        }

        public async Task<IEnumerable<ProductoDTO>> FindAsync(
            Expression<Func<ProductoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Producto, bool>>>(predicate);
            var productos = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<ProductoDTO>>(productos);
        }

        public async Task<ProductoDTO> AddAsync(ProductoDTO productoDto)
        {
            var entity = _mapper.Map<Producto>(productoDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<ProductoDTO>(result);
        }

        public async Task UpdateAsync(ProductoDTO productoDto)
        {
            var entity = _mapper.Map<Producto>(productoDto);
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
            Expression<Func<ProductoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<Producto, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}