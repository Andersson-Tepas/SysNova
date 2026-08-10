using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IPreguntaFrecuenteService
    {
        Task<IEnumerable<PreguntaFrecuenteDTO>> GetAllAsync();

        Task<PreguntaFrecuenteDTO?> GetByIdAsync(int id);

        Task<IEnumerable<PreguntaFrecuenteDTO>> FindAsync(Expression<Func<PreguntaFrecuenteDTO, bool>> predicate);

        Task<PreguntaFrecuenteDTO> AddAsync(PreguntaFrecuenteDTO preguntaFrecuenteDto);

        Task UpdateAsync(PreguntaFrecuenteDTO preguntaFrecuenteDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<PreguntaFrecuenteDTO, bool>> predicate);
    }
}