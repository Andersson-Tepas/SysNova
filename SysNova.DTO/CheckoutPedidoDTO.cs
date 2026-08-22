using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.DTO
{
    public class CheckoutPedidoDTO
    {
        public string Metodo { get; set; } = string.Empty;

        public List<CheckoutPedidoItemDTO> Items { get; set; } = new();
    }


    public class CheckoutPedidoItemDTO
    {
        public int ProductoId { get; set; }

        public int Cantidad { get; set; }
    }
}
