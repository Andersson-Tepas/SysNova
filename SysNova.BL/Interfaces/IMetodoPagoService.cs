using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IMetodoPagoService
    {
        Task<IEnumerable<MetodoPagoDTO>> GetAllAsync();

        Task<MetodoPagoDTO?> GetByIdAsync(int id);

        Task<IEnumerable<MetodoPagoDTO>> FindAsync(Expression<Func<MetodoPagoDTO, bool>> predicate);

        Task<MetodoPagoDTO> AddAsync(MetodoPagoDTO metodoPagoDto);

        Task UpdateAsync(MetodoPagoDTO metodoPagoDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<MetodoPagoDTO, bool>> predicate);
    }
}