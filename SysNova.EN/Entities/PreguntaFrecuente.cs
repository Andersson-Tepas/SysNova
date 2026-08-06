using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class PreguntaFrecuente : BaseEntity
    {
        public int PreguntaFrecuenteId { get; set; }

        public string Pregunta { get; set; } = string.Empty;

        public string Respuesta { get; set; } = string.Empty;

        public int Orden { get; set; }

        public bool Mostrar { get; set; } = true;
    }
}
