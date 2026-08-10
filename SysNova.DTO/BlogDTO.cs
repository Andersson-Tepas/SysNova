using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DTO
{
    public class BlogDTO
    {
        public int BlogId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Resumen { get; set; } = string.Empty;

        public string Contenido { get; set; } = string.Empty;

        public string Imagen { get; set; } = string.Empty;

        public string Autor { get; set; } = string.Empty;

        public DateTime FechaPublicacion { get; set; }

        public int Visitas { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
