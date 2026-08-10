using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.DTO;

namespace SysNova.BL.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDTO login);
        Task<bool> RegisterAsync(RegisterDTO register);
    }
}
