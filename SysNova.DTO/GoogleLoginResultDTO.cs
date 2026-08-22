using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DTO
{
    public class GoogleLoginResultDTO
    {
        public string Token { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
    }
}
