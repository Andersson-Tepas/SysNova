using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface ICarritoService
    {
        Task<IEnumerable<CarritoDTO>> GetAllAsync();

        Task<CarritoDTO?> GetByIdAsync(int id);

        Task<IEnumerable<CarritoDTO>> FindAsync(Expression<Func<CarritoDTO, bool>> predicate);

        Task<CarritoDTO> AddAsync(CarritoDTO carritoDto);

        Task UpdateAsync(CarritoDTO carritoDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<CarritoDTO, bool>> predicate);
    }
}