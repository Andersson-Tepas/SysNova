using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Usuario : BaseEntity
    {
        public int UsuarioId { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public int RolId { get; set; }

        public virtual Rol? Rol { get; set; }
    }
}
