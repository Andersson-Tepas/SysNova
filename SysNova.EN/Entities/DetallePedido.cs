using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class DetallePedido : BaseEntity
    {
        public int DetallePedidoId { get; set; }

        public int PedidoId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal SubTotal { get; set; }

        public virtual Pedido Pedido { get; set; } = null!;

        public virtual Producto Producto { get; set; } = null!;
    }
}