using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.EN.Enums;

namespace SysNova.DTO
{
    public class PedidoDetalleClienteDTO
    {
        public int PedidoId { get; set; }

        public string NumeroPedido { get; set; } = string.Empty;

        public DateTime FechaPedido { get; set; }

        public EstadoPedido Estado { get; set; }

        public decimal SubTotal { get; set; }

        public decimal IVA { get; set; }

        public decimal Descuento { get; set; }

        public decimal Total { get; set; }

        public string MetodoPago { get; set; } = string.Empty;

        public List<PedidoDetalleProductoDTO> Productos { get; set; } = new();
    }


    public class PedidoDetalleProductoDTO
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? ImagenPrincipal { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal SubTotal { get; set; }
    }
}
