using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.BL.Interfaces;
using SysNova.EN.Entities;
using SysNova.Repository.Interfaces;
using System.Linq.Expressions;

namespace SysNova.BL.Services
{
    public class ImagenProductoService : IImagenProductoService
    {
        private readonly IImagenProductoRepository _repository;

        public ImagenProductoService(IImagenProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ImagenProducto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ImagenProducto?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ImagenProducto>> FindAsync(
            Expression<Func<ImagenProducto, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<ImagenProducto> AddAsync(ImagenProducto imagen)
        {
            return await _repository.AddAsync(imagen);
        }

        public async Task UpdateAsync(ImagenProducto imagen)
        {
            await _repository.UpdateAsync(imagen);
        }

        public async Task DeleteAsync(ImagenProducto imagen)
        {
            await _repository.DeleteAsync(imagen);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<ImagenProducto, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
