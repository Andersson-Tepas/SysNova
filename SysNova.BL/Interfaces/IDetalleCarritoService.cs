using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IDetalleCarritoService
    {
        Task<IEnumerable<DetalleCarritoDTO>> GetAllAsync();

        Task<DetalleCarritoDTO?> GetByIdAsync(int id);

        Task<IEnumerable<DetalleCarritoDTO>> FindAsync(Expression<Func<DetalleCarritoDTO, bool>> predicate);

        Task<DetalleCarritoDTO> AddAsync(DetalleCarritoDTO detalleCarritoDto);

        Task UpdateAsync(DetalleCarritoDTO detalleCarritoDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<DetalleCarritoDTO, bool>> predicate);
    }
}