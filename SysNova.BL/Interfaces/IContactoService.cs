using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IContactoService
    {
        Task<IEnumerable<ContactoDTO>> GetAllAsync();

        Task<ContactoDTO?> GetByIdAsync(int id);

        Task<IEnumerable<ContactoDTO>> FindAsync(Expression<Func<ContactoDTO, bool>> predicate);

        Task<ContactoDTO> AddAsync(ContactoDTO contactoDto);

        Task UpdateAsync(ContactoDTO contactoDto);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<ContactoDTO, bool>> predicate);
    }
}