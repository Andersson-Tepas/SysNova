using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SysNova.DTO; // Asegúrate de tener la referencia a tus DTOs

namespace SysNova.BL.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();

        Task<UsuarioDTO?> GetByIdAsync(int id); // Se recomienda tipar el ID (por ejemplo 'int' en lugar de 'object')

        Task<IEnumerable<UsuarioDTO>> FindAsync(Expression<Func<UsuarioDTO, bool>> predicate);

        Task<UsuarioDTO> AddAsync(UsuarioDTO usuarioDto);

        Task UpdateAsync(UsuarioDTO usuarioDto);

        Task DeleteAsync(int id); // Es mejor práctica eliminar pasando el ID en lugar del DTO/Entidad completa

        Task<bool> ExistsAsync(Expression<Func<UsuarioDTO, bool>> predicate);
    }
}