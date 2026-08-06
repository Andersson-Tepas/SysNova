using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class MetodoPago : BaseEntity
    {
        public int MetodoPagoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
