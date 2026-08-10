using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IEnvioService
    {
        Task<IEnumerable<EnvioDTO>> GetAllAsync();

        Task<EnvioDTO?> GetByIdAsync(int id);

        Task<IEnumerable<EnvioDTO>> FindAsync(Expression<Func<EnvioDTO, bool>> predicate);

        Task<EnvioDTO> AddAsync(EnvioDTO envioDto);

        Task UpdateAsync(EnvioDTO envioDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<EnvioDTO, bool>> predicate);
    }
}