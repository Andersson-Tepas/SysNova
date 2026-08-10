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
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaDTO>> GetAllAsync();

        Task<MarcaDTO?> GetByIdAsync(int id);

        Task<MarcaDTO> AddAsync(MarcaDTO dto);

        Task UpdateAsync(MarcaDTO dto);

        Task DeleteAsync(int id);
    }
}