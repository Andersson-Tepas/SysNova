using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Contacto : BaseEntity
    {
        public int ContactoId { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string Asunto { get; set; } = string.Empty;

        public string Mensaje { get; set; } = string.Empty;

        public bool Leido { get; set; }

        public DateTime FechaContacto { get; set; } = DateTime.Now;
    }
}
