using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.EN.Enums;

namespace SysNova.DTO
{
    public class CheckoutPedidoResponseDTO
    {
        public string Mensaje { get; set; } = string.Empty;

        public int PedidoId { get; set; }

        public string NumeroPedido { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public EstadoPedido Estado { get; set; }
    }
}
