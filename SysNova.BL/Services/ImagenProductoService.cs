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
    public class ImagenProductoService : IImagenProductoService
    {
        private readonly IImagenProductoRepository _repository;
        private readonly IMapper _mapper;

        public ImagenProductoService(IImagenProductoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImagenProductoDTO>> GetAllAsync()
        {
            var imagenes = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ImagenProductoDTO>>(imagenes);
        }

        public async Task<ImagenProductoDTO?> GetByIdAsync(int id)
        {
            var imagen = await _repository.GetByIdAsync(id);
            return _mapper.Map<ImagenProductoDTO?>(imagen);
        }

        public async Task<IEnumerable<ImagenProductoDTO>> FindAsync(
            Expression<Func<ImagenProductoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<ImagenProducto, bool>>>(predicate);
            var imagenes = await _repository.FindAsync(entityPredicate);
            return _mapper.Map<IEnumerable<ImagenProductoDTO>>(imagenes);
        }

        public async Task<ImagenProductoDTO> AddAsync(ImagenProductoDTO imagenDto)
        {
            var entity = _mapper.Map<ImagenProducto>(imagenDto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<ImagenProductoDTO>(result);
        }

        public async Task UpdateAsync(ImagenProductoDTO imagenDto)
        {
            var entity = _mapper.Map<ImagenProducto>(imagenDto);
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
            Expression<Func<ImagenProductoDTO, bool>> predicate)
        {
            var entityPredicate = _mapper.Map<Expression<Func<ImagenProducto, bool>>>(predicate);
            return await _repository.ExistsAsync(entityPredicate);
        }
    }
}