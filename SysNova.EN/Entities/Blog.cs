using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Blog : BaseEntity
    {
        public int BlogId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Resumen { get; set; } = string.Empty;

        public string Contenido { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        public string Autor { get; set; } = string.Empty;

        public DateTime FechaPublicacion { get; set; } = DateTime.Now;

        public int Visitas { get; set; }
    }
}
