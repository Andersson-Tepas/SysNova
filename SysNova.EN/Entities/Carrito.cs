using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Carrito : BaseEntity
    {
        public int CarritoId { get; set; }

        public int ClienteId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public virtual Cliente? Cliente { get; set; } 

        public virtual ICollection<DetalleCarrito>? Detalles { get; set; } 
    }
}
