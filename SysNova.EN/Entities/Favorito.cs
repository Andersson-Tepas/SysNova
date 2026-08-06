using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysNova.EN.Entities
{
    public class Favorito : BaseEntity
    {
        public int FavoritoId { get; set; }

        public int ClienteId { get; set; }

        public int ProductoId { get; set; }

        public DateTime FechaAgregado { get; set; } = DateTime.Now;

        public virtual Cliente Cliente { get; set; } = null!;

        public virtual Producto Producto { get; set; } = null!;
    }
}
