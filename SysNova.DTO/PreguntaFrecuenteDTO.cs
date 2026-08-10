using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DTO
{
    public class PreguntaFrecuenteDTO
    {
        public int PreguntaFrecuenteId { get; set; }

        public string Pregunta { get; set; } = string.Empty;

        public string Respuesta { get; set; } = string.Empty;

        public int Orden { get; set; }

        public bool Mostrar { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
