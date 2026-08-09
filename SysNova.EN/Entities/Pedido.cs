using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.EN.Enums;

namespace SysNova.EN.Entities
{
    public class Pedido : BaseEntity
    {
        public int PedidoId { get; set; }

        public string NumeroPedido { get; set; } = Guid.NewGuid().ToString("N")[..10].ToUpper();

        public int ClienteId { get; set; }

        public int MetodoPagoId { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.Now;

        public decimal SubTotal { get; set; }

        public decimal IVA { get; set; }

        public decimal Descuento { get; set; }

        public decimal Total { get; set; }

        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;

        public virtual Cliente? Cliente { get; set; } 

        public virtual MetodoPago? MetodoPago { get; set; } 

        public virtual ICollection<DetallePedido>? Detalles { get; set; } 

        public virtual Envio? Envio { get; set; }
    }
}
