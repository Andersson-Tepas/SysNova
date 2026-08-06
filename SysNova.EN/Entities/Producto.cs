using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Producto : BaseEntity
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public string CodigoSKU { get; set; } = string.Empty;

        public string? CodigoBarra { get; set; }

        public decimal Precio { get; set; }

        public decimal? PrecioAnterior { get; set; }

        public decimal? PorcentajeDescuento { get; set; }

        public int Stock { get; set; }

        public int StockMinimo { get; set; }

        public bool Destacado { get; set; }

        public bool Nuevo { get; set; }

        public string? ImagenPrincipal { get; set; }

        public int CategoriaId { get; set; }

        public int MarcaId { get; set; }

        public virtual Categoria Categoria { get; set; } = null!;

        public virtual Marca Marca { get; set; } = null!;

        public virtual ICollection<ImagenProducto> Imagenes { get; set; } = new List<ImagenProducto>();

        public virtual ICollection<DetalleCarrito> DetallesCarrito { get; set; } = new List<DetalleCarrito>();

        public virtual ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();

        public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }
}
