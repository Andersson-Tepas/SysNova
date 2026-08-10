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
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllAsync();

        Task<ClienteDTO?> GetByIdAsync(int id);

        Task<ClienteDTO> AddAsync(ClienteDTO dto);

        Task UpdateAsync(ClienteDTO dto);

        Task DeleteAsync(int id);
    }
}
