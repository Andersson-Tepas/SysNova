using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.BL.Interfaces
{
    public interface IImagenProductoService
    {
        Task<IEnumerable<ImagenProducto>> GetAllAsync();
        Task<ImagenProducto?> GetByIdAsync(object id);
        Task<IEnumerable<ImagenProducto>> FindAsync(Expression<Func<ImagenProducto, bool>> predicate);
        Task<ImagenProducto> AddAsync(ImagenProducto imagenProducto);
        Task UpdateAsync(ImagenProducto imagenProducto);
        Task DeleteAsync(ImagenProducto imagenProducto);
        Task<bool> ExistsAsync(Expression<Func<ImagenProducto, bool>> predicate);
    }

}