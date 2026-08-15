using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DTO
{
    public class AuthResponseDTO
    {
        public string Mensaje { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}
