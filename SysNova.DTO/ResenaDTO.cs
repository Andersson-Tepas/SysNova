using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DTO
{
    public class ResenaDTO
    {
        public int ResenaId { get; set; }

        public int ClienteId { get; set; }

        public int ProductoId { get; set; }

        public byte Calificacion { get; set; }

        public string Comentario { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
