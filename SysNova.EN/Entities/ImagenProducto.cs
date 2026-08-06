using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class ImagenProducto : BaseEntity
    {
        public int ImagenProductoId { get; set; }

        public string UrlImagen { get; set; } = string.Empty;

        public int Orden { get; set; }

        public int ProductoId { get; set; }

        public virtual Producto Producto { get; set; } = null!;
    }
}
