using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DTO
{
    public class ImagenProductoDTO
    {
        public int ImagenProductoId { get; set; }

        public string UrlImagen { get; set; } = string.Empty;

        public int Orden { get; set; }

        public int ProductoId { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
