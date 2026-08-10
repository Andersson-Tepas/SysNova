using SysNova.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IImagenProductoService
    {
        Task<IEnumerable<ImagenProductoDTO>> GetAllAsync();

        Task<ImagenProductoDTO?> GetByIdAsync(int id);

        Task<ImagenProductoDTO> AddAsync(ImagenProductoDTO dto);

        Task UpdateAsync(ImagenProductoDTO dto);

        Task DeleteAsync(int id);
    }
}