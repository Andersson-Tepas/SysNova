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
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> GetAllAsync();

        Task<CategoriaDTO?> GetByIdAsync(int id);

        Task<CategoriaDTO> AddAsync(CategoriaDTO dto);

        Task UpdateAsync(CategoriaDTO dto);

        Task DeleteAsync(int id);
    }
}