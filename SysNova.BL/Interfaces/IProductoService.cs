using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.EN.Entities;
using System.Linq.Expressions;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> GetAllAsync();

        Task<ProductoDTO?> GetByIdAsync(int id);

        Task<ProductoDTO> AddAsync(ProductoDTO dto);

        Task UpdateAsync(ProductoDTO dto);

        Task DeleteAsync(int id);
    }
}
